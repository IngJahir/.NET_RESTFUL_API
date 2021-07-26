namespace SocialMedia.API
{
    using AutoMapper;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using SocialMedia.CORE.CustomEntities;
    using SocialMedia.CORE.Interfaces;
    using SocialMedia.CORE.Services;
    using SocialMedia.INFRASTRUCTURE.Data;
    using SocialMedia.INFRASTRUCTURE.Extensions;
    using SocialMedia.INFRASTRUCTURE.Filters;
    using SocialMedia.INFRASTRUCTURE.Interfaces;
    using SocialMedia.INFRASTRUCTURE.Options;
    using SocialMedia.INFRASTRUCTURE.Repositories;
    using SocialMedia.INFRASTRUCTURE.Services;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;

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
            // AutoMapper
            // ----------
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Se adiciona .AddNewtonsoftJson para evitar referencia circular
            // Se adiciona .ConfigureApiBehaviorOptions para suprimir la validacion del modelo del API
            // ---------------------------------------------------------------------------------------
            services.AddControllers(options =>
                {
                    options.Filters.Add<GlobalExceptionFilter>();
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });
            //.ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

            // Configuraciones especiales y Conexion a BBDD
            // --------------------------------------------
            services.AddOptions(Configuration);

            // Conexion a BBDD
            // -----------------
            services.AddDbContexts(Configuration);

            // Inyeccion de dependencias
            // -------------------------
            services.AddServices();

            // Inyeccion Swagger
            // ------------------
            services.AddSwagger($"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

            // Inyeccion JWT: SIEMPRE ANTES DE MVC
            // -----------------------------------
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Authentication:Issuer"],
                        ValidAudience = Configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]))
                    };
                });

            // Inyeccion de Filters
            // ---------------------
            services
                .AddMvc(options => { options.Filters.Add<ValidationFilter>(); })
                .AddFluentValidation(options => { options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // Local IIS: Se activa options.SwaggerEndpoint y se descativa options.RoutePrefix
                // -------------------------------------------------------------------------------
                //options.SwaggerEndpoint("../swagger/V1/swagger.json","Social Media API V1");
                //options.RoutePrefix = string.Empty;

                // Azure: Se activa las dos opciones.
                // ----------------------------------
                options.SwaggerEndpoint("/swagger/V1/swagger.json", "Social Media API V1");
                options.RoutePrefix = string.Empty;

            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
