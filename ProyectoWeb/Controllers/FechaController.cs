using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoWeb.Controllers
{
    [Route("api/Fecha")]
    public class FechaController : Controller
    {
        public class test
        {
            public string Nombre { get; set; }
            public DateTime Fecha { get; set; }
        }
        [HttpPost]
        public IActionResult Post([FromBody] test test)
        {
            if (!ModelState.IsValid) return BadRequest();
            Console.WriteLine(test.Fecha.ToString());
            return Ok(test);
        }
        
    }
}
