using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto.Models;
using Proyecto.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProyectoWeb.Controllers
{
    [Authorize]
    [Route("api/ListaPedido")]
    public class ListaPedidoController:Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ListaPedidoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public class PedidoDetalles
        {
            public Pedido Pedido { get; set; }
            public DetallePedido[] DetallePedido { get; set; }
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetPedidoList(int id)
        {
            return Ok(_unitOfWork.Pedido.GetPedidosList(id));
        }

        [HttpPut]
        [Route("cancelar")]
        public IActionResult CancelarPedido([FromBody] PedidoDetalles listPed)
        {
            if (!ModelState.IsValid) return BadRequest();
            var lista = listPed.DetallePedido;
            Console.WriteLine(lista);
            foreach (var item in listPed.DetallePedido)
            {
                if (item.ArticuloId != null)
                {
                    var id = "" + item.ArticuloId;
                    var articulo = _unitOfWork.Articulo.GetById(int.Parse(id));
                    articulo.StockActual += item.Cantidad;
                    _unitOfWork.Articulo.Update(articulo);
                }
                else if (item.ArticuloManufacturadoId != null)
                {
                    var listaArticulos = new List<Articulo>();
                    var listaDetallesArtman = _unitOfWork.ArticuloManufacturadoDetalle.GetList().Where(a => a.ArticuloManufacturadoId == item.ArticuloManufacturadoId).ToList();
                    // seguir aqui.
                    foreach (var detalle in listaDetallesArtman)
                    {
                        var artAux = _unitOfWork.Articulo.GetById(detalle.ArticuloId);
                        artAux.StockActual += item.Cantidad * detalle.Cantidad;
                        _unitOfWork.Articulo.Update(artAux);
                    }
                }
            }
            return Ok(true);
        }
    }
}
