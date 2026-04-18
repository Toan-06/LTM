using Server.Data;
using Server.Services;
using Server.Models;
using BCrypt.Net;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    // LOGIC SERVER (BẠN - NGƯỜI 1 QUẢN LÝ)
    // Đây là nơi xử lý toàn bộ yêu cầu từ Client gửi lên
    public class AuthContext
    {
        public string? Username { get; set; }
    }

    public static class CommandHandler
    {

        public static async Task<string> Handle(string req, AuthContext context)
        {
            try
            {
                // TẠO MỚI MỖI LẦN - KHÔNG STATIC
                using var db = new AppDbContext();
                var fileService = new FileService();

                string[] p = req.Split('|');
                string cmd = p[0].ToUpper();

                // KIỂM TRA QUYỀN TRUY CẬP (SESSION)
                if (cmd != "LOGIN" && cmd != "REGISTER")
                {
                    if (string.IsNullOrEmpty(context.Username))
                        return "ERROR|Chưa đăng nhập. Vui lòng kết nối và đăng nhập trước!";

                    // Bảo mật: Đảm bảo user gửi trong lệnh khớp với session đã đăng nhập
                    if (p.Length > 1 && p[1] != context.Username)
                        return "ERROR|Yêu cầu không hợp lệ. Bạn không thể thực hiện lệnh cho tài khoản khác!";
                }

                string response = cmd switch
                {
                    "REGISTER" => p.Length == 3 ? Register(db, fileService, p[1], p[2]) : "ERROR|Thiếu thông tin đăng ký",
                    "LOGIN" => p.Length == 3 ? Login(db, p[1], p[2]) : "ERROR|Thiếu thông tin đăng nhập",
                    "LIST" => p.Length >= 3 ? List(fileService, p[1], p[2]) : "ERROR|Thiếu tên user",
                    "MKDIR" => p.Length == 4 ? CreateFolder(fileService, p[1], p[2], p[3]) : "ERROR|Thiếu tên thư mục",
                    "DELETE" => p.Length >= 3 ? Delete(fileService, p[1], p[2]) : "ERROR|Thiếu đường dẫn xóa",
                    "UPLOAD" => p.Length == 5 ? await Upload(fileService, p[1], p[2], p[3], p[4]) : "ERROR|Thiếu dữ liệu file",
                    "DOWNLOAD" => p.Length >= 3 ? await Download(fileService, p[1], p[2]) : "ERROR|Thiếu đường dẫn tải",
                    "RENAME" => p.Length == 4 ? Rename(fileService, p[1], p[2], p[3]) : "ERROR|Thiếu tên mới",
                    _ => "ERROR|Lệnh không hợp lệ"
                };

                // Cập nhật session nếu đăng nhập/đăng ký thành công
                if (response.StartsWith("LOGIN_OK|") || response.StartsWith("SUCCESS|Đăng ký tài khoản thành công!"))
                {
                    context.Username = p[1];
                }

                return response;
            }
            catch (Exception ex)
            {
                return $"ERROR|Xử lý lỗi: {ex.Message}";
            }
        }

        // 1. Đăng ký (Tạo User + Tạo Thư mục riêng)
        private static string Register(AppDbContext db, FileService fileService, string user, string pass)
        {
            if (db.Users.Any(u => u.Username == user)) return "ERROR|Tài khoản đã tồn tại";
            
            db.Users.Add(new User { Username = user, PasswordHash = BCrypt.Net.BCrypt.HashPassword(pass) });
            db.SaveChanges();
            fileService.CreateDir(user); // Cấp thư mục riêng
            return "SUCCESS|Đăng ký tài khoản thành công!";
        }

        // 2. Đăng nhập
        private static string Login(AppDbContext db, string user, string pass)
        {
            var u = db.Users.FirstOrDefault(x => x.Username == user);
            if (u == null || !BCrypt.Net.BCrypt.Verify(pass, u.PasswordHash)) return "ERROR|Sai tài khoản hoặc mật khẩu";
            
            return $"LOGIN_OK|{u.Username}";
        }

        // 3. Liệt kê File (Phân quyền: luôn trong thư mục của user)
        private static string List(FileService fileService, string user, string path)
        {
            var items = fileService.List(user, path);
            return "LIST_OK|" + string.Join(",", items);
        }

        // 4. Tạo thư mục con
        private static string CreateFolder(FileService fileService, string user, string path, string folderName)
        {
            fileService.CreateDir(user, Path.Combine(path, folderName));
            return "SUCCESS|Đã tạo thư mục con thành công!";
        }

        // 5. Tải file lên (Dùng Base64 cho đơn giản bài LTM)
        private static async Task<string> Upload(FileService fileService, string user, string path, string fileName, string base64Data)
        {
            byte[] data = Convert.FromBase64String(base64Data);
            await fileService.SaveFile(user, path, fileName, data);
            return "SUCCESS|Đã tải file lên thành công!";
        }

        // 6. Tải file về
        private static async Task<string> Download(FileService fileService, string user, string path)
        {
            byte[]? data = await fileService.ReadFile(user, path);
            if (data == null) return "ERROR|File không tồn tại";
            return "DOWNLOAD_OK|" + Convert.ToBase64String(data);
        }

        // 7. Xóa file/thư mục
        private static string Delete(FileService fileService, string user, string path)
        {
            fileService.Delete(user, path);
            return "SUCCESS|Đã xóa thành công!";
        }

        // 8. Đổi tên file/thư mục
        private static string Rename(FileService fileService, string user, string path, string newName)
        {
            fileService.Rename(user, path, newName);
            return "SUCCESS|Đã đổi tên thành công!";
        }
    }
}
