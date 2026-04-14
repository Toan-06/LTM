namespace Server.Models
{
    // Model người dùng cực kỳ tối giản cho DB
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string PasswordHash { get; set; } = "";
    }

    // Class phụ để bóc tách dữ liệu từ Client gửi lên dạng chuỗi
    public class AuthRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
