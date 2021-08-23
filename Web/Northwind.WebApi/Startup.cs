using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Northwind.BussinessLogic.Implementations;
using Northwind.BussinessLogic.Interfaces;
using Northwind.DataAccess;
using Northwind.UnitOfWork;
using Northwind.WebApi.Authentication;
using Northwind.WebApi.GlobalErrorHandling;

namespace Northwind.WebApi
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
            services.AddTransient<ISupplierLogic, SupplierLogic>();
            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<ICustomerLogic, CustomerLogic>();
            services.AddTransient<ITokenLogic, TokenLogic>();
            //cuando iniciamos el programa se crea una
            //instancia de NorthwindUnitOfWork a la cual se le
            //inyecta la cadena de conexion "Northwind"
            //esta instancia se comparte entre todos los
            //que la requieren
            services.AddSingleton<IUnitOfWork>(option => new NorthwindUnitOfWork(
              Configuration.GetConnectionString("Northwind")
            ));

            var tokenProvider = new JwtProvider("issuer","audience","northwind_2000");
            services.AddSingleton<ITokenProvider>(tokenProvider);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
              options.RequireHttpsMetadata = false;
              options.TokenValidationParameters = tokenProvider.GetValidationParameters();
            });
            services.AddAuthorization(auth => {
              auth.DefaultPolicy = new AuthorizationPolicyBuilder()
              .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
              .RequireAuthenticatedUser()
              .Build();
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();
            app.ConfigureExceptionHandler();
            app.UseMvc();
        }
    }
}
