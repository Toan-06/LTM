namespace Client;

partial class Form1
{
    /// <summary>
    ///  Biến bắt buộc dành cho giao diện Designer.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Dọn dẹp các tài nguyên tĩnh đang sử dụng trong máy để giải phóng Ram.
    /// </summary>
    /// <param name="disposing">true nếu cần kích hoạt tiến trình giải phóng dọn rác; ngược lại thì false.</param>
    protected override void Dispose(bool disposing)
    {
        // Liên kết nội bộ rác bộ nhớ - GIỮ NGUYÊN
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Mã nguồn do Windows Form Designer tự chèn tự động

    /// <summary>
    ///  Phương thức bắt buộc để hỗ trợ giao diện Designer - Đừng tự tay chỉnh sửa nội dung bên trong bằng code gõ, hãy qua màn Hình kéo thả.
    /// </summary>
    private void InitializeComponent()
    {
        // Liên kết hệ thống tỷ lệ kích thước - Tự thiết kế ở Giao Diện
        components = new System.ComponentModel.Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Text = "Form1";
    }

    #endregion
}
