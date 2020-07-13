
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Models
{
    public class ArticuloManufacturado
    {
        public int Id { get; set; }
        public int TiempoEstimadoCocina { get; set; }
        public string Denominacion { get; set; }
        public decimal PrecioVenta { get; set; }
        public string LinkImage { get; set; }
    }
}
