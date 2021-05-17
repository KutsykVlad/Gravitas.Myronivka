using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using NLog;

namespace Gravitas.Infrastructure.Platform.Manager.Display
{
    public class QueueDisplay : IQueueDisplay
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly bool _enabled;
        private readonly string _ipAddress;
        private readonly int _tcpPort;

        public QueueDisplay(string ipAddress, int tcpPort, bool enabled)
        {
            _ipAddress = ipAddress;
            _tcpPort = tcpPort;
            _enabled = enabled;
        }

        public bool WriteText(string text, int time)
        {
            if (!_enabled) return true;
            try
            {
                using (var tcpClient = new TcpClient())
                {
                    tcpClient.Connect(_ipAddress, _tcpPort);
                    var netStream = tcpClient.GetStream();

                    var encoding = Encoding.GetEncoding("windows-1251");
                    var cyrillicTextByte = encoding.GetBytes(text);
                    var msg = Encoding.ASCII.GetBytes(GenerateMessage(cyrillicTextByte, time));
                    netStream.Write(msg, 0, msg.Length);
                    msg = new byte[256];
                    var responseData = string.Empty;
                    if (netStream.CanRead)
                    {
                        netStream.ReadTimeout = 10000;
                        var bytes = netStream.Read(msg, 0, msg.Length);
                        responseData = Encoding.ASCII.GetString(msg, 0, bytes);
                    }
                    else
                    {
                        Logger.Error($"Display: {_ipAddress} Cant read data");
                    }

                    netStream.Close();
                    tcpClient.Close();
                    if (responseData.Contains("[INFO] OK"))
                    {
                        var splitted = text.Split(':');
                        if (splitted.Length > 1 && !string.IsNullOrWhiteSpace(splitted[1]))
                        {
                            Logger.Debug($"Display: {_ipAddress} wrote ok, text = {text}");
                        }

                        return true;
                    }

                    Logger.Error($"Display  {_ipAddress} unexpected answer: {responseData}");
                    return false;
                }
            }
            catch (Exception e)
            {
                Logger.Error($"Display Exception: {e.Message}");
                return false;
            }
        }

        private string GenerateMessage(byte[] text, int time)
        {
            return $"PLAY(TEXT_NOWAIT,{time.ToString()},1,0,{GetHex(text)},huge.font,,,)\n";
        }

        private string GetHex(byte[] text)
        {
            var t = string.Concat(text.Select(b => b.ToString("X2")).ToArray());
            return t;
        }
    }
}