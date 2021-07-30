using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using PlanetaWebApi.Models;
using PlanetaWebApi.Repositories.InnerModel;

namespace PlanetaWebApi.Repositories.Special
{
    public class DClientSubnetsRepository : IClientSubnetsRepository
    {
        private readonly string _connectionString;
        private IDbConnection Connection => new SqlConnection(_connectionString);
        private TableInfo clientTable => TableInfo.ClientTableInfo;
        private TableInfo subnetTable => TableInfo.SubnetTableInfo;

        public DClientSubnetsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<SubnetItem> Get(int clientId)
        {
            using IDbConnection db = Connection;
            var sqlQuery = $"SELECT *" +
                $" FROM [{subnetTable.Name}]" + 
                $" WHERE [ClientId] = @clientId";
            return db.Query<InnerSubnetItem>(sqlQuery, new { clientId })
                .Select(SubnetConverter.Convert);
        }
    }
}
