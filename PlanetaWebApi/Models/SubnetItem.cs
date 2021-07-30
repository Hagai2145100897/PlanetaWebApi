using System.Net;

namespace PlanetaWebApi.Models
{
    public class SubnetItem : Item
    {
        public int ClientId { get; set; }

        public IPAddress NetworkPrefix { get; set; }

        public int SubnetMask { get; set; }
    }
}
