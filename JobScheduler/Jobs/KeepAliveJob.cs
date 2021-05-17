using System;
using System.Net.Http;
using JobScheduler.Jobs.Providers;
using Microsoft.Extensions.Configuration;

namespace JobScheduler.Jobs
{
    public class KeepAliveJob : AbstrctJob
    {
      public KeepAliveJob(IConfiguration config) : base(config)
      {
          
      }

      public override String Description { get; } = "System job to keep the server alive";

      public override void Execute()
      {
        new HttpClient {BaseAddress = new Uri(Configuration["ShcedulerUrl"]) }.GetAsync("/sceduler/refresh");
      }
    }
}
