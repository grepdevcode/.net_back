using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoWeb.Auth
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public string Token_Type { get; set; } = "bearer";
        public int Expires_in { get; set; }
        public string Refresh_Token { get; set; }
    }
}
