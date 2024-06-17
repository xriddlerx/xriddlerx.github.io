using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProjekatDataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projekat.Api.Core
{
    public class JwtTokenCreator
    {
        private readonly ProjekatContext _context;
        //private readonly JwtSettings _settings;
        private readonly ITokenStorage _storage;

        public JwtTokenCreator(ProjekatContext context, ITokenStorage storage)
        {
            _context = context;
            //_settings = settings;
            _storage = storage;
        }

        public string Create(string email, string password)
        {
            var user = _context.Users.Where(x => x.Email == email).Select(x => new
            {
                x.Email,
                x.Password,
                x.FirstName,
                x.LastName,
                x.Id,
                UseCaseIds = x.UseCases.Select(x => x.UseCaseId)
            }).FirstOrDefault();

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new UnauthorizedAccessException();
            }

            var issuer = "asp_api";
            var secretKey = "ThisIsMyVerySecretKeyForAspProject";

            Guid tokenGuid = Guid.NewGuid();

            string tokenId = tokenGuid.ToString();

            var claims = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Jti, tokenId, ClaimValueTypes.String),
                 new Claim(JwtRegisteredClaimNames.Iss, "asp_api", ClaimValueTypes.String),
                 new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                 new Claim("Email", user.Email),
                 new Claim("FirstName", user.FirstName),
                 new Claim("LastName", user.LastName),
                 new Claim("Id", user.Id.ToString()),
                 new Claim("UseCaseIds", JsonConvert.SerializeObject(user.UseCaseIds)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(30),
                signingCredentials: credentials);

            _storage.Add(tokenGuid);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
