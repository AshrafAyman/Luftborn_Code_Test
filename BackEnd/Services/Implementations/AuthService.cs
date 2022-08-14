using Data.Context;
using Data.Models;
using Data.UIModels;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ISecurity security;
        private readonly SystemContext context;

        public AuthService(ISecurity _security, SystemContext _context)
        {
            security = _security;
            context = _context;
        }

        public ResponseDto AuthenticateLogin(UserDto user)
        {
            //--- recive user credentials in UserDto and start authenticate it
            var response = new ResponseDto();
            var validUser = GetAuthenticatedUser(user);
            if (validUser == null)
            {
                response.Status = false;
                response.Message = "Email or password is incorrect";
                return response;
            }
            //--- create user token if it's authenticated successfully
            string token = security.GenerateWebToken(validUser);
            response.Status = true;
            response.Message = token;
            return response;
        }

        public User? GetAuthenticatedUser(UserDto user)
        {
            //--- get user data based on his email
            var isAuthonticated = context.Users.FirstOrDefault(e => e.Email.ToUpper() == user.Email.ToUpper());

            if (isAuthonticated != null)
            {
                //--- verify hashed password 
                var isPasswordVarified = new PasswordHasher<User>();
                var result = isPasswordVarified.VerifyHashedPassword(isAuthonticated, isAuthonticated.Password, user.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    return isAuthonticated;
                }
                else if (result == PasswordVerificationResult.Failed)
                {
                    return null;
                }
            }

            return null;
        }
    }
}
