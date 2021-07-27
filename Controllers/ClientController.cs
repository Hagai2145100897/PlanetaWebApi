using Microsoft.AspNetCore.Mvc;
using PlanetaWebApi.Models;
using PlanetaWebApi.Repositories;
using System.Collections.Generic;

namespace PlanetaWebApi.Controllers
{
    [Route("[controller]")]
    public class ClientController : Controller
    {
        private readonly IRepository<ClientItem> ClienRepository;

        public ClientController(IRepository<ClientItem> clienRepository)
        {
            ClienRepository = clienRepository;
        }

        [HttpGet(Name = "GetAllClients")]
        public IEnumerable<ClientItem> Get()
        {
            return ClienRepository.Get();
        }

        [HttpGet("{id}", Name = "GetClientItem")]
        public IActionResult Get(int id)
        {
            ClientItem clientItem = ClienRepository.Get(id);

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
            ClienRepository.Create(clientItem);
            return CreatedAtRoute("GetClientItem", new { id = clientItem.Id }, clientItem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ClientItem updatedClientItem)
        {
            if (updatedClientItem == null || updatedClientItem.Id != id)
            {
                return BadRequest();
            }

            var clientItem = ClienRepository.Get(id);
            if (clientItem == null)
            {
                return NotFound();
            }

            ClienRepository.Update(updatedClientItem);
            return RedirectToRoute("GetAllClients");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedClientItem = ClienRepository.Delete(id);

            if (deletedClientItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedClientItem);
        }

    }
}
