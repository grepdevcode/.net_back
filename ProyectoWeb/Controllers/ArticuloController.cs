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
    
    [Route("api/Articulo")]
    public class ArticuloController: Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ArticuloController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("{page:int}/{rows:int}")]
        public IActionResult Get(int page, int rows)
        {
            if(page == 0 && rows == 0)
            {
                return Ok(_unitOfWork.Articulo.GetList());
            }
            return Ok(_unitOfWork.Articulo.GetPaginatedList(page, rows));
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unitOfWork.Articulo.GetById(id));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]Articulo articulo)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(_unitOfWork.Articulo.Insert(articulo));
        }
        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody]Articulo articulo)
        {
            if (ModelState.IsValid )
            {
                if ( _unitOfWork.Articulo.Update(articulo))
                {
                    return Ok(new { Message = $"The customer { articulo.Id} has been updated" });
                }
            }
            return BadRequest();
        }
        [Authorize]
        [HttpDelete]
        public IActionResult Delete([FromBody] Articulo articulo)
        {
            
            if (articulo.Id > 0)
            {
                return Ok(_unitOfWork.Articulo.Delete(articulo));
            }
            return BadRequest();
        }
    }
}
