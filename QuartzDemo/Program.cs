using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangfireDemo.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using QuartzDemo.Logging;

namespace QuartzDemo
{
  class Program
  {
    static void Main(string[] args)
    {
      LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

      var scheduler = RunProgramRunExample().GetAwaiter().GetResult();

      Console.WriteLine("Press any key to close the application");
      Console.ReadKey();

      ShutDownSheduler(scheduler).Wait();
    }

    private static async Task ShutDownSheduler(IScheduler scheduler)
    {
      if (scheduler != null)
      {
        await scheduler.Shutdown();
      }
    }

    private static async Task<IScheduler> RunProgramRunExample()
    {
      IScheduler scheduler = null;
      try
      {
        // Grab the Scheduler instance from the Factory
        NameValueCollection props = new NameValueCollection
        {
          { "quartz.serializer.type", "binary" }
        };
        StdSchedulerFactory factory = new StdSchedulerFactory(props);
        scheduler = await factory.GetScheduler();

        // and start it off
        await scheduler.Start();

        // define the job and tie it to our HelloJob class
        IJobDetail job = JobBuilder.Create<HelloJob>().WithIdentity("job1", "group1").Build();

        // Trigger the job to run now, and then repeat every 10 seconds
        ITrigger trigger = TriggerBuilder.Create().WithIdentity("trigger1", "group1")
          .StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
          .Build();

        // Tell quartz to schedule the job using our trigger
        await scheduler.ScheduleJob(job, trigger);

        // some sleep to show what's happening
        //await Task.Delay(TimeSpan.FromSeconds(60));

        // and last shut down the scheduler when you are ready to close your program
        
      }
      catch (SchedulerException se)
      {
        Console.WriteLine(se);
      }

      return scheduler;
    }
  }
}
