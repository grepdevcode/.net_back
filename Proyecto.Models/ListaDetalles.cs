using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto.Models
{
    public class ListaDetalles
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public int FacturaId { get; set; }
        public int PedidoId { get; set; }
        public Nullable< int> ArticuloManufacturadoId { get; set; }
        public Nullable <int> ArticuloId { get; set; }

    }
}
