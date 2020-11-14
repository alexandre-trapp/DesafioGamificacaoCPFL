﻿using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DesafioGamificacaoCPFL.Infra.Database.Settings;

namespace DesafioGamificacaoCPFL.Infra.IoC
{
    public static class ResolveDependencies
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(
                    configuration.GetSection(nameof(DatabaseSettings)));

            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
        }
    }
}
