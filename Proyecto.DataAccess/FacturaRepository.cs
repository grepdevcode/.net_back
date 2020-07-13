using Dapper;
using Proyecto.Models;
using Proyecto.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Proyecto.DataAccess
{
    public class FacturaRepository : Repository<Factura>, IFacturaRepository
    {
        public FacturaRepository(string connectionString) : base(connectionString)
        {
        }

        public bool esValido(int PedidoId)
        {
            var sql = $"Select * from Pedido Where Id = {PedidoId}";
            using (var connection = new SqlConnection(_connectionString))
            {
                if((int)connection.Query(sql).Count() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Factura> GetPaginatedList(string name,int page, int rows)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@page", page);
            parameters.Add("@rows", rows);
            parameters.Add("@name", name);
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Factura>("dbo.PaginatedSelect", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
