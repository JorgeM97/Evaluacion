using Evaluacion.Modelos.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Evaluacion.Helpers;
using Evaluacion.Services;
using Evaluacion.Repositories;
using Evaluacion.Helpers.Interfaces;
using System;
using Microsoft.Extensions.Logging;

namespace Evaluacion
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IInventarioServices, InventarioServices>();
            services.AddScoped<IInventarioRepository, InventarioRepository>();
            services.AddScoped<IRegistroErroesRepository, RegistroErroresRepository>();
            services.AddScoped<IVentasService, VentasService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentasRepository, VentasRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();

            services.AddDbContext<PruebasContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Inventario"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                        );                    
                }
                ));

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Evalucación API",
                    Version = "v1",
                    Description = "Evaluación versión 1.0",
                    Contact = new OpenApiContact
                    {
                        Name = "Jorge Méndez",
                        Email = "mendez.jorgei99@gmail.com",
                        Url = new System.Uri("https://duckduckgo.com/?t=brave"),
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddLog4Net("log4net.config");

            app.UseSwagger();

            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Evaluacion API V1");
                swagger.RoutePrefix = string.Empty;
            }
            );

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
