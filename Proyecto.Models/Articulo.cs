using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Models
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Denominacion { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockActual { get; set; }
        public string UnidadMedida { get; set; }
        public bool esInsumo { get; set; }
        public int RubroArticuloId { get; set; }
        public string LinkImage{ get; set; }
    }
}
