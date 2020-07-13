using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Proyecto.Models;
using Proyecto.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace ProyectoWeb.Controllers
{
    [Authorize]
    [Route("api/RubroArticulo")]
    public class RubroArticuloController: Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public RubroArticuloController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_unitOfWork.RubroArticulo.GetById(id));
        }
        [HttpGet]
        [Route("{page:int}/{rows:int}")]
        public IActionResult Get(int page , int rows)
        {
            if (page == 0 && rows ==0)
            {
                return Ok(_unitOfWork.RubroArticulo.GetList());
            }
            return Ok(_unitOfWork.RubroArticulo.GetPaginatedList(page, rows));
        }
        [HttpPost]
        public IActionResult Post([FromBody] RubroArticulo rubroArticulo)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(_unitOfWork.RubroArticulo.Insert(rubroArticulo));
        }
        [HttpPut]
        public IActionResult Put([FromBody] RubroArticulo rubroArticulo)
        {
            Console.WriteLine(rubroArticulo);
            if (ModelState.IsValid && _unitOfWork.RubroArticulo.Update(rubroArticulo))
            {
                return Ok(new { Message = $"El rubro ID: {rubroArticulo.Id} ha sido actualizado." });
            }
            return BadRequest();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] RubroArticulo rubroArticulo)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (rubroArticulo.Id > 0)
            {
                return Ok(_unitOfWork.RubroArticulo.Delete(rubroArticulo));
            }
            return BadRequest();
        }
    }
}
