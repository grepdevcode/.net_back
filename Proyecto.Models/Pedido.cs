using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Models
{
     public class Pedido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Numero { get; set; }
        public int Estado { get; set; }
        public DateTime HoraEstimadaFin { get; set; }
        public int TipoEnvio { get; set; }
        public int ClienteId { get; set; }
    }

}
