using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  public class ConsoleHostedService : IHostedService
  {
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;

    private int? _exitCode;

    public ConsoleHostedService(
        ILogger<ConsoleHostedService> logger,
        IHostApplicationLifetime appLifetime)
    {
      _logger = logger;
      _appLifetime = appLifetime;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

      _appLifetime.ApplicationStarted.Register(() =>
      {
        Task.Run(async () =>
        {
          try
          {
            Console.CancelKeyPress += async (sender, e) =>
            {
              Console.WriteLine("Cancelling");
              Environment.Exit(0);
            };

            Console.WriteLine("Select a sample:");
            Console.WriteLine("");
            Console.WriteLine("1. Diagnostics");


            var choice = Console.ReadLine();

            switch (choice)
            {
              case "1":
                await Diagnostics.Run();
                break;
              //await RootSite.Run();

              default:
                break;
            }

            var something = System.Console.ReadLine();

            _logger.LogInformation($"You entered '{something}'");

            _exitCode = 0;
          }
          catch (Exception ex)
          {
            _logger.LogError(ex, "Unhandled exception!");
            _exitCode = 1;
          }
          finally
          {
            // Stop the application once the work is done
            _appLifetime.StopApplication();
          }
        });
      });

      return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      _logger.LogDebug($"Exiting with return code: {_exitCode}");

      // Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
      Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
      return Task.CompletedTask;
    }
  }
}
