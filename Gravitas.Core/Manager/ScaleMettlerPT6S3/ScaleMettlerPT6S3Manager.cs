using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Gravitas.Core.DeviceManager;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Newtonsoft.Json;
using NLog;

namespace Gravitas.Core.Manager.ScaleMettlerPT6S3
{
    public class ScaleMettlerPT6S3Manager : IScaleMettlerPT6S3Manager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly char[] _prefixCharset =
        {
            'I' //if gross positive immobile
            ,
            'i' //if gross negative immobile
            ,
            ' ' //if gross positive not immobile
            ,
            '-' //if gross negative not immobile
            ,
            'N' //if net positive immobile
            ,
            'n' //if net negative immobile
            ,
            'B' //if net positive not immobile
            ,
            'b' //if net negative not immobile
            ,
            'D' //if indicator in under tare
            ,
            'S' //if indicator in overload
            ,
            'Z' //if indicator on metrological zero, unstable
            ,
            'z' //if indicator on metrological zero, stable
        };

        private readonly char[] _prefixIsErrorCharset =
        {
            'D' //if indicator in under tare
            ,
            'S' //if indicator in overload
        };

        private readonly char[] _prefixIsGrossCharset =
        {
            'I' //if gross positive immobile
            ,
            'i' //if gross negative immobile
            ,
            ' ' //if gross positive not immobile
            ,
            '-' //if gross negative not immobile
        };

        private readonly char[] _prefixIsImmobileCharset =
        {
            'I' //if gross positive immobile
            ,
            'i' //if gross negative immobile
            ,
            'N' //if net positive immobile
            ,
            'n' //if net negative immobile
            ,
            'z' //if indicator on metrological zero, stable
        };

        private readonly char[] _prefixIsNegativeCharset =
        {
            'i' //if gross negative immobile
            ,
            '-' //if gross negative not immobile
            ,
            'n' //if net negative immobile
            ,
            'b' //if net negative not immobile
        };

        private readonly char[] _prefixIsZeroCharset =
        {
            'Z' //if indicator on metrological zero, unstable
            ,
            'z' //if indicator on metrological zero, stable
        };

        private ScaleInJsonState Parse(string ans)
        {
            if (ans.Length != 8
                || ans[0] != '\r'
                || !_prefixCharset.Contains(ans[1]))
                return null;

            double.TryParse(ans.Substring(2, ans.Length - 3), out var value);

            var paramChar = ans[1];
            var state = new ScaleInJsonState
            {
                Value = _prefixIsNegativeCharset.Contains(paramChar)
                    ? -value
                    : value,
                IsGross = _prefixIsGrossCharset.Contains(paramChar),
                IsImmobile = _prefixIsImmobileCharset.Contains(paramChar),
                IsZero = _prefixIsZeroCharset.Contains(paramChar),
                IsScaleError = _prefixIsErrorCharset.Contains(paramChar)
            };

            return state;
        }

        public ScaleInJsonState GetState(int deviceId)
        {
            var deviceParam = Program.DeviceParams[deviceId];

            var param = JsonConvert.DeserializeObject<ScaleMettlerPT6S3Param>(deviceParam.ParamJson);
            if (param == null) return null;

            if (!IPAddress.TryParse(param.IpAddress, out var ipAddress)) return null;

            var remoteEp = new IPEndPoint(ipAddress, param.Port);

            try
            {
                using (var tcpClient = new TcpClient())
                {
                    tcpClient.Connect(remoteEp.Address, remoteEp.Port);

                    SocketHelper.Send(tcpClient.Client, "P");
                    var ans = SocketHelper.Receive(tcpClient.Client);
                    return Parse(ans);
                }
            }
            catch (IOException)
            {
//					Logger.Debug($@"Device: {_deviceId}. IOException.", e.ToString());
            }
            catch (SocketException e)
            {
                Logger.Debug($@"Device: {deviceId}. SocketException.", e.SocketErrorCode.GetTypeCode().ToString());
            }
            catch (Exception e)
            {
                Logger.Debug(e, $@"Device: {deviceId}. Exception.");
            }

            return null;
        }
    }
}