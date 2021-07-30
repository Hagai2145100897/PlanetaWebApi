using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PlanetaWebApi.Models;
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
        public IEnumerable<SubnetItem> Get(int clientId)
        {
            return Repository.Get(clientId);
        }
    }
}
