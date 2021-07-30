using Microsoft.AspNetCore.Mvc;
using PlanetaWebApi.Repositories.Special;

namespace PlanetaWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ClientSubnetsController : Controller
    {
        private readonly IClientSubnetsRepository Repository;

        public ClientSubnetsController(IClientSubnetsRepository repository)
        {
            Repository = repository;
        }

        // [HttpGet(Name = "GetAllClients")]
        // public IEnumerable<ClientItem> Get()
        // {
        //     return Repository.Get();
        // }

        [HttpGet("{clientId}", Name = "GetClientSubnets")]
        public IActionResult Get(int clientId)
        {
            return new ObjectResult(Repository.Get(clientId));
        }

        // [HttpPost]
        // public IActionResult Create([FromBody] ClientItem clientItem)
        // {
        //     if (clientItem == null)
        //     {
        //         return BadRequest();
        //     }
        //     Repository.Create(clientItem);
        //     return CreatedAtRoute("GetClientItem", new { id = clientItem.Id }, clientItem);
        // }

        // [HttpPut("{id}")]
        // public IActionResult Update(int id, [FromBody] ClientItem updatedClientItem)
        // {
        //     if (updatedClientItem == null || updatedClientItem.Id != id)
        //     {
        //         return BadRequest();
        //     }

        //     var clientItem = Repository.Get(id);
        //     if (clientItem == null)
        //     {
        //         return NotFound();
        //     }

        //     Repository.Update(updatedClientItem);
        //     return RedirectToRoute("GetAllClients");
        // }
    }
}
