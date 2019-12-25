using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ForumService.Configuration;
using ForumService.Data;
using ForumService.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RestSharp;

namespace ForumService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IStringToHtmlHelper, StringToHtmlHelper>();
            services.AddDbContext<ForumServiceContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ForumService")));
            services.AddMvc(
                    options => { options.EnableEndpointRouting = false; })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)

                ;

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            var request = new RestRequest(Configuration.GetValue<string>("TokenServiceRSAAddress"), Method.GET);
            var parameters = new RestClient().Execute<RSAPublicParameters>(request).Data;
            var rsa = RSA.Create(new RSAParameters
            {
                Exponent = Convert.FromBase64String(parameters.Exponent),
                Modulus = Convert.FromBase64String(parameters.Modulus)
            });
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new RsaSecurityKey(rsa),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
