// Khai báo thư viện mạng lõi MVC để xây dựng cổng kết nối
using Microsoft.AspNetCore.Mvc;
// Nạp AppDbContext vào
using Server.Data;
// Nạp model định nghĩa dữ liệu gởi từ Client
using Server.Models;
// Nạp chức năng băm file ổ cứng
using Server.Services;
// Các tiện ích lặt vặt (Linq dò mảng)
using System.Linq;

// Định vị vùng không gian bảo vệ của file
namespace Server.Controllers
{
    // Xác định đường dẫn gốc: http://localhost:5000/api/auth (Từ auth tự bắt tự động nhờ biến [controller])
    [Route("api/[controller]")]
    // Thẻ bài xác nhận đây là file Controller phục vụ cổng mạng chứ ko phải file tính toán thường. Nếu nhập sai chuẩn nó sẽ chửi.
    [ApiController]

    // Lớp Controller trung tâm giải quyết khâu mở cổng tiếp Khách cho luồng Đăng Nhập / Đăng ký
    public class AuthController : ControllerBase
    {
        // Liên kết tĩnh (chỉ đọc) lưu giữ Database và Service
        private readonly AppDbContext _context;
        private readonly IFileStorageService _fileService;
        
        // Cầu nối tiêm phụ thuộc (Dependency Injection Móc nối từ Program.cs đổ vào) 
        public AuthController(AppDbContext context, IFileStorageService fileService)
        {
            // Trút giá trị tiêm sang biến nội bộ cho ông Toàn xài.
            _context = context;
            _fileService = fileService;
        }

        // Quy định cổng phụ: /api/auth/register nhận phương thức Post. Giấu kín data trong [FromBody]
        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginRequest req)
        {
            // Bước 1: Kiểm tra username đã tồn tại chưa
            if (_context.Users.Any(u => u.Username == req.Username))
            {
                return BadRequest(new { message = "Tài khoản đã tồn tại!" });
            }

            // Bước 2: Băm mật khẩu
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(req.Password);

            // Bước 3: Sinh mã bí mật đại diện cho thư mục vật lý lưu trữ file của User này
            string folderName = System.Guid.NewGuid().ToString();

            // Bước 4: Lưu vào cơ sở dữ liệu
            var user = new User
            {
                Username = req.Username,
                PasswordHash = hashedPassword,
                StorageFolderName = folderName
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            // Bước 5: Tạo thư mục trống trên ổ cứng (FileStorageService)
            _fileService.CreateUserDirectory(folderName);

            return Ok(new { message = "Đăng ký thành công!" });
        }

        // Quy định cổng phụ: /api/auth/login nhận phương thức POST xác thực.
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            // Bước 1: Tìm User trong DB
            var user = _context.Users.FirstOrDefault(u => u.Username == req.Username);
            if (user == null)
            {
                return Unauthorized(new { message = "Tài khoản không tồn tại!" });
            }

            // Bước 2: Đối chiếu mật khẩu băm
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash);
            if (!isPasswordCorrect)
            {
                return Unauthorized(new { message = "Sai mật khẩu!" });
            }

            // Bước 3: Sinh JWT Token chứa kẹp FolderName (Thẻ bài)
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            // Khóa bí mật ký Token (thường phải giấu trong appsettings.json, ở đây mình viết thẳng tạm để chạy được ngay)
            var secretKey = System.Text.Encoding.ASCII.GetBytes("ChuoiBiMatSieuDaiCuaToan_PhaiDaiHon32KyTuDoBanNhe_FileStorageApp");
            
            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.Username),
                    new System.Security.Claims.Claim("Folder", user.StorageFolderName)
                }),
                Expires = System.DateTime.UtcNow.AddDays(1), // Token sống được 1 ngày
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(secretKey), 
                    Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwtString = tokenHandler.WriteToken(token);

            // Bước 4: Trả Token về cho Client
            return Ok(new { 
                message = "Đăng nhập thành công!",
                token = jwtString
            });
        }
    }
}
