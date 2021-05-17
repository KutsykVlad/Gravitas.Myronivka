using System;
using Microsoft.Extensions.Configuration;

namespace JobScheduler.Jobs
{
    public interface IJob
    {
      String Description { get; }
      IConfiguration Configuration { get; set; }

      void Execute();
    }
}
