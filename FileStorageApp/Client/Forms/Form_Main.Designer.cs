namespace Client.Forms
{
    partial class Form_Main
    {
        /// <summary>
        ///  Biến bắt buộc dành cho giao diện Designer.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // LIÊN KẾT NHÂN SỰ: Các biến Thành Phần Giao Diện. (Cấm dọn dẹp)
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.TextBox txtAddressBar;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnUpload;
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
            // Khởi tạo đồ hoạ
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colSize = new System.Windows.Forms.ColumnHeader();
            this.colDate = new System.Windows.Forms.ColumnHeader();
            this.txtAddressBar = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            
            // ===== TODO (Giao Diện - Người 2): HƯỚNG DẪN CĂN BẢNG EXPLORER CHI TIẾT =====
            /*
             * Đây là trái tim của giao diện hiển thị dữ liệu Server.
             * 
             * GIAI ĐOẠN 1: MỞ CHẾ ĐỘ DESIGNER
             * - B1: Tới Solution Explorer bên phải (Cột Quản lý File). Mở file `Form_Main.cs` (Bằng cách đúp chuột vào cái Icon Cửa Sổ hình thoi bên cạnh nó).
             * - B2: Bảng vẽ Thiết kế sẽ hiện lên, kèm theo đống Nút Tên, Nút Back đang lộn xộn.
             * 
             * GIAI ĐOẠN 2: XẾP HÌNH! CẦM KÉO QUĂNG THẢ 
             * - B1: Ấn F4 để gọi bảng Properties ra.
             * - B2: Quăng cái ListView trắng (listViewFiles, cục to nhứt) ra nằm choán lấy 80% phần bụng bản vẽ. Ấn F4, kéo xuống kiếm dòng cờ `View`, Đổi cái mâm nó thành chữ `Details` (để nó chuyển thành Mảng Hiển Thị như trên File Explorer). Kiếm dòng `FullRowSelect` bằng True băm vào để được quyền đè vệt sáng xanh khi user nhấp chuột ngang.
             * - B3: Đẩy cục `progressBar1` nằm ngang rạp xuống mép mông đáy màn hình - để lát kéo File nó hiện phần trăm % tải xuống trườn cho mượt.
             * - B4: Mấy cục nút `btnUpload` lôi dọng lên thanh Header. Đổ mực Tím thủy chung (F4 -> BackColor) vào làm Cú Tạo Tác Căng. 
             * - B5: Ghì thanh Address Bar thả ở sát nách của Nút Cờ lê Lùi lại (Back).
             * 
             * GIAI ĐOẠN 3: LƯU TRỮ VÀ THĂNG HOA
             * Ctr+S ấn chốt sổ. Lệnh này triệu hồi cái Cày Ngầm VS găm vĩnh viễn Code Tọa độ vô màn hình của Người 2 mà không lòi chút cặn trên Github! Chúc UI của bạn hoành tráng!
             */
            
            this.txtAddressBar.Name = "txtAddressBar";
            this.txtAddressBar.Text = "/"; // Root path
            
            this.btnBack.Name = "btnBack";
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click); // LIÊN KẾT BACK
            
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click); // LIÊN KẾT REFRESH
            
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Text = "Upload";
            this.btnUpload.Click += new System.EventHandler(this.BtnUpload_Click); // LIÊN KẾT UPLOAD

            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.colName, this.colSize, this.colDate });
            this.listViewFiles.ContextMenuStrip = this.contextMenuStrip;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.DoubleClick += new System.EventHandler(this.ListViewFiles_DoubleClick); // LIÊN KẾT DOUBLE CLICK FOLDER
            
            // Các nút của Hộp thoại chuột phải
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuDownload, this.menuDelete });
            this.contextMenuStrip.Name = "contextMenuStrip";
            
            this.menuDownload.Name = "menuDownload";
            this.menuDownload.Text = "Tải về";
            this.menuDownload.Click += new System.EventHandler(this.MenuDownload_Click); // LIÊN KẾT TẢI
            
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Text = "Xóa";
            this.menuDelete.Click += new System.EventHandler(this.MenuDelete_Click); // LIÊN KẾT XÓA
            
            this.progressBar1.Name = "progressBar1";
            
            // Bơm mọi thứ vào Form_Main hiển thị
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.txtAddressBar);
            this.Name = "Form_Main";
            this.Text = "File Explorer Client";
            this.Load += new System.EventHandler(this.Form_Main_Load); // LIÊN KẾT KHI VỪA BẬT APP
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
