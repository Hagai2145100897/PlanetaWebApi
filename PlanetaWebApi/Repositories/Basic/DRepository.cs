using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using PlanetaWebApi.Models;

namespace PlanetaWebApi.Repositories.Basic
{
    public abstract class DRepository<DataItem> : IRepository<DataItem>
        where DataItem: Item
    {
        private readonly string _connectionString;
        private IDbConnection Connection => new SqlConnection(_connectionString);
        protected abstract TableInfo table { get; }

        public DRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<DataItem> Get()
        {
            using IDbConnection db = Connection;
            return db.Query<DataItem>($"SELECT * FROM [{table.Name}]");
        }

        public DataItem Get(int id)
        {
            using IDbConnection db = Connection;
            var sqlQuery = $"SELECT * FROM [{table.Name}] WHERE Id = @id";
            return db.Query<DataItem>(sqlQuery, new { id }).FirstOrDefault();
        }

        public void Create(DataItem item)
        {
            using IDbConnection db = Connection;
            var sqlQuery = $"INSERT INTO [{table.Name}] ({table.ColumnNamesAsEnumeration})" +
                $" VALUES({table.ColumnNamesAsValues});" +
                $" SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = db.Query<int>(sqlQuery, item).FirstOrDefault();
            item.Id = id;
        }

        public void Update(DataItem item)
        {
            using IDbConnection db = Connection;
            var sqlQuery = $"UPDATE [{table.Name}] SET {table.ColumnNamesForSet} WHERE Id = @Id";
            db.Execute(sqlQuery, item);
        }

        public DataItem Delete(int id)
        {
            using IDbConnection db = Connection;
            var data = Get(id);
            if (data != null)
            {
                var sqlQuery = $"DELETE FROM [{table.Name}] WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
            return data;
        }
    }
}
