using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.API.Domain;
using Order.API.Infrastructure;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderRepository OrderRepository { get; set; }

        public OrderController(IOrderRepository orderRepository)
        {
            OrderRepository = orderRepository;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            string uId = userId;
            if (id == uId || userRole == "admin")
                return Ok(OrderRepository.GetByUserId(id));
            return Unauthorized();
        }

        // POST api/<OrderController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var res = await OrderRepository.Add(userId);
            return Ok(res.Message);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
