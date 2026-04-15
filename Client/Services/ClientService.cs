using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Client.Services;

namespace Client.Services
{
    public static class ClientService
    {
        public static async Task<string> SendAsync(string ip, string command)
        {
            return await SocketService.Send(ip, command);
        }

        public static Task<string> RegisterAsync(string ip, string username, string password) =>
            SendAsync(ip, $"REGISTER|{username}|{password}");

        public static Task<string> LoginAsync(string ip, string username, string password) =>
            SendAsync(ip, $"LOGIN|{username}|{password}");

        public static async Task<string[]> ListFilesAsync(string ip, string username, string path)
        {
            string res = await SendAsync(ip, $"LIST|{username}|{path}");
            if (res.StartsWith("LIST_OK|"))
            {
                string data = res.Split('|')[1];
                return string.IsNullOrEmpty(data) ? Array.Empty<string>() : data.Split(',');
            }
            return new[] { res };
        }

        public static Task<string> CreateFolderAsync(string ip, string username, string path, string folderName) =>
            SendAsync(ip, $"MKDIR|{username}|{path}|{folderName}");

        public static Task<string> UploadAsync(string ip, string username, string path, string filename, byte[] data)
        {
            string base64 = Convert.ToBase64String(data);
            return SendAsync(ip, $"UPLOAD|{username}|{path}|{filename}|{base64}");
        }

        public static async Task<byte[]> DownloadAsync(string ip, string username, string path)
        {
            string res = await SendAsync(ip, $"DOWNLOAD|{username}|{path}");
            if (res.StartsWith("DOWNLOAD_OK|"))
            {
                try { return Convert.FromBase64String(res.Split('|')[1]); }
                catch { return null; }
            }
            return null;
        }

        public static Task<string> RenameAsync(string ip, string username, string path, string newName) =>
            SendAsync(ip, $"RENAME|{username}|{path}|{newName}");

        public static Task<string> DeleteAsync(string ip, string username, string path) =>
            SendAsync(ip, $"DELETE|{username}|{path}");
    }
}
