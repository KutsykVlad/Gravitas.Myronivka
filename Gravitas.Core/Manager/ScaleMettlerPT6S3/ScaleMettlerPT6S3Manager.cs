using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Gravitas.Core.DeviceManager;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Newtonsoft.Json;
using NLog;

namespace Gravitas.Core.Manager.ScaleMettlerPT6S3
{
    public class ScaleMettlerPT6S3Manager : IScaleMettlerPT6S3Manager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly int _deviceId;

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

        private double _oldVal;

        public ScaleMettlerPT6S3Manager(int deviceId)
        {
            _deviceId = deviceId;
        }

        public void SyncData(CancellationToken token)
        {
            var deviceParam = Program.DeviceParams[_deviceId];

            var param = JsonConvert.DeserializeObject<ScaleMettlerPT6S3Param>(deviceParam.ParamJson);
            if (param == null) return;

            if (!IPAddress.TryParse(param.IpAddress, out var ipAddress)) return;

            var remoteEp = new IPEndPoint(ipAddress, param.Port);

            while (!token.IsCancellationRequested)
            {
                try
                {
                    using (var tcpClient = new TcpClient())
                    {
                        tcpClient.Connect(remoteEp.Address, remoteEp.Port);

                        SocketHelper.Send(tcpClient.Client, "P");
                        var ans = SocketHelper.Receive(tcpClient.Client);
                        Parse(ans);

                        var deviceState = Program.DeviceStates[_deviceId];
                        var scaleOutJsonState = JsonConvert.DeserializeObject<ScaleOutJsonState>(deviceState.OutData);
                        var scaleInJsonState = JsonConvert.DeserializeObject<ScaleInJsonState>(deviceState.InData);

                        if (scaleOutJsonState != null
                            && scaleOutJsonState.ZeroScaleCmd
                            && scaleInJsonState != null
                            && scaleInJsonState.Value != 0.0)
                        {
                            SocketHelper.Send(tcpClient.Client, "M");
                            SocketHelper.Receive(tcpClient.Client);
                        }
                    }
                }
                catch (IOException)
                {
//					Logger.Debug($@"Device: {_deviceId}. IOException.", e.ToString());
                }
                catch (SocketException e)
                {
                    Logger.Debug($@"Device: {_deviceId}. SocketException.", e.SocketErrorCode.GetTypeCode().ToString());
                }
                catch (Exception e)
                {
                    Logger.Debug(e, $@"Device: {_deviceId}. Exception.");
                }

                Thread.Sleep(1 * 1000);
            }
        }

        private void Parse(string ans)
        {
            if (ans.Length != 8
                || ans[0] != '\r'
                || !_prefixCharset.Contains(ans[1]))
                return;

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

            if (Math.Abs(_oldVal - state.Value) > 1)
            {
                Logger.Debug($@"Device: {_deviceId}. Scale: {JsonConvert.SerializeObject(state)}");
                _oldVal = state.Value;
            }

            UpdateDeviceState(0, state);
        }

        private void UpdateDeviceState(int errorCode, ScaleInJsonState inState)
        {
            var device = Program.Devices[_deviceId];
            var deviceState = Program.DeviceStates[_deviceId];
            if (device == null) return;

            if (deviceState == null)
            {
                var devState = new DeviceState();
                Program.DeviceStates[_deviceId] = devState;

                device.StateId = _deviceId;
                Program.Devices[_deviceId] = device;
            }

            if (deviceState == null) return;

            var outState = JsonConvert.DeserializeObject<ScaleOutJsonState>(deviceState.OutData);
            if (outState != null
                && outState.ZeroScaleCmd
                && inState.Value == 0)
                outState.ZeroScaleCmd = false;

            deviceState.ErrorCode = errorCode;
            deviceState.LastUpdate = DateTime.Now;
            deviceState.InData = JsonConvert.SerializeObject(inState);
            deviceState.OutData = JsonConvert.SerializeObject(outState);

            Program.DeviceStates[_deviceId] = deviceState;
        }
    }
}