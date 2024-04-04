using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAuthService.Domain;
using UserAuthService.Infrastructure;
using UserAuthService.JWTAuthManager;
using UserAuthService.JWTAuthManager.Models;

namespace UserAuthService.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserRepository UserRepository { get; set; }
        private readonly JwtTokenHandler _jwtTokenHandler;
        public UserController(IUserRepository userRepository, JwtTokenHandler jwtTokenHandler)
        {
            UserRepository = userRepository;
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpGet("users")]
        [Authorize]
        public IEnumerable<UserDomain> GetUsers()
        {
            return UserRepository.GetAll();
        }

        [HttpGet("user/{id}")]
        public UserDomain GetUser(string id)
        {
            return UserRepository.GetById(id);
        }

        [HttpPost("user")]
        public void AddUser([FromBody] UserDomain user)
        {
            UserRepository.Add(user);
        }

        [HttpPut("user/{id}")]
        public void UpdateUser(string id, [FromBody] UserDomain user)
        {
            UserRepository.Update(id, user);
        }

        [HttpDelete("user/{id}")]
        [Authorize(Roles = "admin")]
        public void DeleteUser(string id)
        {
            UserRepository.Delete(id);
        }

        [HttpPost("login")]
        public ActionResult<AuthenticationResponse?> Authenticate([FromBody]AuthenticationRequest request)
        {
            var authResponse = _jwtTokenHandler.GenereateJwtToken(request);
            if (authResponse == null) return Unauthorized();
            return authResponse;
        }
    }
}
