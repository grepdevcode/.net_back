using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace ProyectoWeb.Controllers
{
    [Route("api/ResetPassword")]
    public class ResetPasswordController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string passPhrase = "enzo";
        public ResetPasswordController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public class cambiarPassword
        {
            public string nuevoPassword { get; set; }
            public string uid { get; set; }
        }
        public class ClienteEmail
        {
            public string ToEmail { get; set; }
        }
        [HttpGet]
        public IActionResult Get(string id)
        {
            //Console.WriteLine(id);
            // Tomar Registro en tabla reset pasword.
            ResetPassword registroUID;
            try
            {
                registroUID = _unitOfWork.ResetPassword.GetById(id);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(false);
            }
            //Console.WriteLine(registroUID.ClienteId);
            // validar Guid
            if (registroUID != null) return Ok(true);
            return BadRequest(false);
        }
        [HttpPost]
        public IActionResult ResetPassword([FromBody] ClienteEmail Email)
        {
            if (!ModelState.IsValid) return BadRequest("model state not valid");
            // Recibimos el email.
            // Si el email existe en la base de datos A, sino B.
            Cliente cliente;
            
            try
            {
                cliente = _unitOfWork.Cliente.GetClienteByEmail(Email.ToEmail);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(false);
            }
            // A.1. Generamos un registro en la tabla auxiliar con: clienteId, uid, fecha de Modifcacion.
            if (cliente == null) return BadRequest("El cliente es null");
            string registroreset = _unitOfWork.ResetPassword.Insert(cliente.Id);
            // A.2. Le enviamos un correo con un link temporal.
            bool enviado = sendEmail(cliente.Email,cliente.Nombre, registroreset);

            if (enviado) return Ok(true);
            return BadRequest(false);
        }
        [HttpPut]
        public IActionResult Put([FromBody] cambiarPassword cambiarPassword)
        {
            if (!ModelState.IsValid) return BadRequest("Error en el envio");
            // Validar password
            if (cambiarPassword.nuevoPassword.Length < 7) return BadRequest("Debes otorgar un pasword valido");
            // Tomar Registro en tabla reset pasword.
            var registroUID = _unitOfWork.ResetPassword.GetById(cambiarPassword.uid);
            // validar Guid
            if (!validarUid(cambiarPassword.uid, registroUID.ClienteId)) return BadRequest("Utiliza el link enviado a tu correo");
            // Tomar password del cliente correspondiente.
            var passwordAnterior = _unitOfWork.Cliente.GetPasswordByClienteId(registroUID.ClienteId);
            // Objeto tipo Password con nuevo password
            passwordAnterior.Hash = Encriptador.Encrypt(cambiarPassword.nuevoPassword, passPhrase);
            // Realiza update
            if (_unitOfWork.Password.Update(passwordAnterior))
            {
               return Ok( _unitOfWork.ResetPassword.Delete(registroUID));
               // return Ok(true);
            }
            return BadRequest("Ha ocurrido un error, intentalo nuevamente en unos minutos");
        }
        private bool sendEmail(string ToEmail, string name, string uid)
        {
            if (ToEmail == null) return false;
            // Creacion del objeto emailMessage

            MailMessage mailMessage = new MailMessage("enzomzaocv@gmail.com", ToEmail);
            // Creacion del Cuerpo del email. Es el mensaje que enviaremos.
            StringBuilder emailBody = new StringBuilder();
            emailBody.Append($"Estimado {name},<br/><br/>");
            emailBody.Append("Por favor haz click en el siguiente link para reestablecer tu contraseña");
            emailBody.Append("<br/>");
            emailBody.Append("http://localhost:4200/ingreso/cambiarpassword/?id="+uid);
            emailBody.Append("<br/><br/>");
            emailBody.Append("El buen sabor SA. <br/>");
            emailBody.Append("Gerente regional Enzo Chacon <br/>");
            // Asignar el body del email a el objeto emailMessage
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = emailBody.ToString();
            // Agregamos el ASUNTO
            mailMessage.Subject = "Recuperacion de contraseña";
            // Creacion del cliente SPTM
            SmtpClient smptClient = new SmtpClient("smtp.gmail.com", 587);
            smptClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "email@gmail.com",
                Password = "password"
            };
            smptClient.EnableSsl = true;
            // Enviamos el mensaje
            try
            {
                smptClient.Send(mailMessage);
                return true;
            }catch(Exception e)
            {
               // Console.WriteLine(e);
                return false;
            }
            
        }
        private bool validarUid(string uid,int clienteId)
        {
            if (uid == null) return false;

            return _unitOfWork.ResetPassword.ValidarUID(uid,clienteId);
        }
    }
}
