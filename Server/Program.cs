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
            Console.WriteLine("Server đang chạy tại cổng 12345...");

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

            try
            {
                while (client.Connected)
                {
                    string? req = await reader.ReadLineAsync();
                    if (req == null) break;

                    Console.WriteLine($"Nhận: {req}");
                    string res = await CommandHandler.Handle(req);
                    await writer.WriteLineAsync(res);
                }
            }
            catch { }
            finally { client.Close(); }
        }
    }
}
