using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class ConstantHelper
    {
        #region JWT Token Variables
        public const string JWTKey = "91EC28C46763C4BB3EEE6CA29E91B4A7244324CA6362BA3282B058E5A284F3C5";
        public const string JWTAlgorithm = SecurityAlgorithms.HmacSha256;
        public const string Audiance = "CourseSystemFrontEnd";
        public const string Issuer = "CourseSystemBackEnd";
        public const int KillAfterInDays = 360;
        #endregion

        #region Roles And Polices Variables
        public const string InstructorRole = "Instructor";
        public const string RoleHeaderName = ClaimTypes.Role;
        #endregion
    }
}
