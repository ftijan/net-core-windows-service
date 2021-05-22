using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WinService
{
    /// <summary>
    /// A <see cref="BackgroundService"/> implementation example.
    /// </summary>
    public class Worker : BackgroundService
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private readonly ILogger<Worker> _logger;

        /// <summary>
        /// The configuration instance.
        /// </summary>
        private readonly IConfiguration _configuration;
        
        /// <summary>
        /// The ncrontab schedule instance.
        /// </summary>
        private CrontabSchedule CrontabSchedule { get; }

        /// <summary>
        /// The ncrontab cron expression parse options.
        /// Uses the non-standard second-including parsing scheme.
        /// </summary>
        private static readonly CrontabSchedule.ParseOptions ParseOptionsWithSeconds = new() { IncludingSeconds = true };

        /// <summary>
        /// Creates an instance of <see cref="Worker"/>.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="configuration">The configuration instance.</param>
        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            // Parse the cron expression to establish the schedule
            CrontabSchedule = CrontabSchedule.TryParse(_configuration["NcrontabScheduleExpression"], ParseOptionsWithSeconds);
        }

        /// <summary>
        /// Executes the service process.
        /// </summary>
        /// <param name="stoppingToken">
        /// The <see cref="CancellationToken"/> instance indicating a system-requested service shutdown.
        /// </param>
        /// <returns>An awaitable <see cref="Task"/>.</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Get the next run according to schedule
                var nextRun = CrontabSchedule.GetNextOccurrence(DateTime.Now);

                // If now is past the schedule, run the process
                if (DateTime.Now > nextRun)
                {
                    // Do work
                    _logger.LogInformation("Worker running at: {time}", DateTime.Now);                    
                }                
                
                // Set the delay to a value that will prevent an overly late wake-up
                // This is not strictly necessary, but will prevent the service from constantly checking
                // whether it needs to run and save some resources.
                await Task.Delay(500, stoppingToken);
            }
        }
    }    
}
