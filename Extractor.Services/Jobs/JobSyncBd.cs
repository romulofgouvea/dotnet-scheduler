using Dapper;
using Extractor.Domain.Repositories;
using Extractor.Infra.Repositories;
using Extractor.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Extractor.Services.Jobs
{
    public class JobSyncBd : BaseCronJobService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<JobSyncBd> _logger;
        public JobSyncBd(
            IScheduleConfig<JobSyncBd> config,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<JobSyncBd> logger) : base(config.CronExpression, config.TimeZoneInfo, logger)
        {
            _logger = logger;
            _scopeFactory = serviceScopeFactory;
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<IDapperConnection>();

                using var db = _db.GetConnection();
                var sql = "SELECT Id FROM Usuario";
                List<Guid> ids = db.Query<Guid>(sql).ToList();

                Console.WriteLine(sql);
                Console.WriteLine("Result: " + String.Join("\n", ids));
            }


            Console.WriteLine("DoWork: " + DateTime.Now.ToString("O"));

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
