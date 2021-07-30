using System.Collections.Generic;
using System.Net;
using PlanetaWebApi.Models;
using PlanetaWebApi.Repositories.Special;

namespace PlanetaWebApi.Tests.TestControllers
{
    class ClientSubnetsRepositoryForTest : IClientSubnetsRepository
    {
        private static readonly IDictionary<int, IEnumerable<SubnetItem>> TestData = new Dictionary<int, IEnumerable<SubnetItem>> {
            [1] = new[] {
                    new SubnetItem() { Id = 1, ClientId = 1, NetworkPrefix = IPAddress.Parse("121.0.0.0"), SubnetMask = 8, },
            },
            [2] = new[] {
                    new SubnetItem() { Id = 2, ClientId = 2, NetworkPrefix = IPAddress.Parse("122.0.0.0"), SubnetMask = 8, },
                    new SubnetItem() { Id = 3, ClientId = 2, NetworkPrefix = IPAddress.Parse("123.0.0.0"), SubnetMask = 8, },
            },
            [3] = new[] {
                    new SubnetItem() { Id = 4, ClientId = 3, NetworkPrefix = IPAddress.Parse("124.0.0.0"), SubnetMask = 8, },
                    new SubnetItem() { Id = 5, ClientId = 3, NetworkPrefix = IPAddress.Parse("125.0.0.0"), SubnetMask = 8, },
                    new SubnetItem() { Id = 6, ClientId = 3, NetworkPrefix = IPAddress.Parse("126.0.0.0"), SubnetMask = 8, },
            },
        };

        public IEnumerable<SubnetItem> Get(int clientId)
        {
            return TestData[clientId];
        }
    }
}
