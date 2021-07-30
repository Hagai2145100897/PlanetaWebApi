using Microsoft.AspNetCore.Mvc;
using PlanetaWebApi.Models;
using PlanetaWebApi.Repositories.Basic;
using System.Collections.Generic;

namespace PlanetaWebApi.Controllers
{
    [Route("[controller]")]
    public class SubnetController : Controller
    {
        private readonly IRepository<SubnetItem> Repository;

        public SubnetController(IRepository<SubnetItem> subnetRepository)
        {
            Repository = subnetRepository;
        }

        [HttpGet(Name = "GetAllSubnets")]
        public IEnumerable<SubnetItem> Get()
        {
            return Repository.Get();
        }

        [HttpGet("{id}", Name = "GetSubnet")]
        public IActionResult Get(int id)
        {
            SubnetItem subnetItem = Repository.Get(id);

            if (subnetItem == null)
            {
                return NotFound();
            }

            return new ObjectResult(subnetItem);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SubnetItem subnetItem)
        {
            if (subnetItem == null)
            {
                return BadRequest();
            }
            Repository.Create(subnetItem);
            return CreatedAtRoute("GetSubnet", new { id = subnetItem.Id }, subnetItem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SubnetItem updatedSubnetItem)
        {
            if (updatedSubnetItem == null || updatedSubnetItem.Id != id)
            {
                return BadRequest();
            }

            var subnetItem = Repository.Get(id);
            if (subnetItem == null)
            {
                return NotFound();
            }

            Repository.Update(updatedSubnetItem);
            return RedirectToRoute("GetAllSubnets");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedSubnetItem = Repository.Delete(id);

            if (deletedSubnetItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedSubnetItem);
        }
    }
}
