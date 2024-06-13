using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ChatAPI.Models;

namespace ChatServer
{
    public class TestLoginModel : ILoginModel
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
    }


    public static class AuthorizationApi
    {
        public static async Task<IResult> Login(TestLoginModel user)
        {
            var loginData = CreateTestLoginData();

            TestLoginModel? authorizedUser = loginData.FirstOrDefault(u => u.Login ==
                user.Login && u.Password == user.Password);

            if (authorizedUser is null)
                return Results.Unauthorized();

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, authorizedUser.Login) };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials:
                        new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = authorizedUser.Login
            };

            return Results.Json(response);
        }

        static List<TestLoginModel> CreateTestLoginData()
        {
            return new List<TestLoginModel>()
            {
                new TestLoginModel() { Login = "user1", Password = "user1"},
                new TestLoginModel() { Login = "user2", Password = "user2"},
            };
        }
    }
}
