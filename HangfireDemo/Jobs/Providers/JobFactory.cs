using System;

namespace HangfireDemo.Jobs.Providers
{
  public class JobFactory : IJobFactory
  {
    public IJob GetInstance(string jobName)
    {
      var className = "HangfireDemo.Jobs." + jobName.Replace(" ", "");
      Type type = null;

      try
      {
        type = Type.GetType(className, true);
      }
      catch (Exception)
      {
        //TODO:: Log this error
        return null;
      }

      if (type == null || !(Activator.CreateInstance(type) is IJob instance))
      {
        return null;
      }

      return instance;
    }
  }
}
