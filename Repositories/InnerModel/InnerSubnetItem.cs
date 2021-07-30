using PlanetaWebApi.Models;

namespace PlanetaWebApi.Repositories.InnerModel
{
        public class InnerSubnetItem : Item
        {
            public int ClientId { get; set; }
            public string NetworkPrefix { get; set; }
        }
}
