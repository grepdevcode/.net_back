using Dapper;
using Proyecto.Models;
using Proyecto.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Proyecto.DataAccess
{
    public class PedidoRepository : Repository<Pedido>,IPedidoRepository
    {
        public PedidoRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<ListaPedidos> GetPedidosList(int ClienteId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ClienteId", ClienteId);

            using (var connection = new SqlConnection(_connectionString))
            {
                var res = connection.QueryMultiple("dbo.PedidosPorCliente", parameters, commandType: System.Data.CommandType.StoredProcedure);
                var pedidosLista = res.Read<ListaPedidos>().ToList();
                var listaDetalles = res.Read<ListaDetalles>().ToList();
                foreach (var item in pedidosLista)
                {
                    item.SetDetalles(listaDetalles);
                }
                return pedidosLista;
            }
        }

        public IEnumerable<Pedido> GetPaginatedList(int page, int rows)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@name", "Pedido");
            parameters.Add("@page", page);
            parameters.Add("@rows", rows);

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Pedido>("dbo.PaginatedSelect", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
