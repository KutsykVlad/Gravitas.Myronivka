using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace HangfireJobs
{
  public class FileWriter
  {
    public async void WriteToFile(String path, String text)
    {
      var newDir = false;
      await semaphoreSlim.WaitAsync();
      try
      {
        var folderPath = Path.GetDirectoryName(path);
        if (!Directory.Exists(folderPath))
        {
          Directory.CreateDirectory(folderPath);
          newDir = true;
        }

        if (newDir || !File.Exists(path))
        {
          using (StreamWriter sw = File.CreateText(path))
          {
            sw.WriteLine($"[{DateTime.UtcNow}]: File created!");
          }
        }
        using (StreamWriter writeStream = File.AppendText(path))
        {
          writeStream.WriteLine($"[{DateTime.UtcNow}]: {text}");
        }

      }
      finally
      {
        semaphoreSlim.Release();
      }
    }

    private Object _creationLockObj = new Object();
    private Object _writeLockObj = new Object();
    static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

  }
}
