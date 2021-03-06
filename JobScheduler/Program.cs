using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace JobScheduler
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BuildWebHost(args).Run();
    }

  public static IWebHost BuildWebHost(string[] args) =>
    WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration((webHostBuilderContext, configurationbuilder) =>
      {
        configurationbuilder.AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
        configurationbuilder.AddEnvironmentVariables();
      })
      .UseStartup<Startup>()
      .Build();
  }
}
