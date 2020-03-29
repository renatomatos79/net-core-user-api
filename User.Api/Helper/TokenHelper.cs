using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User.Api.Domain;

namespace User.Api.Helper
{
    public static class TokenHelper
    {
        public static SecurityToken CreateSecurityToken(TokenDomainModel model, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var roles = JsonConvert.SerializeObject(model.Roles);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, model.Name),
                    new Claim(ClaimTypes.Role, roles)
                }),
                Expires = model.ExpiresOn,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.CreateToken(tokenDescriptor);            
        }

        public static string ToJwtString(this SecurityToken securityToken)
        {
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public static string CreateStringSecurityToken(TokenDomainModel model, string secret)
        {
            return CreateSecurityToken(model, secret).ToJwtString();
        }

    }
}
