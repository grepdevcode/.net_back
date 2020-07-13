using Dapper;
using Proyecto.Models;
using Proyecto.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Proyecto.DataAccess
{
    public class ArticuloManufacturadoRepository: Repository<ArticuloManufacturado>, IArticuloManufacturadoRepository
    {
        public ArticuloManufacturadoRepository(string connectionString):base (connectionString)
        {

        }

        public IEnumerable<ArticuloManufacturado> GetPaginatedList(int page, int rows)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@page", page);
            parameters.Add("@rows", rows);
            parameters.Add("@name", "ArticuloManufacturado");
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<ArticuloManufacturado>("dbo.PaginatedSelect", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
