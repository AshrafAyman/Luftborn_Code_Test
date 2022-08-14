using Data.Models;
using Infrastructure.Configuration;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    
    public class Security : ISecurity
    {
        private readonly ClaimsPrincipal claimsPrincipal;
        public Security(IHttpContextAccessor httpContextAccessor)
        {
            claimsPrincipal = httpContextAccessor.HttpContext.User;
        }
        public string GenerateWebToken(User UserInfo)
        {
            //--- create list of claims that contain properties we want to add in token 
            var tokenClaims = new List<Claim>
            {
                new Claim("UserName", UserInfo.UserName ?? ""),
                new Claim("UserId",UserInfo.Id.ToString()),
                new Claim(ConstantHelper.RoleHeaderName, ConstantHelper.InstructorRole )
            };

            //--- set hashing algorith for token
            var credentials = new SigningCredentials(
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(ConstantHelper.JWTKey)
                                    ), ConstantHelper.JWTAlgorithm);

            //--- create token object 
            var token = new JwtSecurityToken(
                    issuer: ConstantHelper.Issuer,
                    audience: ConstantHelper.Audiance,
                    expires: DateTime.Now.AddDays(ConstantHelper.KillAfterInDays),
                    claims: tokenClaims,
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //--- get user id that we add to token 
        public int GetUserIdFromToken()
        {
            return GetValueFromToken<int>("UserId");
        }

        //--- get value from token based on key that we set
        private T GetValueFromToken<T>(string value)
        {
            string TokenValue = "";
            if (claimsPrincipal != null)
            {
                TokenValue = claimsPrincipal.Claims.Where(c => c.Type == value).Select(c => c.Value).SingleOrDefault();
            }
            T tokenValue = (T)Convert.ChangeType(TokenValue, typeof(T));
            return tokenValue;
        }
    }
}
