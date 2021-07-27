using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using PlanetaWebApi.Models;

namespace PlanetaWebApi.Repositories
{
    public abstract class DRepository<DataItem> : IRepository<DataItem>
        where DataItem: Item
    {
        private readonly string _connectionString;
        private readonly ItemInfo _info;
        private string tableColumnNamesAsEnumeration => _info.ItemFieldNames.Aggregate((r, n) => $"{r}, {n}");
        private string tableColumnNamesAsValues => _info.ItemFieldNames.Select(n => $"@{n}").Aggregate((r, n) => $"{r}, {n}");
        private string tableColumnNamesForSet => _info.ItemFieldNames.Select(n => $"{n} = @{n}").Aggregate((r, n) => $"{r}, {n}");

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public DRepository(string connectionString, ItemInfo info)
        {
            _connectionString = connectionString;
            _info = info;
        }

        public void Create(DataItem item)
        {
            using IDbConnection db = Connection;
            var sqlQuery = $"INSERT INTO {_info.ItemName} ({tableColumnNamesAsEnumeration}) VALUES({tableColumnNamesAsValues}); SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = db.Query<int>(sqlQuery, item).FirstOrDefault();
            item.Id = id;
        }

        public DataItem Delete(int id)
        {
            using IDbConnection db = Connection;
            var data = Get(id);
            if (data != null)
            {
                var sqlQuery = $"DELETE FROM {_info.ItemName} WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
            return data;
        }

        public IEnumerable<DataItem> Get()
        {
            using IDbConnection db = Connection;
            return db.Query<DataItem>($"SELECT * FROM {_info.ItemName}").ToList();
        }

        public DataItem Get(int id)
        {
            using IDbConnection db = Connection;
            return db.Query<DataItem>($"SELECT * FROM {_info.ItemName} WHERE Id = @id", new { id }).FirstOrDefault();
        }

        public void Update(DataItem item)
        {
            using IDbConnection db = Connection;
            var sqlQuery = $"UPDATE {_info.ItemName} SET {tableColumnNamesForSet} WHERE Id = @Id";
            db.Execute(sqlQuery, item);
        }
    }
}
