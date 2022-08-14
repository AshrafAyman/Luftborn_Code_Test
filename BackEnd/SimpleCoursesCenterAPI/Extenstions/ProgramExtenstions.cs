using Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace SimpleCoursesCenterAPI.Extenstions
{
    public static class ProgramExtenstions
    {
        public static void InjectAuthLayer(this IServiceCollection services)
        {
            var (instructorPolicy, instructorPolicyName) = Policies.InstructorPolicy();

            services.AddAuthorization(config =>
            {
                config.AddPolicy(instructorPolicyName, instructorPolicy);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.RequireHttpsMetadata = false;
                                options.SaveToken = true;
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = ConstantHelper.Issuer,
                                    ValidAudience = ConstantHelper.Audiance,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConstantHelper.JWTKey)),
                                    ClockSkew = TimeSpan.Zero
                                };
                            });

        }
        public static void InjectMvcLayer(this IServiceCollection services)
        {
            services.AddMvcCore()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                    opt.SerializerSettings.Formatting = Formatting.Indented;
                    opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                });
        }
        public static void InjectCorsLayer(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
        }
    }
}
