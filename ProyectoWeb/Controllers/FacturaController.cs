using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoWeb.Controllers
{
    
    [Route("api/Factura")]
    [Authorize]
    public class FacturaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public FacturaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("{page:int}/{rows:int}")]
        public IActionResult GetPaginatedList(int page,int rows)
        {
            if(page == 0 && rows == 0)
            {
                return Ok(_unitOfWork.Factura.GetList());
            }
            return Ok(_unitOfWork.Factura.GetPaginatedList("Factura",page ,rows));
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unitOfWork.Factura.GetById(id));
        }
        [HttpPost]
        public IActionResult Post([FromBody]Factura factura)
        {
            if (!ModelState.IsValid) return BadRequest();
            factura.Fecha = DateTime.Now;
            if (_unitOfWork.Factura.esValido(factura.PedidoId))
            {
                return Ok(_unitOfWork.Factura.Insert(factura));
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult Put([FromBody]Factura factura)
        {
            if(ModelState.IsValid && _unitOfWork.Factura.Update(factura))
            {
                return Ok(factura);
            }
            return BadRequest();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] Factura factura)
        {
            if(factura.Id > 0)
            {
                return Ok(_unitOfWork.Factura.Delete(factura));
            }
            return BadRequest();
        }
    }
}
