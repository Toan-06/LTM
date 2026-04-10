using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Services
{
    public class ApiClient
    {
        // Người 3: Integration & UX Engineer - Cầu nối kết nối Server & Client
        // TODO: Cấu hình địa chỉ cơ sở (BaseAddress) trỏ tới Server
        private readonly HttpClient _httpClient;
        public static string JWT_Token { get; set; } // Giữ phiên đăng nhập toàn cục

        public ApiClient()
        {
            _httpClient = new HttpClient();
            // baseUrl: https://localhost:xxxx/
        }

        // TODO: Viết các hàm dùng GET/POST gọi đến Server và Deserialize JSON từ Server về Object Client
    }
}
