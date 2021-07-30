using PlanetaWebApi.Models;

namespace PlanetaWebApi.Repositories.Basic
{
    public class DClientRepository : DRepository<ClientItem>
    {
        protected override TableInfo table => TableInfo.ClientTableInfo;

        public DClientRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
