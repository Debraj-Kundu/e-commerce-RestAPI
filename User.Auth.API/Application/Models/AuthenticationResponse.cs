using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAuthService.JWTAuthManager.Models
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public string JwtToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
