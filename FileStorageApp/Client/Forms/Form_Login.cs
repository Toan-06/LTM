// Chạm tương tác Form chuẩn Win C#
using System;
using System.Windows.Forms;

namespace Client.Forms
{
    // Giao diện Khởi thủy của App
    public partial class Form_Login : Form
    {
        // Hàm bắt đầu kích quạt kết nối File giao diện (.Designer) vào Code xử lý
        public Form_Login()
        {
            InitializeComponent();
        }

        // Bắt sóng sự kiện nhấp đúp nút Đăng Nhập ở Giao Diện
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            // ===== TODO (Trang - Người 2): HIỆU ỨNG LUỒNG NETWORK MÀN VIEW =====
            /*
             * Trang ơi, nhớ đoạn này:
             * - Thêm chữ `async` vào thay chỗ void ở trên. Vì hàm của anh Giang là Hàm Bất Đồng Bộ (Task).
             * - Gọi `var kq = await ApiClient.LoginAsync(hút từ Textbox username, hút từ Pass)`.
             * - Code if: Chặn rỗng MessageBox thông báo.
             * - Lệnh Ẩn form này nếu thành công: this.Hide();
             * - Lệnh Mở hộp Form Mới: new Form_Main().ShowDialog(); (Treo ở đây)
             * - Lệnh Đóng sập App triệt để: this.Close(); (Thoát hộp mới là Form này cũng đi theo luôn).
             */
        }

        // Bắt sóng sự kiện Đăng Ký nút bấm
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // ===== TODO (Trang - Người 2): Nút này dùng hàm RegistryAsync của nhóm Mạng =====
        }
    }
}
