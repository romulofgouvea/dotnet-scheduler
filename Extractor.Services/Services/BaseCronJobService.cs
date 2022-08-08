using Cronos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Extractor.Services.Services
{
    public abstract class BaseCronJobService : IHostedService, IDisposable
    {
        private System.Timers.Timer _timer;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;
        private readonly ILogger _logger;

        protected BaseCronJobService(string cronExpression, TimeZoneInfo timeZoneInfo, ILogger logger)
        {
            _expression = CronExpression.Parse(cronExpression);
            _timeZoneInfo = timeZoneInfo;
            _logger = logger;
        }

        public virtual async Task StartAsync(CancellationToken cancelationToken)
        {
            _logger.LogInformation($"Loaded {GetType().Name} {DateTime.Now}");
            await ScheduleJob(cancelationToken);
        }

        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;

                if (delay.TotalMilliseconds <= 0) // prevent non-positive values from being passed into Timer
                {
                    await ScheduleJob(cancellationToken);
                }
                _timer = new System.Timers.Timer(delay.TotalMilliseconds);
                _timer.Elapsed += async (sender, args) =>
                {
                    _timer.Dispose(); // reset and dispose timer
                    _timer = null;

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        _logger.LogInformation($"Timed {GetType().Name} Service is working.  {DateTime.Now}");
                        try
                        {
                            await DoWork(cancellationToken);
                            _logger.LogInformation($"Timed {GetType().Name} Service completed.  {DateTime.Now}");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation($"Timed {GetType().Name} Service failed.  {DateTime.Now}");
                            Console.WriteLine("Error: \n" + ex);
                        }
                    }
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await ScheduleJob(cancellationToken); // reschedule next
                    }
                };
                _timer.Start();
            }
            await Task.CompletedTask;
        }

        public virtual async Task DoWork(CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken); // do the work
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            await Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
