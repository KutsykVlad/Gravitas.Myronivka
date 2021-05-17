using HangfireDemo.Jobs;
using Microsoft.Extensions.Configuration;

namespace JobScheduler.Jobs.Providers
{
  public abstract class AbstrctJob : IJob
  {    
    protected AbstrctJob(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public abstract string Description { get; }

    public IConfiguration Configuration { get; set; }

    public abstract void Execute();
  }
}
