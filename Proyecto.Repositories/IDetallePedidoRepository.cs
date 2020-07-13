using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Repositories
{
    public interface IDetallePedidoRepository : IRepository<DetallePedido>
    {
         int InsertList(List<DetallePedido>lista);
        List<DetallePedido> ListByPedido(int PedidoId);
    }
}
