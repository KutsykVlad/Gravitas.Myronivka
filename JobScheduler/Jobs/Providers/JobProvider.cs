using System;
using System.Collections.Generic;
using System.Linq;
using HangfireDemo.ViewModels;
using JobScheduler.Jobs;
using JobScheduler.Jobs.Providers;

namespace HangfireDemo.Jobs.Providers
{
  public class JobProvider : IJobProvider
  {
    public IEnumerable<String> GetJobNames()
    {
      return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
        .Where(type => typeof(IJob).IsAssignableFrom(type) && !type.IsAbstract && type.IsClass).Select(jobType => jobType.Name);
    }

    public IList<JobViewModel> GetJobViewModelList()
    {
      return GetJobNames().Select(jobName => new JobViewModel { Name = jobName }).ToList();
    }
  }
}
