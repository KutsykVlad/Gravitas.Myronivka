using System;
using HangfireDemo.Jobs;
using HangfireDemo.Jobs.Providers;
using Microsoft.Extensions.Configuration;

namespace JobScheduler.Jobs.Providers
{
  public class JobFactory : IJobFactory
  {
    public JobFactory(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public IJob GetInstance(string jobName)
    {
      var className = "JobScheduler.Jobs." + jobName.Replace(" ", "");
      Type type = null;

      try
      {
        type = Type.GetType(className, true);
      }
      catch (Exception)
      {
        //TODO:: Log this error
        return null;
      }

      if (type == null || !(Activator.CreateInstance(type, _configuration) is IJob instance))
      {
        return null;
      }

      return instance;
    }

    private readonly IConfiguration _configuration;
  }
}
