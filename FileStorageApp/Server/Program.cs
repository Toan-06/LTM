// Nạp thư viện khởi tạo ứng dụng Web/API
using Microsoft.AspNetCore.Builder;
// Nạp thư viện rổ tiêm phụ thuộc (Dependency Injection)
using Microsoft.Extensions.DependencyInjection;
// Nạp thư viện môi trường máy chủ
using Microsoft.Extensions.Hosting;

// Khởi chạy nền móng Builder để cất cấu trúc ứng dụng C# Server
var builder = WebApplication.CreateBuilder(args);

// ===== TODO (Toàn - Người 1): ĐĂNG KÝ DEPENDENCY INJECTION =====
/*
 * Mục tiêu:
 * - Dưới không gian dòng chữ này, mọi Dịch vụ (DB, File, Security) phải được đăng ký vào rổ hệ thống (builder.Services).
 * - Nếu Toàn quên đăng ký ở đây, qua bên Controller gọi ra xài máy sẽ lập tức ném lỗi ngoại lệ Trắng Màn Hình.
 * 
 * Hướng làm:
 * - Bật các Controller: builder.Services.AddControllers();
 * - Khởi tạo Database: builder.Services.AddDbContext<AppDbContext>(...dùng Sqlite...);
 * - Đăng ký Service File vật lý: builder.Services.AddScoped<IFileStorageService, FileStorageService>();
 * - (Nâng cao) Cấu hình JWT: builder.Services.AddAuthentication() và AddJwtBearer().
 */

// Đóng máy ép, trộn toàn bộ biến Services phía trên thành một Máy chủ app có thể chạy được
var app = builder.Build();

// Nếu máy đang ở chế độ Code gỡ lỗi của Dev thì kích hoạt tính năng hỗ trợ nội bộ.
if (app.Environment.IsDevelopment())
{
    // Bật Swagger nếu có cài
}

// Bắt buộc đường truyền bảo mật HTTPS để chống bị nghe trộm gói tin mạng
app.UseHttpsRedirection();

// ===== TODO (Toàn - Người 1): ĐĂNG KÝ MIDDLEWARE BẢO MẬT =====
/*
 * Mục tiêu:
 * - Mọi lệnh API có thẻ [Authorize] sẽ quét thẻ từ dòng này, nếu thiếu dòng này App sẽ ném lỗi 500 hoặc bỏ qua bảo mật.
 * 
 * Hướng làm:
 * - Chèn 2 dòng này ĐÚNG THỨ TỰ (Authentication đứng trước):
 *   + app.UseAuthentication();
 *   + app.UseAuthorization();
 * - Kích hoạt định tuyến tới thư mục Controller: app.MapControllers();
 */

// Nhấn nút RUN khởi chạy nổ máy chủ tại Localhost. Server chính thức nghe tín hiệu và hoạt động không ngủ.
app.Run();
