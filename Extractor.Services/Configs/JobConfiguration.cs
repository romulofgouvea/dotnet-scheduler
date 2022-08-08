using Extractor.Services.Extensions;
using Extractor.Services.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Extractor.Services.Configs
{
    public static class JobConfiguration
    {
        public static void CronJobConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddCronJob<JobSyncBd>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = config["Params:CronJobSyncBd"];
            });
        }
    }
}
