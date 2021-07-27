using PlanetaWebApi.Models;

namespace PlanetaWebApi.Repositories
{
    public class DClientRepository : DRepository<ClientItem>
    {
        private static ItemInfo ClientInfo = new ItemInfo("Client", new[] { "FullName", "Age", "Gender" });

        public DClientRepository(string connectionString) : base(connectionString, ClientInfo)
        {
        }
    }
}
