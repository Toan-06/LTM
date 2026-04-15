using System.Net.Sockets;
using System.Text;

namespace Client.Services
{
    // PHẦN CỦA NGƯỜI 1: CÔNG CỤ KẾT NỐI MẠNG
    // Lớp này dùng để gửi tin nhắn lên Server và nhận kết quả trả về
    public static class SocketService
    {
        public static string ServerIP = "127.0.0.1"; // Mặc định là localhost, có thể đổi khi chạy
        public static int Port = 12345;

        public static async Task<string> Send(string ip, string message)
        {
            try
            {
                using var client = new TcpClient(ip, Port);
                using var stream = client.GetStream();
                
                var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                var reader = new StreamReader(stream, Encoding.UTF8);

                // Gửi thông điệp (Ví dụ: "LOGIN|toan|123")
                await writer.WriteLineAsync(message);

                // Đợi và nhận câu trả lời từ Server
                return await reader.ReadLineAsync() ?? "ERROR|Không nhận được phản hồi";
            }
            catch (Exception ex)
            {
                return $"ERROR|Lỗi kết nối: {ex.Message}";
            }
        }
    }
}
