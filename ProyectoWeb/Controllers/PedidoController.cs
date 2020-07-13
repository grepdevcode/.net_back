using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoWeb.Controllers
{
    [Authorize]
    [Route("api/Pedido")]
    public class PedidoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PedidoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public class PedidoDetalles
        {
            public Pedido Pedido { get; set; }
            public DetallePedido[] DetallePedido { get; set; }
        }
        [HttpGet]
        [Route("{page:int}/{rows:int}")]
        public IActionResult Get(int page, int rows)
        {
            if (page==0 && rows==0)
            {
                return Ok(_unitOfWork.Pedido.GetList());
            }
            return Ok(_unitOfWork.Pedido.GetPaginatedList(page, rows));
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unitOfWork.Pedido.GetById(id));
        }
        [HttpPost]
        public IActionResult Post([FromBody] PedidoDetalles pedidoDetalles)
        {
            //Console.Write( pedidoDetalles);
            //return Ok(pedidoDetalles);
            if (!ModelState.IsValid) return BadRequest();
            pedidoDetalles.Pedido.Fecha = DateTime.Now;
            Console.WriteLine(pedidoDetalles.Pedido.HoraEstimadaFin.ToString());
            //pedidoDetalles.Pedido.HoraEstimadaFin = CalcularHoraFin(DateTime.Now,pedidoDetalles.DetallePedido);
            int fk = _unitOfWork.Pedido.Insert(pedidoDetalles.Pedido);
            var lista = new List<DetallePedido>();
            foreach (var item in pedidoDetalles.DetallePedido)
            {
                if (item.ArticuloId != null)
                {
                    var id =""+ item.ArticuloId;
                    var articulo = _unitOfWork.Articulo.GetById(int.Parse(id));
                    articulo.StockActual -= item.Cantidad;
                    _unitOfWork.Articulo.Update(articulo);
                }else if (item.ArticuloManufacturadoId != null)
                {
                    var listaArticulos=new List<Articulo>();
                    var listaDetallesArtman = _unitOfWork.ArticuloManufacturadoDetalle.GetList().Where(a => a.ArticuloManufacturadoId == item.ArticuloManufacturadoId).ToList();
                    // seguir aqui.
                    foreach (var detalle in listaDetallesArtman)
                    {
                       var artAux = _unitOfWork.Articulo.GetById(detalle.ArticuloId);
                        artAux.StockActual -= item.Cantidad * detalle.Cantidad;
                        _unitOfWork.Articulo.Update(artAux);
                    }
                     
                }
                item.PedidoId = fk;
                lista.Add(item);
            }
            _unitOfWork.DetallePedido.InsertList(lista);
            return Ok(fk);
        }
 
        [HttpDelete]
        public IActionResult Delete([FromBody] PedidoDetalles pedidoDetalles)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (pedidoDetalles.Pedido.Id > 0)
            {
                foreach (var item in pedidoDetalles.DetallePedido)
                {
                    _unitOfWork.DetallePedido.Delete(item);
                }
              return Ok( _unitOfWork.Pedido.Delete(pedidoDetalles.Pedido));
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult Put([FromBody] Pedido pedido)
        {
            if (ModelState.IsValid && _unitOfWork.Pedido.Update(pedido))
            {
                return Ok(pedido);
            }
            return BadRequest();
        }

        private DateTime CalcularHoraFin(DateTime horaPedido, DetallePedido[] detalles)
        {
            var detallesMenufacturados = detalles.Where<DetallePedido>(item => item.ArticuloManufacturadoId != null);
            var listamanufacturados = detallesMenufacturados.Select(item => {
                var id = item.ArticuloManufacturadoId + "";
               return _unitOfWork.ArticuloManufacturado.GetById(int.Parse(id));
                });
            var demora = listamanufacturados.Sum(item => item.TiempoEstimadoCocina);
            return horaPedido.AddMinutes(demora);
        }
    }
}
