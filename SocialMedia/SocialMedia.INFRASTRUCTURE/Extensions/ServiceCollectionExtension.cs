namespace SocialMedia.INFRASTRUCTURE.Extensions
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using SocialMedia.CORE.CustomEntities;
    using SocialMedia.CORE.Interfaces;
    using SocialMedia.CORE.Services;
    using SocialMedia.INFRASTRUCTURE.Data;
    using SocialMedia.INFRASTRUCTURE.Interfaces;
    using SocialMedia.INFRASTRUCTURE.Options;
    using SocialMedia.INFRASTRUCTURE.Repositories;
    using SocialMedia.INFRASTRUCTURE.Services;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;

    public static class ServiceCollectionExtension
    {
        // Conexion a BBDD
        // ----------------
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SocialMediaContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SocialMEdia"))
            );
            return services;
        }

        // Configuraciones especiales
        // --------------------------
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationOptions>(options => configuration.GetSection("Pagination").Bind(options));
            services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOption").Bind(options));

            return services;
        }

        // Configuraciones especiales
        // --------------------------
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            // Inyeccion de dependencias
            // -------------------------
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IPasswordService, PasswordSevice>();
            services.AddSingleton<IUriService>(provaider =>
            {
                var accesor = provaider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });
            //services.AddTransient<IPostRepository, PostMongoRepository>();

            return services;
        }

        // Inyeccion Swagger
        // ------------------
        public static IServiceCollection AddSwagger(this IServiceCollection services, string xmlFileName)
        {
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("V1", new OpenApiInfo { Title = "Social Media API", Version = "V1" });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                doc.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
