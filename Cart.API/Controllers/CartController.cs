using Cart.API.Domain;
using Cart.API.Infrastructure;
using MassTransit.Transports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public ICartRepository CartRepository { get; set; }
        public CartController(ICartRepository cartRepository)
        {
            CartRepository = cartRepository;
        }
        // GET: api/<CartController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get(string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            string uId = userId;
            if(id == uId || userRole == "admin")
                return Ok(await CartRepository.GetByUserId(id));
            return Unauthorized();
        }

        // POST api/<CartController>
        [HttpPost]
        public async Task Post([FromBody] CartDomain cart)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            cart.UserId = userId;
            await CartRepository.Add(cart);
        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
