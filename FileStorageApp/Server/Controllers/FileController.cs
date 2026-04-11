using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using System.Threading.Tasks;

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

        // ===== TODO (Toàn - Người 1): HÀM HELPER HỖ TRỢ BÓC TOKEN =====
        /*
         * Mục đích: Lấy được Thư Mục bí mật từ Claims truyền từ Token sang. 
         * Cần tạo viết hàm bảo mật rỗng: Private string GetUserFolder() -> Trả về Dãy Chuỗi Ẩn (Folder).
         * Code gợi ý: return User.Claims.First(c => c.Type == "Folder").Value;
         */
        
        // Nhận phương thức cấu trúc cổng mạng GET, URL con sẽ trở thành: /api/file/list?path=..... Nạp biến Query Parameter.
        [HttpGet("list")]
        public IActionResult GetItems([FromQuery] string path = "")
        {
            // ===== TODO (Toàn - Người 1): CẤU TRÚC TRẢ DANH SÁCH =====
            /* 
             * Dùng Thùng Try-Catch bắt lỗi. Bên trong Try đọc biến ẩn Folder qua hàm Helper GetUserFolder. Ném nó và path qua hàm _fileService.GetItems(...) để lấy mảng file. Return trút mảng Json. Báo Catch về BadRequest (Lỗi hệ thống trả gởi cho Client).
             */
            return Ok();
        }

        // Phương thức gánh file IFormFile cực nặng. Task bọc khối luồng chờ tải để gồng hệ thống không nghẹt thở mạng. URL là post tới /api/file/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromQuery] string path, IFormFile file)
        {
            // TODO (Toàn - Người 1): Gợi gọi hàm _fileService.SaveFileAsync có dùng "await" chạy đệm.
            return Ok();
        }

        // Kêu gọi kết nối download lấy nhả Stream ra ngoài rò trên dòng tải HTTP.
        [HttpGet("download")]
        public IActionResult DownloadFile([FromQuery] string filePath)
        {
            // ===== TODO (Toàn - Người 1): CẤU TRÚC LƯU CHUYỂN FILE QUA GIAO THỨC OCTET =====
            /*
             * Trả về luồng Tải (File) với File MIME Type "application/octet-stream". Chứ ko return kiểu mã chèn OK() của JSON được ở đây.
             */
            return Ok();
        }

        // Cấu trúc cổng HttpDelete là quy chuẩn mạng chuyên cho các yêu cầu Hủy hoại dữ liệu.
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string itemPath)
        {
            // TODO (Toàn - Người 1): Hủy diệt File/Folder vật lý và trả về cấu trúc chữ OK JSON.
            return Ok();
        }
    }
}
