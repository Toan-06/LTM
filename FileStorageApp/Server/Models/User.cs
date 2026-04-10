namespace Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // Nhớ hash mật khẩu!
        public string StorageFolderName { get; set; } 
    }
}
