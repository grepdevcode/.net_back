using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Models
{
    public class ResetPassword
    {
        public string Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaDeModificacion { get; set; }
    }
}
