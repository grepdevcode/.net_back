using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Repositories
{
    public interface IFacturaRepository: IRepository<Factura>
    {
        bool esValido(int PedidoId);

        IEnumerable<Factura> GetPaginatedList(string name, int page, int rows);
    }
}
