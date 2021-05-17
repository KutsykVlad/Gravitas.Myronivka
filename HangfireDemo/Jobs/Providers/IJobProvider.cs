using System;
using System.Collections.Generic;
using HangfireDemo.ViewModels;

namespace HangfireDemo.Jobs.Providers
{
  public interface IJobProvider
  {
    IEnumerable<String> GetJobNames();
    IList<JobViewModel> GetJobViewModelList();
  }
}
