using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using NLog;

namespace Gravitas.Infrastructure.Common.Helper
{
    public class FileHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static IEnumerable<string> ReadAllLines(string fullPath, int readTryCount, int readTryTimeout = 1000)
        {
            if (!File.Exists(fullPath)) return null;

            var data = new List<string>();
            while (true)
            {
                readTryCount--;
                try
                {
                    using (var reader = new StreamReader(fullPath, Encoding.UTF8))
                    {
                        while (!reader.EndOfStream)
                        {
                            data.Add(reader.ReadLine());
                        }
                    }
                    return data;
                }
                catch (IOException ioException)
                {
                    if (readTryCount < 0)
                        throw;

                    Logger.Log(LogLevel.Warn, ioException);
                    Thread.Sleep(readTryTimeout);
                }
            }
        }
        
        public static string ReadAllText(string fullPath, int readTryCount, int readTryTimeout = 1000)
        {
            if (!File.Exists(fullPath)) return null;

            while (true)
            {
                readTryCount--;
                try
                {
                    using (var reader = new StreamReader(fullPath, Encoding.GetEncoding(1251)))
                    {
                        var data = reader.ReadToEnd();
                        return data;
                    }
                }
                catch (IOException ioException)
                {
                    if (readTryCount < 0)
                        throw;

                    Logger.Log(LogLevel.Warn, ioException);
                    Thread.Sleep(readTryTimeout);
                }
            }
        }
    }
}