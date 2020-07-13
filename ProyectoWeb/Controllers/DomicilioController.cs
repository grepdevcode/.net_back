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
    [Route("api/Domicilio")]
    public class DomicilioController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DomicilioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Domicilio.GetList());
        }
        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unitOfWork.Domicilio.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Domicilio domicilio)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(_unitOfWork.Domicilio.Insert(domicilio));
        }
        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody]Domicilio domicilio)
        {
            if (ModelState.IsValid)
            {
                if (_unitOfWork.Domicilio.Update(domicilio))
                {
                    return Ok( domicilio.Id);
                }
            }
            return BadRequest();
        }
        [Authorize]
        [HttpDelete]
        public IActionResult Delete([FromBody] Domicilio domicilio)
        {

            if (domicilio.Id > 0)
            {
                return Ok(_unitOfWork.Domicilio.Delete(domicilio));
            }
            return BadRequest();
        }
    }
}
