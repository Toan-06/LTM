// Mở kết nối mạng http
using System;
using System.Net.Http;
// Khởi tạo quy chuẩn Task luồng đa tầng
using System.Threading.Tasks;

// ===== BẮT BUỘC YÊU CẦU CHO GIANG (Người 3) TẠI ĐÂY =====
// Giang phải vào Nuget tải thư viện Newtonsoft.Json. Xong tháo ngâm 3 cái comment dưới đây ra để xài. 
// using Newtonsoft.Json; 
// using System.Text;
// using System.Net.Http.Headers;

namespace Client.Services
{
    // Cầu nối giao thức mạng tĩnh.
    public class ApiClient
    {
        // Khởi tạo một cái đường ống truyền mạng tĩnh (_httpClient). Liên kết trỏ thẳng tới BaseAddress gốc là MÁY CHỦ BÊN KIA (cổng 5000 API).
        private static readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000/api/") };
        
        // Khởi tạo két sắt lưu lại CHÌA KHÓA BẢI MẬT mạng. Biến này khai báo public tĩnh để nhét khóa JWT sau khi đăng nhập thành công.
        public static string JWT_Token { get; set; } 

        // Khai báo kiểu chạy Task bool Async nghĩa là: Hàm này chạy ngầm không giật lag mạng. Trả về đúng/sai báo kết quả giao phối với Server.
        public static async Task<bool> LoginAsync(string username, string password)
        {
            // ===== TODO (Giang - Người 3): API GÓI TIN ĐĂNG NHẬP =====
            /*
             * Giang xem kỹ nhé:
             * - Tạo cục Object Json C# { Username = username... }
             * - Ép vô StringContent. Mã hóa UTF8. Label là "Application/json".
             * - Ném PostAsync qua thư mục auth/login
             * - Níu Status Code IS_OK: Xé cái Response Body Json ra làm mảnh động. Trượt bóc lá dán mang tên Token dính vào biến tĩnh JWT_Token ở trên kia!
             * - Trét token vào cờ Authorization: _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", biến_token);
             * Gợi ý API: Newtonsoft JsonConvert.SerializeObject và Deserialize.
             */
            return false;
        }

        // Cũng là Task nhưng lần này là bốc Mảng Danh Sách <array string> xuống WinForms.
        public static async Task<string[]> GetFilesAsync(string path = "")
        {
            // ===== TODO (Giang - Người 3): API ĐỌC JSON ARRAY =====
            /* Nhấp nhảy hàm GetAsync() ném path vào. Lôi ngực Content ra và đè Json DeserializeObject với kiểu <string[]> nhé. Nhớ Try Catch không bể dĩa. */
            return new string[0];
        }

        // ===== TODO (Giang - Người 3): CÁC HÀM TẢI LÊN/XUỐNG THEO GIÁO TRÌNH LƯU TRỮ =====
        /* Giang tự đẻ nốt hàm Download StreamAsync (nhận luồng chảy vô máy) và Upload MultipartAsync (bọc hộp data gửi máy chủ) nhé. Chịu khó đọc Doc và search Google các thư viện MultipartFormDataContent và ReadAsStreamAsync */
    }
}
