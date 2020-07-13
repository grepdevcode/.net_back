using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.UnitOfWork;
using ProyectoWeb.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoWeb.Controllers
{
    [Route("api/Token")]
    public class TokenController : Controller
    {
        private ITokenProvider _tokenProvider;
        private IUnitOfWork _unitOfWork;
        private string passPhrase = "enzo";
        public TokenController(ITokenProvider tokenProvider, IUnitOfWork unitOfWork)
        {
            _tokenProvider = tokenProvider;
            _unitOfWork = unitOfWork;
        }
        public class Login
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        [HttpPost]
        public JsonWebToken Post([FromBody] Login userLogin)
        {
            var user = VerificarPassword(userLogin.Email, userLogin.Password);
            //var user = _unitOfWork.Cliente.ValidarCliente(userLogin.Email, password);
            if (user == null) throw new UnauthorizedAccessException();

            var token = new JsonWebToken
            {
                AccessToken = _tokenProvider.CreateToken(user, DateTime.UtcNow.AddHours(8)),
                Expires_in = 480
            };
            return token;
        }

        private Cliente VerificarPassword(string email, string password)
        {
            Cliente cliente;
            try
            {
                cliente = _unitOfWork.Cliente.GetClienteByEmail(email);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            if(cliente != null)
            {
                Password storedPassword = _unitOfWork.Cliente.GetPasswordByClienteId(cliente.Id);
                string cleanPassword = Encriptador.Decrypt(storedPassword.Hash,passPhrase);
                Console.WriteLine("password desencrip  "+cleanPassword );
                if (cleanPassword.Equals(password))
                {
                    return cliente;
                }
            }
            return null;
        }
    }
}
