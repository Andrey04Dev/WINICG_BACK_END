using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Models;

namespace webapi.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly SymmetricSecurityKey sskey;

        public TokenServices(IConfiguration config)
        {
            sskey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Token"]));
        }
        public string createToken(USERS user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.EMAIL),
                new Claim(ClaimTypes.Role, user.ROLE.ROLE), 
                new Claim(ClaimTypes.Name, user.FULLNAME),
            };
            var credentials = new SigningCredentials(sskey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(1000),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

