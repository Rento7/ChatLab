using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ChatDb;
using ChatServer.Models;
using ChatDb.Models;

namespace ChatServer
{
    public static class AuthorizationApi
    {
        public static async Task<IResult> Login(IChatRepository repository, LoginModel loginModel)
        {
            //TODO change
            var usres = await repository.GetUsersAsync();

            User user = usres.FirstOrDefault(u => u.Login == loginModel.Login && u.Password == loginModel.Password);

            if (user is null)
                return Results.Unauthorized();

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };

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
                username = user.Login,
                userId = user.Id
            };

            return Results.Json(response);
        }
    }
}
