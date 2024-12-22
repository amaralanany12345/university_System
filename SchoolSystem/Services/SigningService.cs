using Microsoft.IdentityModel.Tokens;
using SchoolSystem.General;
using SchoolSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolSystem.Services
{
    public abstract class SigningService
    {
        private readonly Jwt _jwt;
        HttpContextAccessor httpContextAccessor;
        public SigningService(Jwt jwt)
        {
            _jwt = jwt;
            httpContextAccessor=new HttpContextAccessor();
        }

        public string generateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwt.Issuer,
                Audience = _jwt.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SigningKey)),
                SecurityAlgorithms.HmacSha256Signature),
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.Name,user.userName),
                    new (ClaimTypes.Email,user.email),
                    new Claim(ClaimTypes.NameIdentifier,user.id.ToString()),
                })
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);
            return accessToken;
        }

        public static string hashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool verifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        public abstract SigningResponse signin(SigninInfo userInfo);
    }
}
