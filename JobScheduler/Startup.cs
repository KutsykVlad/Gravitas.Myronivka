using Hangfire;
using Hangfire.MemoryStorage;
using HangfireDemo.Jobs.Providers;
using JobScheduler.Jobs.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobScheduler
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddHangfire(x => x.UseMemoryStorage());

      services.AddSingleton<IJobProvider, JobProvider>();
      services.AddSingleton<IJobFactory, JobFactory>();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHangfireServer();
      app.UseHangfireDashboard();

      app.UseMvc();
    }
  }
}
