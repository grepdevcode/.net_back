using Dapper;
using Proyecto.Models;
using Proyecto.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Proyecto.DataAccess
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        public ArticuloRepository(string connectionString): base(connectionString)
        {
                
        }
        public IEnumerable<Articulo> GetPaginatedList(int page, int rows)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@name", "Articulo");
            parameters.Add("@page", page);
            parameters.Add("@rows", rows);
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Articulo>("dbo.PaginatedSelect", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
