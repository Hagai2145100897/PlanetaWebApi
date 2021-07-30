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

        [HttpGet("{clientId}", Name = "GetClientSubnets")]
        public IActionResult Get(int clientId)
        {
            return new ObjectResult(Repository.Get(clientId));
        }
    }
}
