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

        // BÓC TÁCH MẢNG NHIỆM VỤ NGƯỜI 1: ĐÃ HOÀN THIỆN
        public bool IsSafePath(string rootPath, string targetPath)
        {
            var fullRootPath = Path.GetFullPath(rootPath);
            var fullTargetPath = Path.GetFullPath(targetPath);
            // Hacker truyền targetPath kiểu "../../../Windows", sau khi GetFullPath nó không thể nào có phần đầu giống fullRootPath được.
            return fullTargetPath.StartsWith(fullRootPath, System.StringComparison.OrdinalIgnoreCase);
        }

        public void CreateUserDirectory(string folderName)
        {
            var path = GetUserRootPath(folderName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public string[] GetItems(string userFolder, string subPath)
        {
            string folderPath = Path.Combine(GetUserRootPath(userFolder), subPath ?? "");
            if (!IsSafePath(GetUserRootPath(userFolder), folderPath)) return System.Array.Empty<string>();
            if (!Directory.Exists(folderPath)) return System.Array.Empty<string>();

            var folders = Directory.GetDirectories(folderPath);
            var files = Directory.GetFiles(folderPath);
            
            var result = new System.Collections.Generic.List<string>();
            foreach(var d in folders) result.Add("[Dir] " + Path.GetFileName(d));
            foreach(var f in files) result.Add("[File] " + Path.GetFileName(f));
            
            return result.ToArray();
        }

        public async Task SaveFileAsync(string userFolder, string subPath, IFormFile file)
        {
            if (file == null || file.Length == 0) return;
            string folderPath = Path.Combine(GetUserRootPath(userFolder), subPath ?? "");
            if (!IsSafePath(GetUserRootPath(userFolder), folderPath)) return;
            
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            
            string filePath = Path.Combine(folderPath, file.FileName);
            if (!IsSafePath(GetUserRootPath(userFolder), filePath)) return;

            // Ghi file từ RAM xuống ổ cứng vật lý
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public FileStream GetFileStream(string userFolder, string filePath)
        {
            string fullPath = Path.Combine(GetUserRootPath(userFolder), filePath ?? "");
            if (!IsSafePath(GetUserRootPath(userFolder), fullPath)) return null;
            if (!File.Exists(fullPath)) return null;

            return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        }

        public void DeleteItem(string userFolder, string itemPath)
        {
            string fullPath = Path.Combine(GetUserRootPath(userFolder), itemPath ?? "");
            if (!IsSafePath(GetUserRootPath(userFolder), fullPath)) return;

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            else if (Directory.Exists(fullPath))
            {
                Directory.Delete(fullPath, true);
            }
        }
    }
}
