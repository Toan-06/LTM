using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Client.Services;
using Client.Controllers;
using Microsoft.VisualBasic;

namespace Client.Forms
{
    public partial class Form_Main : Form
    {
        // ==========================================
        // BỘ KHUNG & KẾT NỐI
        // ==========================================
        private string currentUser = "";
        private string currentPath = "/";
        private readonly ClientControllers _controllers = new ClientControllers();

        public Form_Main()
        {
            InitializeComponent();
        }

        // Bạn (Người 1) đã làm sẵn logic Đăng nhập/Đăng ký để nhóm có thể kết nối được Server
        private async void BtnLogin_Socket_Click(object sender, EventArgs e)
        {
            // Truyền txtIP.Text vào hàm Send để kết nối động theo đúng yêu cầu
            string res = await _controllers.LoginAsync(txtIP.Text, txtUser.Text, txtPass.Text);

            if (res.StartsWith("LOGIN_OK"))
            {
                currentUser = res.Split('|')[1];
                MessageBox.Show($"Đã kết nối! Chào mừng {currentUser}.");
                await LoadFiles(currentPath); // Gọi hàm tải file
            }
            else MessageBox.Show("Lỗi: " + res);
        }

        private async void BtnRegister_Socket_Click(object sender, EventArgs e)
        {
            string res = await _controllers.RegisterAsync(txtIP.Text, txtUser.Text, txtPass.Text);
            MessageBox.Show(res);
        }

        // ==========================================
        // LOGIC XỬ LÝ DỮ LIỆU
        // ==========================================

        // List file

        private async Task LoadFiles(string path)
        {
            if (string.IsNullOrEmpty(currentUser))
            {
                MessageBox.Show("Vui lòng đăng nhập trước!");
                return;
            }

            currentPath = path ?? "/";
            txtAddressBar.Text = currentPath;

            try
            {
                string[] files = await _controllers.ListFilesAsync(txtIP.Text, currentUser, currentPath);
                listViewFiles.Items.Clear();

                foreach (string file in files)
                    listViewFiles.Items.Add(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách: {ex.Message}");
            }
        }

        // MKDIR - Tạo thư mục

        private async void BtnNewFolder_Click(object sender, EventArgs e)
        {
            string folderName = Interaction.InputBox("Tên thư mục:", "Tạo mới");
            if (string.IsNullOrEmpty(folderName)) return;

            string res = await _controllers.CreateFolderAsync(txtIP.Text, currentUser, currentPath, folderName);
            MessageBox.Show(res);
            await LoadFiles(currentPath);
        }

        // UPLOAD

        private async void BtnUpload_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                var (filename, data) = await _controllers.PrepareUpload(dlg.FileName);

                string res = await _controllers.UploadAsync(txtIP.Text, currentUser, currentPath, filename, data);
                MessageBox.Show(res);
                await LoadFiles(currentPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi upload: {ex.Message}");
            }
        }

        // DELETE

        private async void MenuDelete_Click(object sender, EventArgs e)
        {
            if (listViewFiles.SelectedItems.Count == 0)
            {
                MessageBox.Show("Chọn file/thư mục!");
                return;
            }

            string fullItemName = listViewFiles.SelectedItems[0].Text;
            string filename = _controllers.ExtractName(fullItemName);
            string fullPath = _controllers.BuildPath(currentPath, filename);

            var confirm = MessageBox.Show($"Xóa '{fullItemName}'?", "Xác nhận", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                string res = await _controllers.DeleteAsync(txtIP.Text, currentUser, fullPath);
                MessageBox.Show(res);
                await LoadFiles(currentPath);
            }
        }

        // DOWNLOAD

        private async void MenuDownload_Click(object sender, EventArgs e)
        {
            if (listViewFiles.SelectedItems.Count == 0)
            {
                MessageBox.Show("Chọn file để tải!");
                return;
            }

            string fullItemName = listViewFiles.SelectedItems[0].Text;
            if (!fullItemName.StartsWith("[F] "))
            {
                MessageBox.Show("Chỉ tải được file!");
                return;
            }

            // Xử lý tên qua Controller
            string filename = _controllers.ExtractName(fullItemName);

            using SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = filename;

            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                string fullPath = _controllers.BuildPath(currentPath, filename);

                byte[] data = await _controllers.DownloadAsync(txtIP.Text, currentUser, fullPath);

                if (data != null)
                {
                    // Ghi file qua Controller
                    await _controllers.SaveFile(dlg.FileName, data);
                    MessageBox.Show($"Tải thành công!\n{data.Length} bytes");
                }
                else
                {
                    MessageBox.Show("File rỗng hoặc không tồn tại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải file:\n{ex.Message}");
            }
        }

           /** 

        // ==========================================
        // GIAO DIỆN & TƯƠNG TÁC
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
    **/