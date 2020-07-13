using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto.Models
{
    public class ListaPedidos
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Numero { get; set; }
        public int Estado { get; set; }
        public DateTime HoraEstimadaFin { get; set; }
        public int TipoEnvio { get; set; }
        public int ClienteId { get; set; }
        public List <ListaDetalles> ListaDetalles { get; set; }
        public void SetDetalles(List<ListaDetalles> detalles)
        {
            ListaDetalles = detalles.Where(detalle => detalle.PedidoId == Id).ToList();
        }
    }
}
