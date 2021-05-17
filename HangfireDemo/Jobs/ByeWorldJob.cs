using System;
using HangfireJobs;

namespace HangfireDemo.Jobs
{
  public class ByeWorldJob : IJob
  {
    public String Description { get; } = "AAAA";

    public void Execute()
    {
      var fileWriter = new FileWriter();
      fileWriter.WriteToFile(@"C://temp/hangfire/hang_output.txt", "Bye world");
    }
  }
}
