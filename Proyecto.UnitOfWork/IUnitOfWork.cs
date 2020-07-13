using Proyecto.Models;
using Proyecto.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.UnitOfWork
{
    public interface IUnitOfWork
    {

        //Propios
        IArticuloRepository Articulo { get; }
        IArticuloManufacturadoRepository ArticuloManufacturado { get; }
        IArticuloManufacturadoDetalleRepository ArticuloManufacturadoDetalle { get; }
        IClienteRepository Cliente { get; }
        IDetallePedidoRepository DetallePedido { get; }
        IRepository<Domicilio> Domicilio { get; }
        IFacturaRepository Factura { get; }
        IPedidoRepository Pedido { get; }
        IRubroArticuloRepository RubroArticulo { get; }
        IRepository<Password> Password { get; }
        IReportesRepository Reportes { get; }
        IResetPasswordRepository ResetPassword { get;}
    }
}
