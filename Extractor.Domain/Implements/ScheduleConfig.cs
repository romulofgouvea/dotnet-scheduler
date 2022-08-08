using Extractor.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Extractor.Domain.Implements
{
    public class ScheduleConfig<T> : IScheduleConfig<T>
    {
        public string CronExpression { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
        public ILogger Logger { get; set; }
    }
}
