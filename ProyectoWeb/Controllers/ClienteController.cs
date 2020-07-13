using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proyecto.Models;
using Proyecto.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoWeb.Controllers
{
    
    [Route("api/Cliente")]
    public class ClienteController: Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string passPhrase = "enzo";
        public class ClienteDomicilio
        {
            public Cliente Cliente { get; set; }
            public Domicilio Domicilio { get; set; }
            public String password { get; set; }
        }

        public ClienteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok( _unitOfWork.Cliente.GetById(id) );
        }
        [Authorize]
        [HttpGet]
        [Route("{page:int}/{rows:int}")]
        public IActionResult Get(int page, int rows)
        {
            if(page== 0 && rows == 0)
            {
                return Ok(_unitOfWork.Cliente.GetList());
            }
            return Ok(_unitOfWork.Cliente.GetPaginatedList(page,rows));
        }
        [HttpPost]
        public IActionResult Post( [FromBody] ClienteDomicilio data)
        {
            if (!ModelState.IsValid) return BadRequest();
            data.Cliente.FechaRegistro = DateTime.Now; 
            int fk = _unitOfWork.Cliente.Insert(data.Cliente);
            Console.WriteLine(fk);
            if (fk > 0)
            {
                Password password = new Password();
                password.Hash = Encriptador.Encrypt(data.password,passPhrase);
                password.ClienteId = fk;
                _unitOfWork.Password.Insert(password);
            }
            data.Domicilio.ClienteId = fk;
           
            if(data.Domicilio.ClienteId != 0)
            {
               return Ok(_unitOfWork.Domicilio.Insert(data.Domicilio));
            }
            //var nuevoRegistro = _unitOfWork.Cliente.InsertClienteDomicilio(data.Cliente, data.Domicilio);
            return BadRequest();
        }
        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody] Cliente cliente)
        {
            if (ModelState.IsValid && _unitOfWork.Cliente.Update(cliente))
            {
                return Ok(cliente.Id);
            }
            return BadRequest();
        }
        [Authorize]
        [HttpDelete]
        public IActionResult Delete([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (cliente.Id > 0)
            {
                return Ok(_unitOfWork.Cliente.BorrarCliente(cliente));
            }
            return BadRequest();
        }
    }
}
