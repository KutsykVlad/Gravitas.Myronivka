using System;
using Hangfire;
using HangfireDemo.DTO;
using HangfireDemo.Jobs.Providers;
using HangfireDemo.ViewModels;
using JobScheduler.Jobs.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JobScheduler.Controllers
{
  [Route("api/[controller]")]
  public class ScheduleController : Controller
  {
    public ScheduleController(IJobFactory jobFactory, IJobProvider jobProvider, IConfiguration configuration)
    {
      _jobFactory = jobFactory;
      _jobProvider = jobProvider;
      _configuration = configuration;
    }

    [HttpGet]
    public IActionResult JobsList()
    {  
      return View(_jobProvider.GetJobViewModelList());
    }

    [HttpGet("{name}")]
    [Route("JobDetails"), ActionName("JobDetails")]
    public IActionResult JobDetails(String name)
    {
      var jobInstance = _jobFactory.GetInstance(name);
      if (jobInstance == null)
      {
        return NotFound("Job not found!!");
      }

      return View(new JobViewModel { Name = name, Description = jobInstance.Description});
    }

    [HttpGet("{jobName}")]
    [Route("ScheduleJob"), ActionName("ScheduleJobPage")]
    public IActionResult ScheduleJobPage(string jobName)
    {
      return View(new JobScheduleDto { Name = jobName, Schedule ="* * * * *" });
    }

    [HttpPost]
    [Route("ScheduleJob"), ActionName("ScheduleJob")]
    public IActionResult ScheduleJob(JobScheduleDto dto)
    {
      var jobInstance = _jobFactory.GetInstance(dto.Name);
      if (jobInstance == null)
      {
        return NotFound("Job not found!!");
      }

      RecurringJob.AddOrUpdate(() => jobInstance.Execute(), dto.Schedule);

      return RedirectToAction("JobsList");
    }

    [HttpGet]
    [Route("ScheduleAllJobs"), ActionName("ScheduleAllJobs")]
    public IActionResult ScheduleAllJobs()
    {
      foreach (var jobInfo in _jobProvider.GetJobViewModelList())
      {
        var jobInstance = _jobFactory.GetInstance(jobInfo.Name);
        if (jobInstance == null)
        {
          continue;
        }

        var jobSchedule = _configuration[$"Shcedule:{jobInfo.Name}"];
        if (jobSchedule == null)
        {
          continue;
        }

        RecurringJob.AddOrUpdate(() => jobInstance.Execute(), jobSchedule);
      }

      return RedirectToAction("JobsList");
    }

    private readonly IJobFactory _jobFactory;
    private readonly IJobProvider _jobProvider;
    private readonly IConfiguration _configuration;
  }
}
