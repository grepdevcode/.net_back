using System;
using System.Collections.Generic;
using System.Text;
using Proyecto.Models;
using Proyecto.Repositories;
using Proyecto.UnitOfWork;

namespace Proyecto.DataAccess
{
    public class ProyectoUnitOfWork : IUnitOfWork
    {
        public ProyectoUnitOfWork(string connectionString)
        {

            //Propios
            Articulo = new ArticuloRepository(connectionString);
            ArticuloManufacturado = new ArticuloManufacturadoRepository(connectionString);
            ArticuloManufacturadoDetalle = new ArticuloManufacturadoDetalleRepository(connectionString);
            Cliente = new ClienteRepository(connectionString);
            DetallePedido = new DetallePedidoRepository(connectionString);
            Domicilio = new Repository<Domicilio>(connectionString);
            Factura = new FacturaRepository(connectionString);
            Pedido = new PedidoRepository(connectionString);
            RubroArticulo = new RubroArticuloRepository (connectionString);
            Password = new Repository<Password>(connectionString);
            Reportes = new ReportesRepository(connectionString);
            ResetPassword = new ResetPasswordRepository(connectionString);
        }

        //Propios
        public IArticuloRepository Articulo { get; private set; }
        public IArticuloManufacturadoRepository ArticuloManufacturado { get; private set; }
        public IArticuloManufacturadoDetalleRepository  ArticuloManufacturadoDetalle { get; private set; }
        public IClienteRepository Cliente { get; private set; }
        public IDetallePedidoRepository DetallePedido { get; private set; }
        public IRepository<Domicilio> Domicilio { get; private set; }
        public IFacturaRepository Factura { get; private set; }
        public IPedidoRepository Pedido { get; private set; }
        public IRubroArticuloRepository RubroArticulo { get; private set; }
        public IRepository<Password> Password { get; private set; }
        public IReportesRepository Reportes { get; private set; }
        public IResetPasswordRepository ResetPassword { get; private set; }
    }
}
