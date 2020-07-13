using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Repositories
{
    public interface IArticuloManufacturadoDetalleRepository : IRepository<ArticuloManufacturadoDetalle>
    {
        int InsertList(List<ArticuloManufacturadoDetalle> lista);

        bool CascadeDelete(int ArticuloManufacturadoId);

        
    }
}
