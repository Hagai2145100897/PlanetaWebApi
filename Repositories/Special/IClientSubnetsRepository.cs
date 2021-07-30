using System.Collections.Generic;
using PlanetaWebApi.Models;

namespace PlanetaWebApi.Repositories.Special
{
    public interface IClientSubnetsRepository
    {
        IEnumerable<SubnetItem> Get(int clientId);
    }
}
