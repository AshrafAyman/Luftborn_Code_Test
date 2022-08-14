using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class Policies
    {
        //--- create policy for authorization
        public static (AuthorizationPolicy Policy, string PolicyName) InstructorPolicy()
        {
            var Policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .RequireRole(ConstantHelper.InstructorRole)
                            .Build();

            var PolicyName = ConstantHelper.InstructorRole;
            return (Policy, PolicyName);
        }
    }
}
