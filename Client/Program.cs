using System;
using System.Windows.Forms;
using Client.Forms;

namespace Client
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Khởi chạy hệ thống từ màn hình chính (Đã tích hợp đăng nhập)
            Application.Run(new Form_Main());
        }
    }
}
