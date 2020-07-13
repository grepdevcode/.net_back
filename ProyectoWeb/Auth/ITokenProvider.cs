using Microsoft.IdentityModel.Tokens;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoWeb.Auth
{
    public interface ITokenProvider
    {
        string CreateToken(Cliente user, DateTime expiry);
        TokenValidationParameters GetValidationParameters();
    }
}
