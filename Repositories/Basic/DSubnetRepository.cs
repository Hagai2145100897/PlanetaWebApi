using PlanetaWebApi.Models;

namespace PlanetaWebApi.Repositories
{
    public class DSubnetRepository : DRepository<SubnetItem>
    {
        private static ItemInfo SubnetInfo = new ItemInfo("Subnet", new[] { "FullName", "Age", "Gender" });

        public DSubnetRepository(string connectionString) : base(connectionString, SubnetInfo)
        {
        }
    }
}
