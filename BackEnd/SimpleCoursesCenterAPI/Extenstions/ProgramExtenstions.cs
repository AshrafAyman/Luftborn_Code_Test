using Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace SimpleCoursesCenterAPI.Extenstions
{
    public static class ProgramExtenstions
    {
        //--- extenstion method to inject authentication and authorization service
        public static void InjectAuthLayer(this IServiceCollection services)
        {
            //--- create policy for instructor
            var (instructorPolicy, instructorPolicyName) = Policies.InstructorPolicy();

            //--- add created policy
            services.AddAuthorization(config =>
            {
                config.AddPolicy(instructorPolicyName, instructorPolicy);
            });

            //--- set configurations for authentication service
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

        //--- extenstion method to inject MVC service it's responsible for the way JSON object will be serialized 
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

        //--- extenstion method to inject CORS service
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
