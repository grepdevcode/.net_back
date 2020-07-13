using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Repositories
{
    public interface IPedidoRepository: IRepository<Pedido>
    {
        IEnumerable<ListaPedidos> GetPedidosList(int ClienteId);
        IEnumerable<Pedido> GetPaginatedList(int page, int rows);
    }
}
