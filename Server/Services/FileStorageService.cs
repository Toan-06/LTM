using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    // DỊCH VỤ FILE (Người 1 quản lý)
    public class FileService
    {
        private readonly string _root = Path.Combine(Directory.GetCurrentDirectory(), "Storage");

        public FileService()
        {
            if (!Directory.Exists(_root)) Directory.CreateDirectory(_root);
        }

        // Lấy đường dẫn: Luôn bắt đầu từ Storage/Username để đảm bảo PHÂN QUYỀN
        public string GetPath(string user, string subPath = "") 
        {
            string userRoot = Path.Combine(_root, user);
            if (!Directory.Exists(userRoot)) Directory.CreateDirectory(userRoot);
            
            // Kết hợp với subPath (nếu có)
            return Path.Combine(userRoot, subPath ?? "");
        }

        // Tạo thư mục
        public void CreateDir(string user, string subPath = "")
        {
            Directory.CreateDirectory(GetPath(user, subPath));
        }

        // Liệt kê file/thư mục
        public string[] List(string user, string subPath = "")
        {
            string p = GetPath(user, subPath);
            if (!Directory.Exists(p)) return new string[] { "Thư mục không tồn tại" };
            
            return Directory.GetFileSystemEntries(p)
                            .Select(x => (Directory.Exists(x) ? "[D] " : "[F] ") + Path.GetFileName(x))
                            .ToArray();
        }

        // Lưu file (Từ mảng byte)
        public async Task SaveFile(string user, string subPath, string fileName, byte[] data)
        {
            string p = Path.Combine(GetPath(user, subPath), fileName);
            await File.WriteAllBytesAsync(p, data);
        }

        // Đọc file (Trả về mảng byte)
        public async Task<byte[]> ReadFile(string user, string filePath)
        {
            string p = GetPath(user, filePath);
            if (!File.Exists(p)) return null;
            return await File.ReadAllBytesAsync(p);
        }

        // Xóa
        public void Delete(string user, string subPath)
        {
            string p = GetPath(user, subPath);
            if (File.Exists(p)) File.Delete(p);
            else if (Directory.Exists(p)) Directory.Delete(p, true);
        }
    }
}
