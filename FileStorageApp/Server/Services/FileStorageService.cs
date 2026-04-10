using System.IO;

namespace Server.Services
{
    public class FileStorageService
    {
        // TODO: Cấu hình thư mục gốc lưu trữ (C:\ServerStorage hoặc ở đây)
        private readonly string _rootStoragePath = "C:\\ServerStorage";

        // Giai đoạn 2: Trọng tâm chặn Path Traversal
        // TODO: Code hàm chuẩn hóa đường dẫn (Path.GetFullPath) để block ../
        
        // TODO: Xử lý các logic đọc/ghi/xóa/System.IO tại class này
    }
}
