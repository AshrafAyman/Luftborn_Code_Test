using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extenstions
{
    //--- extendtion method to auto inject services that ends with service word
    public static class DependencyInjection
    {
        public static void InjectServiceProjectServices(this IServiceCollection services)
        {
            services.RegisterAssemblyPublicNonGenericClasses()
                    .Where(s => s.Name.EndsWith("Service"))
                    .AsPublicImplementedInterfaces();
        }
    }
}
