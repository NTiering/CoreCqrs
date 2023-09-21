using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Core.Data.Support;

public class DurationLogger<Tcmd>
{
    private readonly Stopwatch stopWatch;
    private readonly Type logType;
    private readonly ILogger logger;

    public DurationLogger(ILogger logger)
    {
        stopWatch = Stopwatch.StartNew();
        logType = typeof(Tcmd);
        this.logger = logger;
    }

    public long StopAndRead()
    {
        stopWatch.Stop();
        var elapsed = stopWatch.ElapsedMilliseconds;
        logger?.LogInformation($"{logType?.Name} : {elapsed}");
        return elapsed;
    }

}
