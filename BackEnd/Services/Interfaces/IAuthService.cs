using Data.Models;
using Data.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAuthService
    {
        ResponseDto AuthenticateLogin(UserDto user);
        User? GetAuthenticatedUser(UserDto user);
    }
}
