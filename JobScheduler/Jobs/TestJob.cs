using System;
using HangfireDemo.Jobs;
using HangfireJobs;
using JobScheduler.Jobs.Providers;
using Microsoft.Extensions.Configuration;

namespace JobScheduler.Jobs
{
  public class TestJob : AbstrctJob
  {
    public TestJob(IConfiguration config) : base(config)
    {

    }

    public override String Description { get; } = "This is the Test job. Just to be shure Hangfire is working!";

    public override void Execute()
    {
      var fileWriter = new FileWriter();
      fileWriter.WriteToFile(@"C://temp/hangfire/hang_output.txt", Description);
    }
  }
}
