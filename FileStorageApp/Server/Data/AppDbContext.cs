// Khai báo thư viện EntityFrameworkCore dùng để thao tác với cơ sở dữ liệu SQLite
using Microsoft.EntityFrameworkCore;
// Nạp Models (chứa định nghĩa bảng User) vào để sử dụng
using Server.Models;

// Đặt tên vùng không gian mạng là Server.Data để các file khác dễ dàng tìm tới
namespace Server.Data
{
    // Lớp AppDbContext kế thừa DbContext (Khuôn mẫu CSDL gốc của Microsoft). Đây là TRÁI TIM của Database.
    public class AppDbContext : DbContext
    {
        // Hàm khởi tạo (Constructor): Nhận cấu hình từ Program.cs truyền vào (ví dụ: chuỗi đường dẫn tới file CSDL rDB.sqlite)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Khai báo dòng này để DBContext hiểu rằng: "À, hãy tạo cho tôi một Bảng (Table) tên là Users dựa trên thiết kế file User.cs"
        public DbSet<User> Users { get; set; }

        // Hàm này mặc định được gọi mồi khi Database bắt đầu khởi tạo cấu trúc cho các bảng
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Vẫn giữ lại gọi hàm lõi gốc của lớp cha DbContext để không bị mất chức năng mặc định
            base.OnModelCreating(modelBuilder);
            
            // ===== ĐÃ XONG: CONFIG DATABASE =====
            // Mục tiêu: Cột Username trong Table Users phải là duy nhất (UNIQUE)
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
