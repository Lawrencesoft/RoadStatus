using RoadStatus.ApiClient;
using RoadStatus.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RoadStatus.ApiClient.RoadStatus;
using Serilog;

namespace RoadStatus
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //setup the rolling logfile
            var serilogLogger = new LoggerConfiguration().WriteTo.RollingFile("logs/tfl_road_logs.txt").CreateLogger();

            //setup our Dependancy Injection and log level
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Information);
                    builder.AddSerilog(logger: serilogLogger, dispose: true);
                })
                .AddSingleton<IRoadStatusService, RoadStatusService>()
                .AddSingleton<IRoadStatusApiClient, RoadStatusApiClient>()
                .AddSingleton<IRoadStatusClientFactory, RoadStatusClientFactory>()
                .BuildServiceProvider();


            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogInformation("Starting the application");

            //invoke the road status service
            var roadService = serviceProvider.GetService<IRoadStatusService>();
            roadService.GetRoadStatus(args);

            logger.LogInformation("All done! completed the application");
        }
    }
}
