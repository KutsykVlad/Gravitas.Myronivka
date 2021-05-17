using System;
using HangfireJobs;

namespace HangfireDemo.Jobs
{
  public class HelloWorldJob : IJob
  {
    public String Description { get; } = "BBBB";

    public void Execute()
    {
      var fileWriter = new FileWriter();
      fileWriter.WriteToFile(@"C://temp/hangfire/hang_output.txt", "Hello world");
    }
  }
}
