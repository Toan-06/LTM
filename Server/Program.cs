using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static async Task Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            TcpListener server = new TcpListener(IPAddress.Any, 12345);
            server.Start();

            Console.WriteLine("=== SERVER FILE STORAGE (LTM) ===");
            
            // Lấy và hiển thị IP LAN để đồng đội kết nối
            var host = Dns.GetHostEntry(Dns.GetHostName());
            Console.WriteLine("Các địa chỉ IP khả dụng trên máy này:");
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine($"- LAN IP: {ip}");
                }
            }
            
            Console.WriteLine("Server đang chạy tại cổng 12345...");

            // Khởi tạo Database nếu chưa có
            using (var db = new Server.Data.AppDbContext())
            {
                Console.WriteLine("Đang kiểm tra cơ sở dữ liệu...");
                db.Database.EnsureCreated(); // Đã đổi sang EnsureCreated()
                Console.WriteLine("Cơ sở dữ liệu đã sẵn sàng!");
            }

            while (true)
            {
                var client = await server.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClient(client));
            }
        }

        static async Task HandleClient(TcpClient client)
        {
            using var stream = client.GetStream();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            var authContext = new CommandHandler.AuthContext(); // Mỗi kết nối có 1 Context riêng

            try
            {
                while (client.Connected)
                {
                    string? req = await reader.ReadLineAsync();
                    if (req == null) break;

                    string displayReq = req.Length > 100 ? req.Substring(0, 100) + "..." : req;
                    Console.WriteLine($"Nhận: {displayReq}");
                    string res = await CommandHandler.Handle(req, authContext);
                    await writer.WriteLineAsync(res);
                }
            }
            catch { }
            finally { client.Close(); }
        }
    }
}
