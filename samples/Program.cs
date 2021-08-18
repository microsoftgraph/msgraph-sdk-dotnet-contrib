using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  class Program
  {
    static async Task Main(string[] args)
    {
      using var host =
        Host.CreateDefaultBuilder(args)
              .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
              .UseEnvironment("Development")
              .ConfigureServices((hostContext, services) =>
              {
                services.AddSingleton<MenuService>();

                services.AddOptions<AzureAdSettings>()
                  .Configure<IConfiguration>((settings, configuration) =>
                  {
                    configuration.GetSection(AzureAdSettings.ConfigurationSectionName).Bind(settings);
                  });

                services.AddOptions<SharePointSettings>()
                  .Configure<IConfiguration>((settings, configuration) =>
                  {
                    configuration.GetSection(SharePointSettings.ConfigurationSectionName).Bind(settings);
                  });

                // Add our sample classes
                services.AddTransient<Diagnostics>();
                services.AddTransient<RootSite>();
                services.AddTransient<ExpiringClientSecrets>();
                //services.AddTransient<Chan>();
              })
              .Build();

      await host.StartAsync();

      var menu = host.Services.GetRequiredService<MenuService>();
      menu.StartMenuLoop(host.Services);

      await host.WaitForShutdownAsync();
    }
  }
}
