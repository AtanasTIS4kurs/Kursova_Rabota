using Serilog.Events;
using Serilog;

namespace GameStore.Logging
{
    public class SerilogConfiguration
    {
        public static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.File("Logs/game_store_log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
