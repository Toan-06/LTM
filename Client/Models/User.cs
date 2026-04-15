namespace Client.Models
{
    public class User
    {
        public string Username { get; set; } = "";
        public bool IsLoggedIn { get; set; }
        public string CurrentPath { get; set; } = "/";
    }
}
