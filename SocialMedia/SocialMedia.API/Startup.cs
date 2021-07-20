namespace SocialMedia.API
{
    using AutoMapper;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using SocialMedia.CORE.Interfaces;
    using SocialMedia.CORE.Services;
    using SocialMedia.INFRASTRUCTURE.Data;
    using SocialMedia.INFRASTRUCTURE.Filters;
    using SocialMedia.INFRASTRUCTURE.Repositories;
    using System;

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
            services.AddControllers(options=> 
                {
                    options.Filters.Add<GlobalExceptionFilter>();
                })
                .AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });
                //.ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

            // Conexion a BBDD
            // ---------------
            services.AddDbContext<SocialMediaContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SocialMEdia")));

            // Inyeccion de dependencias
            // -------------------------
            services.AddTransient<IPostService, PostService>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<IPostRepository, PostMongoRepository>();

            // Inyeccion de Filters
            // ---------------------
            services
                .AddMvc(options => { options.Filters.Add<ValidationFilter>(); })
                .AddFluentValidation(options => { options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
