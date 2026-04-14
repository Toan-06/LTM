using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using System.Threading.Tasks;
using System.Linq;

namespace Server.Controllers
{
    // Xác định đường dẫn gốc: /api/file
    [Route("api/[controller]")]
    // Thẻ đánh dấu API
    [ApiController]

    // ===== TODO (Toàn - Người 1): BẬT KHIẾN VỆ TOKEN BẢO VỆ MỌI API =====
    // Ý NGHĨA THẺ NÀY: Mọc khiên an ninh chặn mọi Request HTTP tới Cổng File. Phụ thuộc vào Authentication của Program. Nếu ko có JWT Token móc nối từ AuthController gửi qua, nạy cửa cũng không vào được. 
    [Authorize] 
    public class FileController : ControllerBase
    {
        // Biến trữ hệ thống móc nối làm việc với file.
        private readonly IFileStorageService _fileService;

        // Constructor tiêm cái IFileStorageService kia vào
        public FileController(IFileStorageService fileService)
        {
            _fileService = fileService;
        }

        // ===== ĐÃ XONG: HÀM HELPER HỖ TRỢ BÓC TOKEN =====
        private string GetUserFolder()
        {
            // Trích xuất claim "Folder" mà lúc nãy AuthController nhét vào Token
            return User.Claims.FirstOrDefault(c => c.Type == "Folder")?.Value;
        }
        
        // Nhận phương thức cấu trúc cổng mạng GET, URL con sẽ trở thành: /api/file/list?path=..... Nạp biến Query Parameter.
        [HttpGet("list")]
        public IActionResult GetItems([FromQuery] string path = "")
        {
            try
            {
                string folder = GetUserFolder();
                if (string.IsNullOrEmpty(folder)) return Unauthorized("Lỗi xác thực Token");

                var items = _fileService.GetItems(folder, path);
                return Ok(items);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Lỗi truy xuất hệ thống: " + ex.Message });
            }
        }

        // Phương thức gánh file IFormFile cực nặng. Task bọc khối luồng chờ tải để gồng hệ thống không nghẹt thở mạng. URL là post tới /api/file/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromQuery] string path, IFormFile file)
        {
            try
            {
                string folder = GetUserFolder();
                if (string.IsNullOrEmpty(folder)) return Unauthorized("Token sai lạc");

                await _fileService.SaveFileAsync(folder, path, file);
                return Ok(new { message = "Tải file lên ổ cứng Server thành công!" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Xảy ra lỗi trong luồng RAM: " + ex.Message });
            }
        }

        // Kêu gọi kết nối download lấy nhả Stream ra ngoài rò trên dòng tải HTTP.
        [HttpGet("download")]
        public IActionResult DownloadFile([FromQuery] string filePath)
        {
            try
            {
                string folder = GetUserFolder();
                if (string.IsNullOrEmpty(folder)) return Unauthorized();

                var stream = _fileService.GetFileStream(folder, filePath);
                if (stream == null) return NotFound(new { message = "Không tìm kiếm thấy file trên ổ đĩa Server!" });

                var fileName = System.IO.Path.GetFileName(filePath);
                // Báo cho giao thức HTTP Browser của Client đây là File nhị phân dạng Octet. Bơm cả mảng stream này thẳng xuống đường truyền.
                return File(stream, "application/octet-stream", fileName);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Xảy ra lỗi chập luồng ống: " + ex.Message });
            }
        }

        // Cấu trúc cổng HttpDelete là quy chuẩn mạng chuyên cho các yêu cầu Hủy hoại dữ liệu.
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string itemPath)
        {
            try
            {
                string folder = GetUserFolder();
                if (string.IsNullOrEmpty(folder)) return Unauthorized();

                _fileService.DeleteItem(folder, itemPath);
                return Ok(new { message = "Lệnh Hủy Diệt Vật Lý Thành Công!" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Lỗi Hủy Diệt File: " + ex.Message });
            }
        }
    }
}
