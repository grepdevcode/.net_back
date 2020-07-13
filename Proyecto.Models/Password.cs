using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Models
{
    public class Password
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public int ClienteId { get; set; }
    }
}
