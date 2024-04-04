using Inventory.API.Domain;
using Inventory.API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        public IInventoryRepository InventoryRepository { get; set; }
        public InventoryController(IInventoryRepository inventoryRepository) 
        {
            InventoryRepository = inventoryRepository;
        }
        // GET: api/<InventoryController>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IEnumerable<ProductQuantityDomain> Get()
        {
            return InventoryRepository.GetAll();
        }

        // GET api/<InventoryController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return "value";
        }

        // POST api/<InventoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<InventoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InventoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
