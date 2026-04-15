using System.IO;
using System.Threading.Tasks;
using Client.Services;

namespace Client.Controllers
{
    public class ClientControllers
    {
        // ===== CÁC HÀM GỌI SERVICE =====
        public Task<string> RegisterAsync(string ip, string user, string pass) =>
            ClientService.RegisterAsync(ip, user, pass);

        public Task<string> LoginAsync(string ip, string user, string pass) =>
            ClientService.LoginAsync(ip, user, pass);

        public Task<string[]> ListFilesAsync(string ip, string user, string path) =>
            ClientService.ListFilesAsync(ip, user, path);

        public Task<string> CreateFolderAsync(string ip, string user, string path, string name) =>
            ClientService.CreateFolderAsync(ip, user, path, name);

        public Task<string> DeleteAsync(string ip, string user, string path) =>
            ClientService.DeleteAsync(ip, user, path);

        public Task<string> RenameAsync(string ip, string user, string path, string newName) =>
            ClientService.RenameAsync(ip, user, path, newName);

        public Task<string> UploadAsync(string ip, string user, string path, string filename, byte[] data) =>
            ClientService.UploadAsync(ip, user, path, filename, data);

        public Task<byte[]?> DownloadAsync(string ip, string user, string path) =>
            ClientService.DownloadAsync(ip, user, path);

        // ===== LOGIC MVC =====

        public string ExtractName(string item)
        {
            return item.Replace("[F] ", "").Replace("[D] ", "");
        }

        public string BuildPath(string currentPath, string name)
        {
            return currentPath == "/" ? name : $"{currentPath}/{name}";
        }

        public async Task<(string filename, byte[] data)> PrepareUpload(string filePath)
        {
            byte[] data = await File.ReadAllBytesAsync(filePath);
            string filename = Path.GetFileName(filePath);
            return (filename, data);
        }

        public async Task SaveFile(string savePath, byte[] data)
        {
            await File.WriteAllBytesAsync(savePath, data);
        }
    }
}
