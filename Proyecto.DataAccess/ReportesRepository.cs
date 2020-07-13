using Dapper;
using Dapper.Contrib.Extensions;
using Proyecto.Models;
using Proyecto.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Proyecto.DataAccess
{
    class ReportesRepository : Repository<ArticuloManufacturado>, IReportesRepository
    {
        public ReportesRepository(string connectionString) : base(connectionString)
        {
        }

        public List<dynamic> PedidosPeriodoAgrupadosPorCliente(DateTime from, DateTime to)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@startDate", from);
            parameters.Add("@endDate", to);
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query("dbo.PedidosPeriodoAgrupadosPorCliente", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public List<dynamic> ClientesRegistradosPorPeriodoTiempo(DateTime from, DateTime to)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@startDate", from);
            parameters.Add("@endDate", to);
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query("dbo.ClientesRegistradosPeriodo", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public List<dynamic> ComidasMasPedidasPorClientes()
        {
            var parameters = new DynamicParameters();

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query("dbo.ComidasMasPedidasPorClientes", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public List<dynamic> FacturasPorPeriodoTiempo(DateTime from, DateTime to)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@startDate", from);
            parameters.Add("@endDate", to);
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query("dbo.FacturasPorPeriodoTiempo", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public List<dynamic> PedidosPorPeriodoTiempo(DateTime from, DateTime to)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@startDate", from);
            parameters.Add("@endDate", to);
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query("dbo.PedidosPorPeriodoTiempo", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }
    }
}
