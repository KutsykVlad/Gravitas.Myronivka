using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangfireDemo.DTO;

namespace HangfireDemo.Jobs.Providers
{
  public abstract class AbstrctJob : IJob
  {
    public abstract string Description { get; }

    public abstract void Execute();
  }
}
