using Microsoft.AspNetCore.Mvc;
using Proyecto.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoWeb.Controllers
{
    [Route("api/ArticuloManufacturadoDetalle")]
    public class ArticuloManufacturadoDetalleController: Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public ArticuloManufacturadoDetalleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(_unitOfWork.ArticuloManufacturadoDetalle.GetList());
        }



    }
}
