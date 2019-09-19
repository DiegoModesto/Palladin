﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Palladin.Presentation.API.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssemby(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes
                .Where(x =>
                   typeof(IInstaller).IsAssignableFrom(x) &&
                   !x.IsInterface &&
                   !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();

            installers.ForEach(install => install.InstallServices(services, configuration));
        }
    }
}