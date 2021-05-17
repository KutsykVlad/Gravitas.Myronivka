using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireDemo.Jobs.Providers
{
  public interface IJobFactory
  {
    IJob GetInstance(String jobName);
  }
}
