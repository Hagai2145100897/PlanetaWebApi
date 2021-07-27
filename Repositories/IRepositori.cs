using System.Collections.Generic;

namespace PlanetaWebApi.Repositories
{
    public interface IRepository<DataItem>
    {
        IEnumerable<DataItem> Get();
        DataItem Get(int id);
        void Create(DataItem item);
        void Update(DataItem item);
        DataItem Delete(int id);
    }
}
