using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Repositories
{
    public interface IReportesRepository
    {
        List<dynamic> ComidasMasPedidasPorClientes();
        List<dynamic> FacturasPorPeriodoTiempo(DateTime from, DateTime to);
        List<dynamic>PedidosPorPeriodoTiempo(DateTime from, DateTime to);
        List<dynamic> PedidosPeriodoAgrupadosPorCliente(DateTime from, DateTime to);
        List<dynamic> ClientesRegistradosPorPeriodoTiempo(DateTime from, DateTime to);
    }
}
