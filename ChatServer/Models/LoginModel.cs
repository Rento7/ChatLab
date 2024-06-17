using ChatAPI;

namespace ChatServer.Models
{
    public class LoginModel : ILoginModel
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
