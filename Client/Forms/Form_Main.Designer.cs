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
        private System.Windows.Forms.ToolStripMenuItem menuRename;
        private System.Windows.Forms.Button btnLogout;
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
            components = new System.ComponentModel.Container();
            panelLogin = new Panel();
            txtIP = new TextBox();
            txtUser = new TextBox();
            txtPass = new TextBox();
            btnLogin = new Button();
            btnRegister = new Button();
            listViewFiles = new ListView();
            colName = new ColumnHeader();
            colSize = new ColumnHeader();
            colDate = new ColumnHeader();
            contextMenuStrip = new ContextMenuStrip(components);
            menuDownload = new ToolStripMenuItem();
            menuRename = new ToolStripMenuItem();
            menuDelete = new ToolStripMenuItem();
            txtAddressBar = new TextBox();
            btnBack = new Button();
            btnUpload = new Button();
            btnNewFolder = new Button();
            btnRefresh = new Button();
            btnLogout = new Button();
            progressBar1 = new ProgressBar();
            panelLogin.SuspendLayout();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // panelLogin
            // 
            panelLogin.BackColor = Color.FromArgb(45, 45, 48);
            panelLogin.Controls.Add(txtIP);
            panelLogin.Controls.Add(txtUser);
            panelLogin.Controls.Add(txtPass);
            panelLogin.Controls.Add(btnLogin);
            panelLogin.Controls.Add(btnRegister);
            panelLogin.Dock = DockStyle.Top;
            panelLogin.Location = new Point(0, 0);
            panelLogin.Margin = new Padding(3, 4, 3, 4);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(896, 67);
            panelLogin.TabIndex = 7;
            // 
            // txtIP
            // 
            txtIP.Location = new Point(26, 20);
            txtIP.Margin = new Padding(3, 4, 3, 4);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(114, 27);
            txtIP.TabIndex = 0;
            txtIP.Text = "127.0.0.1";
            // 
            // txtUser
            // 
            txtUser.Location = new Point(309, 19);
            txtUser.Margin = new Padding(3, 4, 3, 4);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "Username";
            txtUser.Size = new Size(137, 27);
            txtUser.TabIndex = 1;
            txtUser.TextChanged += txtUser_TextChanged;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(460, 19);
            txtPass.Margin = new Padding(3, 4, 3, 4);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '*';
            txtPass.PlaceholderText = "Password";
            txtPass.Size = new Size(137, 27);
            txtPass.TabIndex = 2;
            txtPass.TextChanged += txtPass_TextChanged;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.DarkCyan;
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(612, 15);
            btnLogin.Margin = new Padding(3, 4, 3, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(114, 37);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Đăng Nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Socket_Click;
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.DarkCyan;
            btnRegister.ForeColor = SystemColors.ControlLightLight;
            btnRegister.Location = new Point(741, 15);
            btnRegister.Margin = new Padding(3, 4, 3, 4);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(114, 37);
            btnRegister.TabIndex = 4;
            btnRegister.Text = "Đăng Ký";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += BtnRegister_Socket_Click;
            // 
            // listViewFiles
            // 
            listViewFiles.Columns.AddRange(new ColumnHeader[] { colName, colSize, colDate });
            listViewFiles.ContextMenuStrip = contextMenuStrip;
            listViewFiles.FullRowSelect = true;
            listViewFiles.Location = new Point(11, 127);
            listViewFiles.Margin = new Padding(3, 4, 3, 4);
            listViewFiles.Name = "listViewFiles";
            listViewFiles.Size = new Size(873, 399);
            listViewFiles.TabIndex = 1;
            listViewFiles.UseCompatibleStateImageBehavior = false;
            listViewFiles.View = View.Details;
            listViewFiles.DoubleClick += ListViewFiles_DoubleClick;
            // 
            // colName
            // 
            colName.Text = "Tên file/thư mục";
            colName.Width = 350;
            // 
            // colSize
            // 
            colSize.Text = "Kích thước";
            colSize.Width = 120;
            // 
            // colDate
            // 
            colDate.Text = "Ngày sửa đổi";
            colDate.Width = 200;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.ImageScalingSize = new Size(20, 20);
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { menuDownload, menuRename, menuDelete });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(128, 76);
            // 
            // menuDownload
            // 
            menuDownload.Name = "menuDownload";
            menuDownload.Size = new Size(127, 24);
            menuDownload.Text = "Tải về";
            menuDownload.Click += MenuDownload_Click;
            // 
            // menuRename
            // 
            menuRename.Name = "menuRename";
            menuRename.Size = new Size(127, 24);
            menuRename.Text = "Đổi tên";
            menuRename.Click += MenuRename_Click;
            // 
            // menuDelete
            // 
            menuDelete.Name = "menuDelete";
            menuDelete.Size = new Size(127, 24);
            menuDelete.Text = "Xóa";
            menuDelete.Click += MenuDelete_Click;
            // 
            // txtAddressBar
            // 
            txtAddressBar.Location = new Point(57, 80);
            txtAddressBar.Margin = new Padding(3, 4, 3, 4);
            txtAddressBar.Name = "txtAddressBar";
            txtAddressBar.Size = new Size(393, 27);
            txtAddressBar.TabIndex = 6;
            txtAddressBar.Text = "/";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(11, 77);
            btnBack.Margin = new Padding(3, 4, 3, 4);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(40, 33);
            btnBack.TabIndex = 5;
            btnBack.Text = "<";
            btnBack.Click += BtnBack_Click;
            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(548, 77);
            btnUpload.Margin = new Padding(3, 4, 3, 4);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(86, 33);
            btnUpload.TabIndex = 3;
            btnUpload.Text = "Tải lên";
            btnUpload.Click += BtnUpload_Click;
            // 
            // btnNewFolder
            // 
            btnNewFolder.Location = new Point(640, 77);
            btnNewFolder.Margin = new Padding(3, 4, 3, 4);
            btnNewFolder.Name = "btnNewFolder";
            btnNewFolder.Size = new Size(109, 33);
            btnNewFolder.TabIndex = 2;
            btnNewFolder.Text = "Thư mục mới";
            btnNewFolder.Click += BtnNewFolder_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(456, 77);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(86, 33);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Làm mới";
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.Red;
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(791, 527);
            btnLogout.Margin = new Padding(3, 4, 3, 4);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(95, 33);
            btnLogout.TabIndex = 9;
            btnLogout.Text = "Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Visible = false;
            btnLogout.Click += BtnLogout_Click;
            // 
            // progressBar1
            // 
            progressBar1.Dock = DockStyle.Bottom;
            progressBar1.Location = new Point(0, 561);
            progressBar1.Margin = new Padding(3, 4, 3, 4);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(896, 27);
            progressBar1.TabIndex = 8;
            progressBar1.Visible = false;
            // 
            // Form_Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(896, 588);
            Controls.Add(listViewFiles);
            Controls.Add(btnNewFolder);
            Controls.Add(btnUpload);
            Controls.Add(btnRefresh);
            Controls.Add(btnLogout);
            Controls.Add(btnBack);
            Controls.Add(txtAddressBar);
            Controls.Add(panelLogin);
            Controls.Add(progressBar1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form_Main";
            Text = "File Storage Client (LTM)";
            Load += Form_Main_Load;
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
