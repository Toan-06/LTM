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
            
            // Bảo mật: Loại bỏ dấu gạch chéo ở đầu để tránh Path.Combine hiểu nhầm là gốc ổ đĩa
            string sanitizedPath = (subPath ?? "").TrimStart('/', '\\');
            string combinedPath = Path.Combine(userRoot, sanitizedPath);
            
            string fullPath = Path.GetFullPath(combinedPath);
            // Đảm bảo root luôn có dấu gạch chéo ở cuối để so sánh chính xác (tránh giang vs giang2)
            string rootFullPath = Path.GetFullPath(userRoot).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;

            if (!fullPath.StartsWith(rootFullPath) && fullPath != rootFullPath.TrimEnd(Path.DirectorySeparatorChar))
            {
                throw new UnauthorizedAccessException("Bạn không có quyền truy cập ngoài thư mục được cấp!");
            }

            return fullPath;
        }

        // Tạo thư mục
        public void CreateDir(string user, string subPath = "")
        {
            Directory.CreateDirectory(GetPath(user, subPath));
        }

        // Liệt kê file/thư mục (Có kèm kích thước và ngày)
        public string[] List(string user, string subPath = "")
        {
            string p = GetPath(user, subPath);
            if (!Directory.Exists(p)) return new string[] { "Thư mục không tồn tại" };

            return Directory.GetFileSystemEntries(p)
                            .Select(x => {
                                var info = new FileInfo(x);
                                string type = Directory.Exists(x) ? "[D] " : "[F] ";
                                string name = type + Path.GetFileName(x);
                                string size = Directory.Exists(x) ? "" : FormatSize(info.Length);
                                string date = info.LastWriteTime.ToString("dd/MM/yyyy HH:mm");
                                return $"{name}|{size}|{date}";
                            })
                            .ToArray();
        }

        private string FormatSize(long bytes)
        {
            string[] units = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int unitIndex = 0;
            while (len >= 1024 && unitIndex < units.Length - 1)
            {
                len /= 1024;
                unitIndex++;
            }
            return $"{len:N1} {units[unitIndex]}";
        }

        // Lưu file (Từ mảng byte)
        public async Task SaveFile(string user, string subPath, string fileName, byte[] data)
        {
            string p = Path.Combine(GetPath(user, subPath), fileName);
            await File.WriteAllBytesAsync(p, data);
        }

        // Đọc file (Trả về mảng byte)
        public async Task<byte[]?> ReadFile(string user, string filePath)
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

        // Đổi tên hoặc di chuyển trong cùng thư mục của user
        public void Rename(string user, string oldSubPath, string newName)
        {
            string oldPath = GetPath(user, oldSubPath);
            string parentDir = Path.GetDirectoryName(oldPath) ?? "";
            string newPath = Path.Combine(parentDir, newName);

            // Kiểm tra bảo mật cho đường dẫn mới
            string fullNewPath = Path.GetFullPath(newPath);
            string rootFullPath = Path.GetFullPath(Path.Combine(_root, user));
            if (!fullNewPath.StartsWith(rootFullPath))
            {
                throw new UnauthorizedAccessException("Không thể đổi tên ra ngoài thư mục của bạn!");
            }

            if (File.Exists(oldPath)) File.Move(oldPath, newPath);
            else if (Directory.Exists(oldPath)) Directory.Move(oldPath, newPath);
        }
    }
}
