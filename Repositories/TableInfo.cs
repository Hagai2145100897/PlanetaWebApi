using System.Linq;

namespace PlanetaWebApi.Repositories
{
    public class TableInfo
    {
        public readonly string Name;
        public readonly string ColumnNamesAsEnumeration;
        public readonly string ColumnNamesAsValues;
        public readonly string ColumnNamesForSet;

        public TableInfo(string tableName, string[] tableColumnNames)
        {
            Name = tableName;
            ColumnNamesAsEnumeration = tableColumnNames.Select(n => $"[{n}]").Aggregate((r, n) => $"{r}, {n}");
            ColumnNamesAsValues = tableColumnNames.Select(n => $"@{n}").Aggregate((r, n) => $"{r}, {n}");
            ColumnNamesForSet = tableColumnNames.Select(n => $"[{n}] = @{n}").Aggregate((r, n) => $"{r}, {n}");
        }

        public static readonly TableInfo ClientTableInfo = new TableInfo("Client", new[] {"FullName", "Age", "Gender"});
        public static readonly TableInfo SubnetTableInfo = new TableInfo("Subnet", new[] {"ClientId", "NetworkPrefix"});
    }
}