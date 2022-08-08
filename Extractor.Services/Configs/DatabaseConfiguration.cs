using Extractor.Infra;
using Extractor.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Extractor.Services.Configs
{
    public static class DatabaseConfiguration
    {
        public static void DapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IDapperConnection, DapperConnection>();
        }
    }
}
