using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ChatServer
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";

        const string KEY = "XMf0pWPjpwvsodXph1EvjUKjdOsvNEiSct4gEnn71TBDVyIBplArwiP0EHv0CsYo8vNjetIPIYDpizIPLwj1UfNQW3Id02s1jR5V";

        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
             new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
