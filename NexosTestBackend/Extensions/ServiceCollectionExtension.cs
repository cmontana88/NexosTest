using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexosTestBackend.Core.Interfaces;
using NexosTestBackend.Core.Servicios;
using NexosTestBackend.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexosTestBackend.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, string conexion)
        {
            services.AddTransient<IService<Autor>>(autor => ActivatorUtilities.CreateInstance<AutorService>(autor, conexion));
            services.AddTransient<IService<Editorial>>(editorial => ActivatorUtilities.CreateInstance<EditorialService>(editorial, conexion));
            services.AddTransient<IService<Libro>>(libro => ActivatorUtilities.CreateInstance<LibroService>(libro, conexion));

            return services;
        }
    }
}
