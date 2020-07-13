using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Models
{
    public class Domicilio
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public int Numero { get; set; }
        public string Localidad { get; set; }
        public int ClienteId { get; set; }
    }
}
