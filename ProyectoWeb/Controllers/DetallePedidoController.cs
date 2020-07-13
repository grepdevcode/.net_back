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
    [Authorize]
    [Route("api/DetallePedido")]
    public class DetallePedidoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public class PedidoDetalles
        {
            public Pedido Pedido { get; set; }
            public DetallePedido[] DetallePedido { get; set; }
        }
        public DetallePedidoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(_unitOfWork.DetallePedido.GetList());
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public IActionResult GetListByPedidoId(int id)
        {
            return Ok(_unitOfWork.DetallePedido.ListByPedido(id));
        }

        [HttpPut]
        public IActionResult Put([FromBody] PedidoDetalles pedidoDetalles)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in pedidoDetalles.DetallePedido)
                {
                    _unitOfWork.DetallePedido.Update(item);
                }
                if (_unitOfWork.Pedido.Update(pedidoDetalles.Pedido))
                {
                    return Ok(new { pedidoDetalles.Pedido.Id });
                }
            }
            return BadRequest();
        }
        

    }
    
}
