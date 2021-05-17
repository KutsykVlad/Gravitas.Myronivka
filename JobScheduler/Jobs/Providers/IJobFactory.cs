using System;
using HangfireDemo.Jobs;

namespace JobScheduler.Jobs.Providers
{
  public interface IJobFactory
  {
    IJob GetInstance(String jobName);
  }
}
