using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Numero { get; set; }
        public decimal MontoDescuento { get; set; }
        public decimal Total { get; set; }
        public int PedidoId { get; set; }
    }
}
