using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public long Telefono { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
