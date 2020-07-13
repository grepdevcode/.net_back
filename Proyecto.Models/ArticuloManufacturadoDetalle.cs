
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Models
{
    public class ArticuloManufacturadoDetalle
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int ArticuloManufacturadoId { get; set; }
        public int ArticuloId { get; set; }
    }
}
