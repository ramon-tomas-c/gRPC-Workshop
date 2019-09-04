using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoClient
{
    public static class JwtToken
    {
        public static string Get()
        {
            // Define const Key this should be private secret key  stored in some safe place
            string key = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";

            // Create Security key  using private key above:
            // not that latest version of JWT using Microsoft namespace instead of System
            var securityKey = new Microsoft
                .IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials
                             (securityKey, SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(credentials);
            var payload = new JwtPayload(
                 "",
                 "",
                 null,
                 DateTime.UtcNow,
                 DateTime.UtcNow.AddSeconds(10000));


            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(secToken);
        }
    }
}
