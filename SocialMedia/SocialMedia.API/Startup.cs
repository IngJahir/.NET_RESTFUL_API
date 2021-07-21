namespace SocialMedia.API
{
    using AutoMapper;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using SocialMedia.CORE.CustomEntities;
    using SocialMedia.CORE.Interfaces;
    using SocialMedia.CORE.Services;
    using SocialMedia.INFRASTRUCTURE.Data;
    using SocialMedia.INFRASTRUCTURE.Filters;
    using SocialMedia.INFRASTRUCTURE.Interfaces;
    using SocialMedia.INFRASTRUCTURE.Repositories;
    using SocialMedia.INFRASTRUCTURE.Services;
    using System;
    using System.IO;
    using System.Reflection;

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

            // Configuraciones especiales
            // --------------------------
            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));

            // Conexion a BBDD
            // ---------------
            services.AddDbContext<SocialMediaContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SocialMEdia")));

            // Inyeccion de dependencias
            // -------------------------
            services.AddTransient<IPostService, PostService>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IUriService>(provaider =>
           {
               var accesor = provaider.GetRequiredService<IHttpContextAccessor>();
               var request = accesor.HttpContext.Request;
               var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
               return new UriService(absoluteUri);
           });
            //services.AddTransient<IPostRepository, PostMongoRepository>();

            // Inyeccion Swagger
            // ------------------
            services.AddSwaggerGen(doc => 
            {
                doc.SwaggerDoc("V1", new OpenApiInfo { Title = "Social Media API", Version="V1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                doc.IncludeXmlComments(xmlPath);
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
                options.SwaggerEndpoint("/swagger/V1/swagger.json","Social Media API V1");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
