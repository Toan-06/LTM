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
    public static class CommandHandler
    {
        private static readonly AppDbContext db = new AppDbContext();
        private static readonly FileService fileService = new FileService();

        public static async Task<string> Handle(string req)
        {
            try
            {
                // Quy tắc: COMMAND|User|Param1|Param2...
                string[] p = req.Split('|');
                string cmd = p[0].ToUpper();

                return cmd switch
                {
                    "REGISTER" => Register(p[1], p[2]),
                    "LOGIN"    => Login(p[1], p[2]),
                    "LIST"     => List(p[1], p[2]),
                    "MKDIR"    => CreateFolder(p[1], p[2], p[3]),
                    "DELETE"   => Delete(p[1], p[2]),
                    "UPLOAD"   => await Upload(p[1], p[2], p[3], p[4]), // user|path|name|base64
                    "DOWNLOAD" => await Download(p[1], p[2]),           // user|path
                    _          => "ERROR|Lệnh không hợp lệ"
                };
            }
            catch (Exception ex)
            {
                return $"ERROR|Lỗi Server: {ex.Message}";
            }
        }

        // 1. Đăng ký (Tạo User + Tạo Thư mục riêng)
        private static string Register(string user, string pass)
        {
            if (db.Users.Any(u => u.Username == user)) return "ERROR|Tài khoản đã tồn tại";
            
            db.Users.Add(new User { Username = user, PasswordHash = BCrypt.Net.BCrypt.HashPassword(pass) });
            db.SaveChanges();
            fileService.CreateDir(user); // Cấp thư mục riêng
            return "SUCCESS|Đăng ký tài khoản thành công!";
        }

        // 2. Đăng nhập
        private static string Login(string user, string pass)
        {
            var u = db.Users.FirstOrDefault(x => x.Username == user);
            if (u == null || !BCrypt.Net.BCrypt.Verify(pass, u.PasswordHash)) return "ERROR|Sai tài khoản hoặc mật khẩu";
            
            return $"LOGIN_OK|{u.Username}";
        }

        // 3. Liệt kê File (Phân quyền: luôn trong thư mục của user)
        private static string List(string user, string path)
        {
            var items = fileService.List(user, path);
            return "LIST_OK|" + string.Join(",", items);
        }

        // 4. Tạo thư mục con
        private static string CreateFolder(string user, string path, string folderName)
        {
            fileService.CreateDir(user, Path.Combine(path, folderName));
            return "SUCCESS|Đã tạo thư mục con thành công!";
        }

        // 5. Tải file lên (Dùng Base64 cho đơn giản bài LTM)
        private static async Task<string> Upload(string user, string path, string fileName, string base64Data)
        {
            byte[] data = Convert.FromBase64String(base64Data);
            await fileService.SaveFile(user, path, fileName, data);
            return "SUCCESS|Đã tải file lên thành công!";
        }

        // 6. Tải file về
        private static async Task<string> Download(string user, string path)
        {
            byte[] data = await fileService.ReadFile(user, path);
            if (data == null) return "ERROR|File không tồn tại";
            return "DOWNLOAD_OK|" + Convert.ToBase64String(data);
        }

        // 7. Xóa file/thư mục
        private static string Delete(string user, string path)
        {
            fileService.Delete(user, path);
            return "SUCCESS|Đã xóa thành công!";
        }
    }
}
