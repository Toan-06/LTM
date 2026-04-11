namespace Client.Forms
{
    partial class Form_Login
    {
        /// <summary>
        ///  Biến bắt buộc dành cho giao diện Designer.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // LIÊN KẾT NHÂN SỰ: Các biến Thành Phần Giao Diện. 
        // Phải giữ nguyên khai báo này để bên file Code Form_Login.cs có thể gọi tên được nó (như txtUsername).
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;

        /// <summary>
        ///  Dọn dẹp các tài nguyên tĩnh đang sử dụng trong máy để giải phóng Ram.
        /// </summary>
        /// <param name="disposing">true nếu cần kích hoạt tiến trình giải phóng dọn rác; ngược lại thì false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Mã nguồn do Windows Form Designer tự chèn tự động

        /// <summary>
        ///  Phương thức bắt buộc để hỗ trợ giao diện Designer - Đừng tự tay chỉnh sửa nội dung bên trong, hãy qua màn Hình kéo thả.
        /// </summary>
        private void InitializeComponent()
        {
            // BƯỚC 1: Liên kết Sinh Thành phần - Buộc giữ lại để App hiểu sự tồn tại của Nút bấm
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // ===== TODO (Giao Diện - Người 2): HƯỚNG DẪN CHI TIẾT CÁCH SẮP XẾP LẠI GIAO DIỆN =====
            /*
             * Do code canh lề đã bị xóa sạch để bạn tự do sáng tạo, hiện tại form này đang trống trơn hoặc các nút dồn lại một cục. 
             * Mời bạn làm đúng các bước Cầm tay chỉ việc sau:
             * 
             * GIAI ĐOẠN 1: MỞ KHUNG VẼ
             * - B1: Tìm sang cái bảng "Solution Explorer" dọc bên phải màn hình Visual Studio.
             * - B2: Tìm mở thư mục Client -> Forms.
             * - B3: Bạn sẽ thấy file tên là `Form_Login.cs` (Kế bên có icon hình cửa sổ win nhỏ xíu). Hãy CLICK ĐÚP CHUỘT trái vào cái icon hình cửa sổ đó.
             * - B4: Lúc này, 1 bản vẽ giao diện đồ hoạ sẽ hiện ra trên màn hình. Nếu hệ thống hỏi mở dưới dạng [Design], thì ấn chọn nó.
             * 
             * GIAI ĐOẠN 2: THAO TÁC CĂN CHỈNH BẰNG CHUỘT
             * - B1: Bấm phím `F4` trên bàn phím. Một cái Bảng dài sọc tên là "Properties" (Thuộc tính) sẽ hiện ra ở mé bên phải màn hình.
             * - B2: Trên bản vẽ, bạn sẽ thấy mấy cái Nút và chữ đang nằm dồn cục ở góc trên cùng bên trái. Dùng chuột ấn đè lên từng cái nút (ví dụ cái Nút Đăng Nhập) rồi cầm kéo nó ra giữa bản vẽ.
             * - B3: Vẫn đang bấm lấy cái Nút Đăng nhập đó, bạn nhìn sang bảng F4 bên kia, cuộn xuống dưới cùng tìm dòng chữ `Size` (Kích cỡ). Đổi số thành "150, 45". Khi enter tự dưng bạn sẽ thấy cái nút nó mập ú ra xinh đẹp.
             * - B4: Tiếp tục tìm dòng `BackColor` trong bảng F4, bấm vào dấu mũi tên, chọn cột Custom (Tùy chọn), pick lấy 1 màu Xanh Dương đậm thật đẹp. Tiếp tục tìm dòng `ForeColor` đổi thành màu Trắng (White) để nổi chữ. Thấy xịn chưa?
             * 
             * GIAI ĐOẠN 3: LÀM ĐẸP CHỮ VÀ Ô NHẬP LƯU
             * - Bạn cầm và lôi cái vùng gõ chữ (txtUsername, txtPassword) ra căn giữa màn hình phía trên 2 cái nút bấm. Vào bảng F4 cũng set lại Size (Chiều rộng tăng lên 250 chẳng hạn).
             * - Đối với cái "txtPassword" (vùng gõ Mật khẩu), bạn ấn vào nó, tìm dòng `PasswordChar` trên bảng F4, gõ dấu sao ( * ) vào đấy. Để lúc mật khẩu đánh vào nó bị giấu đi tránh bị nhìn trộm.
             * 
             * GIAI ĐOẠN 4: LƯU LẠI
             * - Ấn tổ hợp phím `Ctrl + S`. Ngay khi ấn Ctr S, thì Visual Studio sẽ lén lút ở sau lưng tự động ghi hết tọa độ X Y vừa tạo vào trong cái file Designer này. Thế là xong Layout!
             */
            
            // LIÊN KẾT: Gắn răn tên biến (Name) và Móc Nối sự kiện Click liên kết với hàm Code bên kia. (Cấm xóa)
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Text = "Đăng Nhập";
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Text = "Đăng Ký";
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            
            this.txtPassword.Name = "txtPassword";
            this.txtUsername.Name = "txtUsername";

            // BƯỚC CUỐI: Liên kết Bơm Control vào Bảng vẽ
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 311);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblTitle);
            this.Name = "Form_Login";
            this.Text = "Đăng nhập Client";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
