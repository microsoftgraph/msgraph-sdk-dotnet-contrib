using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Graph.Community.Samples
{
  public class MenuService
  {
    private readonly ILogger logger;
    private readonly CancellationToken cancellationToken;
    private IServiceProvider serviceProvider;

    public MenuService(
        ILogger<MenuService> logger,
        IHostApplicationLifetime applicationLifetime)
    {
      this.logger = logger;
      cancellationToken = applicationLifetime.ApplicationStopping;
    }

    public void StartMenuLoop(IServiceProvider serviceProvider)
    {
      logger.LogDebug($"{nameof(MenuService)} is starting.");

      this.serviceProvider = serviceProvider;

      // Run a console user input loop in a background thread
      Task.Run(async () => await MenuAsync());
    }

    private async ValueTask MenuAsync()
    {
      while (!cancellationToken.IsCancellationRequested)
      {
        Console.WriteLine("");
        Console.WriteLine("Select a sample:");
        Console.WriteLine("");
        Console.WriteLine("1. Diagnostics");
        Console.WriteLine("2. Root site (Graph)");
        Console.WriteLine("3. Expiring client secrets");
        Console.WriteLine("4. Change log");
        Console.WriteLine("5. Site Groups");
        Console.WriteLine("6. SharePoint Search");
        Console.WriteLine("7. Site Design");
        Console.WriteLine("8. Add Member to Group (Graph)");
        //Console.WriteLine("9. ");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("Ctrl+C to Exit");
        Console.WriteLine("");

        var keyStroke = Console.ReadKey();

        Console.WriteLine("");

        try
        {
          switch (keyStroke.Key)
          {
            case ConsoleKey.D1:
            case ConsoleKey.NumPad1:
              var diagnosticSample = serviceProvider.GetRequiredService<Diagnostics>();
              await diagnosticSample.Run();
              break;

            case ConsoleKey.D2:
            case ConsoleKey.NumPad2:
              var rootSiteSample = serviceProvider.GetRequiredService<RootSite>();
              await rootSiteSample.Run();
              break;

            case ConsoleKey.D3:
            case ConsoleKey.NumPad3:
              var expiringSecretsSample = serviceProvider.GetRequiredService<ExpiringClientSecrets>();
              await expiringSecretsSample.Run();
              break;

            case ConsoleKey.D4:
            case ConsoleKey.NumPad4:
              var changeLogSample = serviceProvider.GetRequiredService<ChangeLog>();
              await changeLogSample.Run();
              break;

            case ConsoleKey.D5:
            case ConsoleKey.NumPad5:
              var siteGroupsSample = serviceProvider.GetRequiredService<SiteGroups>();
              await siteGroupsSample.Run();
              break;

            case ConsoleKey.D6:
            case ConsoleKey.NumPad6:
              var searchSample = serviceProvider.GetRequiredService<SharePointSearch>();
              await searchSample.Run();
              break;

            case ConsoleKey.D7:
            case ConsoleKey.NumPad7:
              var siteDesignSample = serviceProvider.GetRequiredService<SiteDesign>();
              await siteDesignSample.Run();
              break;

            case ConsoleKey.D8:
            case ConsoleKey.NumPad8:
              var graphGroupSample = serviceProvider.GetRequiredService<GraphGroupExtensions>();
              await graphGroupSample.Run();
              break;

            case ConsoleKey.D9:
            case ConsoleKey.NumPad9:
              break;
            default:
              break;
          }

        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
      }
    }
  }
}
