using UserAuthService.JWTAuthManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAuthService.Infrastructure;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace UserAuthService.JWTAuthManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECRET_KEY = "asMdasySeasdacretK123123fafass12ey@1sdfsd2f34asddasd@@3423afd09u8ahduga";
        private const int JWT_TOKEN_VALIDITY_MINS = 60;

        public IUserRepository UserRepository { get; set; }
        public JwtTokenHandler(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public AuthenticationResponse? GenereateJwtToken(AuthenticationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return null;
            var userAccount = UserRepository.Login(request.Email, request.Password);
            if (userAccount == null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECRET_KEY);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
                new Claim("Name", userAccount.Name),
                new Claim(ClaimTypes.Role, userAccount.Role)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature
            );

            var securityDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                Id = userAccount.Id,
                Name = userAccount.Name,
                Role = userAccount.Role,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token,
            };
        }
    }
}
