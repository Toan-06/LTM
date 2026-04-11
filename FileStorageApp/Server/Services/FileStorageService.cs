// Nạp thư viện lõi hệ thống thao tác Tệp của hệ điều hành
using System.IO;
// Nạp luồng vận hành mạng bất đồng bộ giúp app ko treo
using System.Threading.Tasks;
// Khai báo form file tải lên từ giao thức Http
using Microsoft.AspNetCore.Http;

namespace Server.Services
{
    // Bản hợp đồng (Interface) định ra hàng loạt Khuôn mẫu Cấm sai sửa để các lớp phải tuân theo khi kết nối API
    public interface IFileStorageService
    {
        string GetUserRootPath(string folderName);
        bool IsSafePath(string rootPath, string targetPath);
        void CreateUserDirectory(string folderName);
        string[] GetItems(string userFolder, string subPath);
        Task SaveFileAsync(string userFolder, string subPath, IFormFile file);
        FileStream GetFileStream(string userFolder, string filePath);
        void DeleteItem(string userFolder, string itemPath);
    }

    // Lớp thực thi bản hợp đồng. Là trung tâm xử lý thao tác với SSD / HDD.
    public class FileStorageService : IFileStorageService
    {
        // Biến lưu giữ cứng cái đường dẫn ngầm định trên máy chủ (C:\App\StorageTemp)
        private readonly string _baseStoragePath;

        // Constructor chạy đầu tiên lúc FileStorageService được đẻ ra 1 lần
        public FileStorageService()
        {
            // Thiết lập gắn mác đường dẫn mặc định bằng cách tính toán Vị trí file Executable hiện hành nối với chữ StorageTemp
            _baseStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "StorageTemp");
            // Nếu cục thư mục gốc này chưa khởi tạo (mới chạy lần đầu) thì ra tay sinh mới nó
            if (!Directory.Exists(_baseStoragePath)) Directory.CreateDirectory(_baseStoragePath);
        }

        // Hàm helper sinh ra đường dẫn gốc cho thư mục con bí mật của riêng cá nhân User. (Biến folderName là mã Guid)
        public string GetUserRootPath(string folderName) => Path.Combine(_baseStoragePath, folderName);

        // BÓC TÁCH MẢNG NHIỆM VỤ NGƯỜI 1: TẤT CẢ TODO VẪN GIỮ TRANG THÁI NHƯ CŨ BÊN TRONG!
        public bool IsSafePath(string rootPath, string targetPath)
        {
            // ===== TODO (Toàn - Người 1): THUẬT TOÁN BẮT HACKER =====
            /*
             * Mục tiêu:
             * - Phải rào được lỗ hổng chết người của Lập trình mạng: Path Traversal (Client cố tình truy cập "D:/Hack/../../Máy chủ").
             * - Hàm này sẽ được các hàm bên dưới gọi để rào chốt An ninh.
             * 
             * Hướng làm:
             * - Sử dụng hàm lõi: Path.GetFullPath(chuỗi) trên biến rootPath và biến targetPath để Hệ điều hành bung tên thật của chuỗi độc hại ra.
             * - So sánh: Dùng hàm TargetFull.StartsWith(RootFull, StringComparison.OrdinalIgnoreCase).
             * - Trả True (An toàn) nếu Target nằm trọn vúi trong Root. False (Hack) nếu Target lòi ra ổ C, ổ D...
             */
            return false;
        }

        public void CreateUserDirectory(string folderName)
        {
            // TODO (Toàn - Người 1): Viết lệnh gọi Directory.CreateDirectory, nhớ Check Directory.Exists trước.
        }

        public string[] GetItems(string userFolder, string subPath)
        {
            // ===== TODO (Toàn - Người 1): THUẬT TOÁN LẤY & GỘP DANH SÁCH =====
            /* Mọi thứ đã có hướng dẫn như cũ, bạn tự tin mở khóa chức năng ... */
            return System.Array.Empty<string>();
        }

        public async Task SaveFileAsync(string userFolder, string subPath, IFormFile file)
        {
            // ===== TODO (Toàn - Người 1): THUẬT TOÁN TẢI FILE BẤT ĐỒNG BỘ =====
            /* Mọi thứ đã có hướng dẫn như cũ, hãy sử dụng await và FileStream. */
        }

        public FileStream GetFileStream(string userFolder, string filePath)
        {
            // TODO (Toàn - Người 1): Trả về 1 FileStream (dạng FileMode.Open, FileAccess.Read) để Controller ném qua mạng về Client. Nhớ kiểm tra IsSafePath.
            return null;
        }

        public void DeleteItem(string userFolder, string itemPath)
        {
            // TODO (Toàn - Người 1): Dùng If kiểm tra (File.Exists thì xóa File, Directory.Exists thì xóa Folder - dùng cờ true để bắt buộc xóa dù bên trong có ruột hay không).
        }
    }
}
