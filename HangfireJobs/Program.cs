using System;
using Gravitas.Infrastructure.Common.Configuration;

namespace HangfireJobs
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("This library is used only for executing scheduled jobs.");
      Console.WriteLine($"Settings: Test: {GlobalConfigurationManager.Test}");
      Console.WriteLine("Press any key to exit...");

      Console.ReadKey();
    }
  }
}
