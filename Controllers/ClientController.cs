using Microsoft.AspNetCore.Mvc;
using PlanetaWebApi.Models;
using PlanetaWebApi.Repositories.Basic;
using System.Collections.Generic;

namespace PlanetaWebApi.Controllers
{
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly IRepository<ClientItem> Repository;

        public ClientController(IRepository<ClientItem> clienRepository)
        {
            Repository = clienRepository;
        }

        [HttpGet(Name = "GetAllClients")]
        public IEnumerable<ClientItem> Get()
        {
            return Repository.Get();
        }

        [HttpGet("{id}", Name = "GetClient")]
        public IActionResult Get(int id)
        {
            ClientItem clientItem = Repository.Get(id);

            if (clientItem == null)
            {
                return NotFound();
            }

            return new ObjectResult(clientItem);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ClientItem clientItem)
        {
            if (clientItem == null)
            {
                return BadRequest();
            }
            Repository.Create(clientItem);
            return CreatedAtRoute("GetClient", new { id = clientItem.Id }, clientItem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ClientItem updatedClientItem)
        {
            if (updatedClientItem == null || updatedClientItem.Id != id)
            {
                return BadRequest();
            }

            var clientItem = Repository.Get(id);
            if (clientItem == null)
            {
                return NotFound();
            }

            Repository.Update(updatedClientItem);
            return RedirectToRoute("GetAllClients");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedClientItem = Repository.Delete(id);

            if (deletedClientItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedClientItem);
        }

    }
}
