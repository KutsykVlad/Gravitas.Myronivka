using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireDemo.Jobs
{
    public interface IJob
    {
      String Description { get; }

      void Execute();
    }
}
