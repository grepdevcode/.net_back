using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoWeb.Controllers
{
    [Authorize]
    [Route("api/Reportes")]
    public class ReportesController: Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Reportes.ComidasMasPedidasPorClientes());
        }

        [Route("{op:int}/{startDateString}/{endDateString}")]
        [HttpGet]
        public IActionResult GetByDateRange(int op, string startDateString, string endDateString)
        {
            DateTime from = DateTime.Parse(startDateString);
            DateTime to = DateTime.Parse(endDateString);
            switch (op)
            {
                case 1:
                    return Ok(_unitOfWork.Reportes.ClientesRegistradosPorPeriodoTiempo(from,to));
                case 2:
                    Console.WriteLine();
                    return Ok(_unitOfWork.Reportes.FacturasPorPeriodoTiempo(from, to));
                case 3:
                    Console.WriteLine();
                    return Ok(_unitOfWork.Reportes.PedidosPeriodoAgrupadosPorCliente(from, to));
                case 4:
                    Console.WriteLine();
                    return Ok(_unitOfWork.Reportes.PedidosPorPeriodoTiempo(from, to));
                default:
                    return BadRequest();
            }
            
        }
    }
}
