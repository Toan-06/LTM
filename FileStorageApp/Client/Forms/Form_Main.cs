// Chạm tương tác đồ hoạ Winforms
using System;
using System.Windows.Forms;

namespace Client.Forms
{
    // Bàn làm việc trung tâm
    public partial class Form_Main : Form
    {
        // Khởi tạo link vào UI
        public Form_Main()
        {
            InitializeComponent();
        }

        // Móc hàm Lifecycle mồi (Ngay khi Form Cửa sổ vừa mở lên xong, Hàm này sẽ mồi gọi bảng List hiển thị để khỏi phải F5)
        private void Form_Main_Load(object sender, EventArgs e)
        {
            LoadFiles(txtAddressBar.Text);
        }

        // Hàm helper dùng để vẽ lại cái Bảng Hiển Thị File
        private void LoadFiles(string path)
        {
            // ===== TODO (Trang - Người 2): TRẢ LỚP LAYER HIỂN THỊ =====
            /* Thêm chữ Async nhé Trang! Moi var mang_tra_ve = await ApiClient.GetFilesAsync của anh Giang làm. Chạy vòng lặp foreach ném từng cái vào ListViewItem. Đừng quên Cấm kị là phải Dọn sạch thẻ bằng listViewFiles.Items.Clear() trước khi ném data mới vào ko rác ngập ngụa cái bảng. */
        }

        // Móc Sự Kiện Back
        private void BtnBack_Click(object sender, EventArgs e)
        {
            // ===== TODO (Trang - Người 2): LOGIC CẮT CHUỖI NHẢY BẬC =====
            /* Dùng LastIndexOf('/') tóm cổ vị trí dấu xuyệt. Xài SubString lấy vạt chữ cái. Gán ngược vào thanh AddressBar và mồi lại tải data qua LoadFiles()*/
        }

        // Móc Sự kiện Nút Refresh
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            // Gọi tươi lại màn hình hiển thị theo giá trị ô text
            LoadFiles(txtAddressBar.Text);
        }

        // Móc sự kiện Nút Upload Nhấn Vô Cửa Sổ Tìm File
        private void BtnUpload_Click(object sender, EventArgs e)
        {
            // ===== TODO (Trang - Người 2): LOGIC HỘP THOẠI UPLOAD =====
            /*
             * Trang là UI, Trang phải mở Windows Popup: using var ofd = new OpenFileDialog();
             * Kích lệnh chặn if (ofd.ShowDialog() == DialogResult.OK)
             * Trích cái Tên File Vật lý nằm ở "ofd.FileName" để thảy sang Gọi hàm Upload (await) bên nhà Dịch Vụ Mạng (ApiClient). Thành công thì báo MessageBox thông báo tươi rồi mớm Refresh Loadfiles.
             */
        }

        // Móc sự kiện Mouse Click Double vào 1 ô dữ liệu trong Bảng
        private void ListViewFiles_DoubleClick(object sender, EventArgs e)
        {
            // ===== TODO (Trang - Người 2): LOGIC NHẤN KÉP FOLDER =====
            /* Lấy tên ô chọn nằm tại mảng: listViewFiles.SelectedItems[0].Text . Nối vào đuôi của AddressBar. Xong gọi LoadFile. Mượt! */
        }

        // Các hàm xử lý chuột Trái/Phải của Mạng Download và Delete.
        private void MenuDownload_Click(object sender, EventArgs e)
        {
            // ===== TODO (Trang - Người 2): HỘP THOẠI SAVE =====
            /* Trang xử hộp SaveFileDialog tương tự ở trên. Mớm cái FileName chốt vô API Download File ổng Giang ổng mần xong. */
        }

        private void MenuDelete_Click(object sender, EventArgs e)
        {
            // TODO (Trang - Người 2): Bắt được Tên trên Table -> quăng File HTTP delete sang cho Giang ăn rác.
        }
    }
}
