using Dapper;
using Dapper.Contrib.Extensions;
using Proyecto.Models;
using Proyecto.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Proyecto.DataAccess
{
    public class DetallePedidoRepository : Repository<DetallePedido>, IDetallePedidoRepository
    {
        public DetallePedidoRepository(string connectionString) : base(connectionString)
        {
        }

        public int InsertList(List<DetallePedido> lista)
        {
            using (var Connection = new SqlConnection(_connectionString))
            {
                return (int)Connection.Insert(lista);
            }
        }

        public List<DetallePedido> ListByPedido(int PedidoId)
        {
            var sql = $"SELECT * FROM DetallePedido WHERE PedidoId = {PedidoId}";
            using (var connection = new SqlConnection(_connectionString))
            {
                return (List<DetallePedido>)connection.Query<DetallePedido>(sql);
            }
        }
    }
}
