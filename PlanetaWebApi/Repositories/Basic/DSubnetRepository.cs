using System.Collections.Generic;
using System.Linq;
using PlanetaWebApi.Models;
using PlanetaWebApi.Repositories.InnerModel;

namespace PlanetaWebApi.Repositories.Basic
{
    public class DSubnetRepository : IRepository<SubnetItem>
    {
        private class DInnerSubnetRepository : DRepository<InnerSubnetItem>
        {
            protected override TableInfo table => TableInfo.SubnetTableInfo;

            public DInnerSubnetRepository(string connectionString) : base(connectionString)
            {
            }
        }
        private readonly DInnerSubnetRepository InnerReposutory;

        public DSubnetRepository(string connectionString)
        {
            InnerReposutory = new DInnerSubnetRepository(connectionString);
        }

        public IEnumerable<SubnetItem> Get() =>
            InnerReposutory.Get().Select(SubnetConverter.Convert);

        public SubnetItem Get(int id) =>
            SubnetConverter.Convert(InnerReposutory.Get(id));

        public void Create(SubnetItem item) =>
            InnerReposutory.Create(SubnetConverter.Convert(item));

        public void Update(SubnetItem item) =>
            InnerReposutory.Update(SubnetConverter.Convert(item));

        public SubnetItem Delete(int id) =>
            SubnetConverter.Convert(InnerReposutory.Delete(id));
    }
}
