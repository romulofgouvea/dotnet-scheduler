using Microsoft.Extensions.Logging;

namespace Extractor.Domain.Repositories
{
    public interface IScheduleConfig<T>
    {
        string CronExpression { get; set; }
        TimeZoneInfo TimeZoneInfo { get; set; }
        ILogger Logger { get; set; }
    }
}
