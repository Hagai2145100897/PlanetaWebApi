using System.Collections.Generic;

namespace PlanetaWebApi.Repositories.Basic
{
    public interface IRepository<DataType>
    {
        IEnumerable<DataType> Get();
        DataType Get(int id);
        void Create(DataType item);
        void Update(DataType item);
        DataType Delete(int id);
    }
}
