namespace Client.Forms
{
    partial class Form_Main
    {
        /// <summary>
        ///  Biến bắt buộc dành cho giao diện Designer.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // LIÊN KẾT NHÂN SỰ: Các biến Thành Phần Giao Diện. (Cấm dọn dẹp)
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.TextBox txtAddressBar;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnNewFolder;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuDownload;
        private System.Windows.Forms.ToolStripMenuItem menuDelete;
        private System.Windows.Forms.ProgressBar progressBar1;

        /// <summary>
        ///  Dọn dẹp các tài nguyên tĩnh đang sử dụng.
        /// </summary>
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
        ///  Phương thức bắt buộc để hỗ trợ giao diện Designer - Đừng tự tay chỉnh sửa nội dung bên trong.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            // Khởi tạo đồ hoạ Đăng nhập
            this.panelLogin = new System.Windows.Forms.Panel();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();

            // Khởi tạo đồ hoạ File
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colSize = new System.Windows.Forms.ColumnHeader();
            this.colDate = new System.Windows.Forms.ColumnHeader();
            this.txtAddressBar = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnNewFolder = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            
            this.panelLogin.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();

            // Cấu hình Panel Đăng nhập (Nằm ở trên đầu)
            this.panelLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelLogin.Controls.Add(this.txtIP);
            this.panelLogin.Controls.Add(this.txtUser);
            this.panelLogin.Controls.Add(this.txtPass);
            this.panelLogin.Controls.Add(this.btnLogin);
            this.panelLogin.Controls.Add(this.btnRegister);
            this.panelLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogin.Location = new System.Drawing.Point(0, 0);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(784, 50);
            
            this.txtIP.Text = "127.0.0.1";
            this.txtIP.Location = new System.Drawing.Point(10, 15);
            this.txtIP.Size = new System.Drawing.Size(100, 23);
            
            this.txtUser.PlaceholderText = "Username";
            this.txtUser.Location = new System.Drawing.Point(120, 15);
            this.txtUser.Size = new System.Drawing.Size(120, 23);
            
            this.txtPass.PlaceholderText = "Password";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Location = new System.Drawing.Point(250, 15);
            this.txtPass.Size = new System.Drawing.Size(120, 23);

            this.btnLogin.Text = "Đăng Nhập";
            this.btnLogin.BackColor = System.Drawing.Color.DarkCyan;
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(380, 13);
            this.btnLogin.Size = new System.Drawing.Size(100, 28);
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Socket_Click);

            this.btnRegister.Text = "Đăng Ký";
            this.btnRegister.Location = new System.Drawing.Point(490, 13);
            this.btnRegister.Size = new System.Drawing.Size(100, 28);
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Socket_Click);

            // Cấu hình ListView và AddressBar (Dịch xuống dưới Panel)
            this.txtAddressBar.Location = new System.Drawing.Point(50, 60);
            this.txtAddressBar.Size = new System.Drawing.Size(400, 23);
            this.txtAddressBar.Text = "/";

            this.listViewFiles.Location = new System.Drawing.Point(10, 95);
            this.listViewFiles.Size = new System.Drawing.Size(764, 300);
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.colName, this.colSize, this.colDate });
            this.listViewFiles.DoubleClick += new System.EventHandler(this.ListViewFiles_DoubleClick);
            this.listViewFiles.ContextMenuStrip = this.contextMenuStrip;

            // Cấu hình Text và Size cho các cột để người dùng có thể xem được Data
            this.colName.Text = "Tên file/thư mục";
            this.colName.Width = 350;
            this.colSize.Text = "Kích thước";
            this.colSize.Width = 120;
            this.colDate.Text = "Ngày sửa đổi";
            this.colDate.Width = 200;

            this.btnBack.Location = new System.Drawing.Point(10, 58);
            this.btnBack.Size = new System.Drawing.Size(35, 25);
            this.btnBack.Text = "<";
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);

            this.btnRefresh.Location = new System.Drawing.Point(460, 58);
            this.btnRefresh.Size = new System.Drawing.Size(75, 25);
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);

            this.btnUpload.Text = "Tải lên";
            this.btnUpload.Click += new System.EventHandler(this.BtnUpload_Click);

            this.btnNewFolder.Location = new System.Drawing.Point(630, 58);
            this.btnNewFolder.Size = new System.Drawing.Size(95, 25);
            this.btnNewFolder.Text = "Thư mục mới";
            this.btnNewFolder.Click += new System.EventHandler(this.BtnNewFolder_Click);

            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Height = 20;
            this.progressBar1.Visible = false; // Mặc định ẩn, sẽ hiện khi tải file

            // Context Menu
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuDownload, this.menuDelete });
            this.menuDownload.Text = "Tải về";
            this.menuDownload.Click += new System.EventHandler(this.MenuDownload_Click);
            this.menuDelete.Text = "Xóa";
            this.menuDelete.Click += new System.EventHandler(this.MenuDelete_Click);

            // Bơm mọi thứ vào Form_Main
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.btnNewFolder);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.txtAddressBar);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.progressBar1);
            this.Name = "Form_Main";
            this.Text = "File Storage Client (LTM)";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
