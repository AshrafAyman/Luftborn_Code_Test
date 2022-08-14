using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extenstions
{
    //--- extenstion method for database service injection
    public static class DBContextInjection
    {
        public static void InjectDBContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SystemContext>(option =>
            {
                option.UseSqlServer(connectionString, config =>
                {
                    config.EnableRetryOnFailure(3);
                });
            });
        }
    }
}
