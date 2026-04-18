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
            try
            {
                progressBar1.Visible = true;
                string res = await _controllers.LoginAsync(txtIP.Text, txtUser.Text, txtPass.Text);

                if (res.StartsWith("LOGIN_OK"))
                {
                    currentUser = res.Split('|')[1];
                    panelLogin.Visible = false; // [HOÀN THIỆN] Ẩn bảng đăng nhập cho đẹp
                    btnLogout.Visible = true;   // Hiện nút Đăng xuất
                    btnLogout.BringToFront();   // Đảm bảo hiển thị trên cùng
                    MessageBox.Show($"Đã kết nối! Chào mừng {currentUser}.");
                    await LoadFiles(currentPath);
                }
                else MessageBox.Show("Lỗi: " + res);
            }
            finally { progressBar1.Visible = false; }
        }

        private async void BtnRegister_Socket_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Visible = true;
                string res = await _controllers.RegisterAsync(txtIP.Text, txtUser.Text, txtPass.Text);
                MessageBox.Show(res);
            }
            finally { progressBar1.Visible = false; }
        }

        // ==========================================
        // LOGIC XỬ LÝ DỮ LIỆU
        // ==========================================

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            currentUser = "";
            currentPath = "/";
            listViewFiles.Items.Clear();
            txtAddressBar.Text = "/";
            btnLogout.Visible = false; // Ẩn nút Đăng xuất
            panelLogin.Visible = true; // Hiện lại bảng đăng nhập
            MessageBox.Show("Đã đăng xuất thành công!");
        }

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

                foreach (string fileInfo in files)
                {
                    if (string.IsNullOrEmpty(fileInfo)) continue;

                    string[] parts = fileInfo.Split('?');

                    if (parts.Length > 0)
                    {
                        var item = new ListViewItem(parts[0]); // Cột Tên
                        if (parts.Length >= 2) item.SubItems.Add(parts[1]); // Cột Kích thước
                        if (parts.Length >= 3) item.SubItems.Add(parts[2]); // Cột Ngày sửa đổi

                        listViewFiles.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách: {ex.Message}");
            }
        }

        // MKDIR - Tạo thư mục

        private async void BtnNewFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentUser))
            {
                MessageBox.Show("Vui lòng đăng nhập để sử dụng tính năng này!");
                return;
            }

            string folderName = Interaction.InputBox("Tên thư mục:", "Tạo mới");
            if (string.IsNullOrEmpty(folderName)) return;

            string res = await _controllers.CreateFolderAsync(txtIP.Text, currentUser, currentPath, folderName);
            MessageBox.Show(res);
            await LoadFiles(currentPath);
        }

        // UPLOAD

        private async void BtnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentUser))
            {
                MessageBox.Show("Vui lòng đăng nhập trước khi tải tập tin!");
                return;
            }

            using OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                progressBar1.Visible = true; // [KẾT NỐI] Hiện thanh % của Giang
                var (filename, data) = await _controllers.PrepareUpload(dlg.FileName);

                string res = await _controllers.UploadAsync(txtIP.Text, currentUser, currentPath, filename, data);
                MessageBox.Show(res);
                await LoadFiles(currentPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi upload: {ex.Message}");
            }
            finally
            {
                progressBar1.Visible = false; // [KẾT NỐI] Ẩn thanh % khi xong
            }
        }

        // DELETE

        private async void MenuDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentUser))
            {
                MessageBox.Show("Bạn cần đăng nhập để thực hiện xóa!");
                return;
            }

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

        // RENAME

        private async void MenuRename_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentUser))
            {
                MessageBox.Show("Vui lòng đăng nhập để đổi tên!");
                return;
            }

            if (listViewFiles.SelectedItems.Count == 0)
            {
                MessageBox.Show("Chọn file/thư mục để đổi tên!");
                return;
            }

            string fullItemName = listViewFiles.SelectedItems[0].Text;
            string oldName = _controllers.ExtractName(fullItemName);
            string oldPath = _controllers.BuildPath(currentPath, oldName);

            string newName = Interaction.InputBox($"Đổi tên '{oldName}' thành:", "Cập nhật", oldName);
            if (string.IsNullOrEmpty(newName) || newName == oldName) return;

            try
            {
                string res = await _controllers.RenameAsync(txtIP.Text, currentUser, oldPath, newName);
                MessageBox.Show(res);
                await LoadFiles(currentPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đổi tên:\n{ex.Message}");
            }
        }

        // DOWNLOAD

        private async void MenuDownload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentUser))
            {
                MessageBox.Show("Vui lòng đăng nhập để tải xuống!");
                return;
            }

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

            string filename = _controllers.ExtractName(fullItemName);

            using SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = filename;

            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                progressBar1.Visible = true; // [KẾT NỐI] Hiện thanh %
                string fullPath = _controllers.BuildPath(currentPath, filename);

                byte[]? data = await _controllers.DownloadAsync(txtIP.Text, currentUser, fullPath);

                if (data != null)
                {
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
            finally
            {
                progressBar1.Visible = false; // [KẾT NỐI] Ẩn thanh %
            }
        }

        // ==========================================
        // GIAO DIỆN & TƯƠNG TÁC
        // ==========================================

        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            await LoadFiles(currentPath);
        }

        private async void BtnBack_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentPath) || currentPath == "/")
                return;

            try
            {
                // [SỬA LỖI] Logic quay lại chuẩn xác hơn
                string trimPath = currentPath.TrimEnd('/');
                int lastIndex = trimPath.LastIndexOf('/');

                if (lastIndex >= 0)
                {
                    currentPath = trimPath.Substring(0, lastIndex);
                    if (string.IsNullOrEmpty(currentPath)) currentPath = "/";
                }
                else
                {
                    currentPath = "/";
                }

                await LoadFiles(currentPath);
            }
            catch { await LoadFiles("/"); }
        }

        private async void ListViewFiles_DoubleClick(object sender, EventArgs e)
        {
            if (listViewFiles.SelectedItems.Count == 0) return;

            string selectedItem = listViewFiles.SelectedItems[0].Text;

            // Tránh việc ghép tên file nếu nó không phải là thư mục (giả sử file bắt đầu với [F])
            if (selectedItem.StartsWith("[F] ")) return;

            string folderName = _controllers.ExtractName(selectedItem);
            currentPath = _controllers.BuildPath(currentPath, folderName);

            await LoadFiles(currentPath);
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}