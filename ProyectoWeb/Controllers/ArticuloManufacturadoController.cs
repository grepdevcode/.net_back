using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using Proyecto.Models;
using Proyecto.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoWeb.Controllers
{
    
    [Route("api/ArticuloManufacturado")]
    public class ArticuloManufacturadoController:Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public ArticuloManufacturadoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public class ManufacturadoDetalles
        {
            public ArticuloManufacturado ArticuloManufacturado { get; set; }
            public ArticuloManufacturadoDetalle[] ArticuloManufacturadoDetalle { get; set; }
        }
        //GET
        [HttpGet]
        [Route("{id:int:min(1)}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unitOfWork.ArticuloManufacturado.GetById(id));
        }
        [HttpGet]
        [Route("{page:int}/{rows:int}")]
        public IActionResult GetList(int page, int rows)
        {
            if(page ==0 && rows == 0)
            {
                return Ok(_unitOfWork.ArticuloManufacturado.GetList());
            }
            return Ok(_unitOfWork.ArticuloManufacturado.GetPaginatedList(page, rows));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] ManufacturadoDetalles manufacturadoDetalles)
        {
            Console.Write(manufacturadoDetalles.ArticuloManufacturado.TiempoEstimadoCocina);
            if (!ModelState.IsValid) return BadRequest();
            int fk = _unitOfWork.ArticuloManufacturado.Insert(manufacturadoDetalles.ArticuloManufacturado);
            var list = new List<ArticuloManufacturadoDetalle>();
            foreach (var item in manufacturadoDetalles.ArticuloManufacturadoDetalle)
            {
                item.ArticuloManufacturadoId = fk;
                list.Add(item);
            }
            _unitOfWork.ArticuloManufacturadoDetalle.InsertList(list);
            return Ok(fk);
        }

        // PUT
        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody] ManufacturadoDetalles manufacturadoDetalles)
        {
            if (ModelState.IsValid)
            {
                int fk = manufacturadoDetalles.ArticuloManufacturado.Id;
                var list = new List<ArticuloManufacturadoDetalle>();
                if (_unitOfWork.ArticuloManufacturado.Update(manufacturadoDetalles.ArticuloManufacturado))
                {
                    if (_unitOfWork.ArticuloManufacturadoDetalle.CascadeDelete(fk))
                    {
                        foreach (var item in manufacturadoDetalles.ArticuloManufacturadoDetalle)
                        {
                            item.ArticuloManufacturadoId = fk;
                            list.Add(item);
                        }
                        _unitOfWork.ArticuloManufacturadoDetalle.InsertList(list);
                        return Ok(fk);
                    }
                }
            }
            return BadRequest();
        }

        // DELETE primero borramos los detalles y despues el articuloManufacturado
        [Authorize]
        [HttpDelete]
        public IActionResult Delete([FromBody]ArticuloManufacturado articuloManufacturado)
        {
            if (articuloManufacturado.Id > 0)
            {
                if (_unitOfWork.ArticuloManufacturadoDetalle.CascadeDelete(articuloManufacturado.Id))
                {
                    return Ok(_unitOfWork.ArticuloManufacturado.Delete(articuloManufacturado));
                }
            }
            return BadRequest();
        }

    }
}
