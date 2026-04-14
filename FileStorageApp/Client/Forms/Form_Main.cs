using System;
using System.Windows.Forms;
using Client.Services;

namespace Client.Forms
{
    public partial class Form_Main : Form
    {
        // ==========================================
        // KHU VỰC CỦA NGƯỜI 1 (BẠN): BỘ KHUNG & KẾT NỐI
        // ==========================================
        private string currentUser = ""; 
        private string currentPath = ""; 

        public Form_Main()
        {
            InitializeComponent();
        }

        // Bạn (Người 1) đã làm sẵn logic Đăng nhập/Đăng ký để nhóm có thể kết nối được Server
        private async void BtnLogin_Socket_Click(object sender, EventArgs e)
        {
            // Truyền txtIP.Text vào hàm Send để kết nối động theo đúng yêu cầu
            string res = await SocketService.Send(txtIP.Text, $"LOGIN|{txtUser.Text}|{txtPass.Text}");

            if (res.StartsWith("LOGIN_OK"))
            {
                currentUser = res.Split('|')[1];
                MessageBox.Show($"Đã kết nối! Chào mừng {currentUser}.");
                LoadFiles(""); // Gọi hàm tải file
            }
            else MessageBox.Show("Lỗi: " + res);
        }

        private async void BtnRegister_Socket_Click(object sender, EventArgs e)
        {
            // Truyền txtIP.Text vào hàm Send
            string res = await SocketService.Send(txtIP.Text, $"REGISTER|{txtUser.Text}|{txtPass.Text}");
            MessageBox.Show(res);
        }

        // ==========================================
        // KHU VỰC CỦA TRANG (NGƯỜI 2): LOGIC XỬ LÝ DỮ LIỆU
        // ==========================================
        
        private async void LoadFiles(string subPath)
        {
            // TODO (Trang): 
            // 1. Gọi lệnh LIST qua SocketService: SocketService.Send(txtIP.Text, $"LIST|{currentUser}|{subPath}")
            // 2. Tách chuỗi kết quả (Split '|') để lấy danh sách file.
            // 3. Xóa ListView cũ và dùng vòng lặp ném mảng file vào listViewFiles để hiển thị.
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            // TODO (Trang): 
            // 1. Sau khi Giang (Người 3) mở được hộp thoại chọn file, hãy đọc file thành mảng Byte.
            // 2. Chuyển mảng Byte thành chuỗi Base64: Convert.ToBase64String(bytes).
            // 3. Gửi lệnh UPLOAD lên Server: await SocketService.Send(txtIP.Text, $"UPLOAD|{currentUser}|{currentPath}|{fileName}|{base64Data}")
        }

        private void MenuDelete_Click(object sender, EventArgs e)
        {
            // TODO (Trang): 
            // 1. Lấy tên file đang chọn trên ListView.
            // 2. Gửi lệnh DELETE lên Server theo đúng định dạng qua SocketService.Send(txtIP.Text, ...)
        }

        // ==========================================
        // KHU VỰC CỦA GIANG (NGƯỜI 3): GIAO DIỆN & TƯƠNG TÁC
        // ==========================================

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            // TODO (Giang): Gọi lại hàm LoadFiles(currentPath) để làm mới màn hình.
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            // TODO (Giang): Xử lý cắt chuỗi currentPath để quay lại thư mục cha (Dùng LastIndexOf).
        }

        private void ListViewFiles_DoubleClick(object sender, EventArgs e)
        {
            // TODO (Giang): Lấy tên Item đang được click đúp, nối vào currentPath và gọi LoadFiles.
        }

        private void MenuDownload_Click(object sender, EventArgs e)
        {
            // TODO (Giang): Mở SaveFileDialog để người dùng chọn chỗ lưu file trên máy mình.
            // (Phần tải dữ liệu thật sẽ do Trang gọi API Download).
        }

        private void Form_Main_Load(object sender, EventArgs e) { }
    }
}
