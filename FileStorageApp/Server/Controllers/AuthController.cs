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
            // ===== TODO (Toàn - Người 1): LUỒNG ĐĂNG KÝ (REGISTER) =====
            /*
             * Mục tiêu:
             * - Kiểm tra trùng lặp UserName. Ném trả BadRequest.
             * - Không được lưu mật khẩu gốc. Phải băm (Hash).
             * - Tạo cơ sở thư mục vật lý đầu tiên cho người dùng lưu dữ liệu sau này.
             * 
             * Hướng làm (Chỉ còn mình Toàn - code logic bên trong lõi này):
             * - Lệnh kiểm tra tồn tại: _context.Users.Any(u => u.Username == req.Username)
             * - Cài NuGet "BCrypt.Net-Next" -> Dùng BCrypt.Net.BCrypt.HashPassword(req.Password).
             * - Dùng kỹ thuật sinh tự động ID rác (Guid.NewGuid().ToString()) làm tên StorageFolderName để tránh Hacker đoán.
             * - Gắn tất cả vào Object User -> .Add(user) -> .SaveChanges().
             * - Đừng quên gọi _fileService.CreateUserDirectory(cái tên Guid đó);
             */
            return Ok(); // Lệnh trả trạng thái mạng 200 Bình Thường tới Client (Không cần trả JSON text)
        }

        // Quy định cổng phụ: /api/auth/login nhận phương thức POST xác thực.
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            // ===== TODO (Toàn - Người 1): LUỒNG ĐĂNG NHẬP & SINH THẺ BÀI (JWT) =====
            /*
             * Mục tiêu:
             * - Khớp Username và Giải mã PasswordHash.
             * - Trả về token mạng có giấu mã Folder của User đó (để FileController lấy ra móc dữ liệu chuẩn xác).
             * 
             * Hướng làm:
             * - Kéo data User ra: _context.Users.FirstOrDefault(...). Nếu null = Unauthorized.
             * - Khớp Pass: BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash). Nếu check == false = Unauthorized.
             * - Lên mạng gõ "Generate C# JWT Object". Dùng JwtSecurityTokenHandler.
             * - !! BẮT BUỘC nhét 2 cục này vào mảng Claim của JWT Token:
             *   + Tham số Name: user.Username
             *   + Tham số "Folder": user.StorageFolderName
             * - Trả về Token kèm mã báo Ok.
             */
            return Ok(); // Cấu trúc khi trả JSON ra ngòai Client: return Ok(new { ThuocTinh = ket_qua });
        }
    }
}
