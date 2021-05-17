using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace HangfireDemo.Jobs
{
  class HelloJob : IJob
  {
    public async Task Execute(IJobExecutionContext context)
    {
      await Console.Out.WriteLineAsync($"{DateTime.UtcNow}: Greetings from HelloJob!");
    }
  }
}
