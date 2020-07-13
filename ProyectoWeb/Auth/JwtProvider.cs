using Microsoft.IdentityModel.Tokens;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ProyectoWeb.Auth
{
    public class JwtProvider : ITokenProvider
    {
        private RsaSecurityKey _key { get; set; }
        private string _algorithm { get; set; }
        private string _issuer { get; set; }
        private string _audience { get; set; }
        public JwtProvider(string issuer, string audience, string keyName)
        {
            var parameters = new CspParameters() { KeyContainerName = keyName };
            var provider = new RSACryptoServiceProvider(2048, parameters);
            _key = new RsaSecurityKey(provider);
            _algorithm = SecurityAlgorithms.RsaSha256Signature;
            _issuer = issuer;
            _audience = audience;

        }
        public string CreateToken(Cliente cliente, DateTime expiry)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, $"{cliente.Email}"),
                    new Claim(ClaimTypes.Role, cliente.Roles),
                    new Claim(ClaimTypes.PrimarySid, cliente.Id.ToString())
                }, "Custom"
            );
            SecurityToken token = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Audience = _audience,
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(_key, _algorithm),
                Expires = expiry.ToUniversalTime(),
                Subject = identity
            });
            return tokenHandler.WriteToken(token);
        }

        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                IssuerSigningKey = _key,
                ValidAudience = _audience,
                ValidIssuer = _issuer,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(0)
            };
        }
    }
}
