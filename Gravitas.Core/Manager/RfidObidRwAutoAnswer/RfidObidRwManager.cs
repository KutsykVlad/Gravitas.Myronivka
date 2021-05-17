using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Gravitas.Model;
using Gravitas.Model.Dto;

namespace Gravitas.Core.Manager.RfidObidRwAutoAnswer
{
    public class RfidObidRwManager : IRfidObidRwManager
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly long _deviceId;

        public RfidObidRwManager(long deviceId)
        {
            _deviceId = deviceId;
        }

        private void GetInformationAsync(IPAddress ipAddress, int port, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    using (var tcpClient = new TcpClient())
                    {
                        tcpClient.Connect(ipAddress, port);

                        var stream = tcpClient.GetStream();
                        stream.ReadTimeout = 5000;

                        while (true)
                        {
                            byte[] bytes = new byte[1024];
                            int bytesCount = stream.Read(bytes, 0, 1024);

                            string rfid = Parse(bytes, bytesCount);
                            if (rfid != null)
                            {
                                UpdateDeviceState(0, new RfidObidRwInJsonState()
                                {
                                    Rifd = rfid
                                });
                            }
                            else
                            {
                                UpdateDeviceState(255, new RfidObidRwInJsonState()
                                {
                                    Rifd = string.Empty
                                });
                            }

                            Thread.Sleep(1000);
                        }
                    }
                }
                catch (System.IO.IOException)
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

                Thread.Sleep(1000);
            }
        }

        public void SyncData(CancellationToken token)
        {
            if (!Program.Devices.ContainsKey(_deviceId))
            {
                return;
            }

            var deviceParams = Program.DeviceParams[_deviceId];

            RfidObidRwParam param = RfidObidRwParam.FromJson(deviceParams.ParamJson);
            if (param == null)
            {
                return;
            }

            if (!IPAddress.TryParse(param.IpAddress, out var ipAddress))
            {
                return;
            }

            GetInformationAsync(ipAddress, param.Port, token);

            while (!token.IsCancellationRequested)
            {
                Thread.Sleep(500);
            }
        }

        private string Parse(byte[] dataBytes, int bytesCount)
        {
            const int dataLen = 11;

            if (bytesCount < dataLen)
            {
                Logger.Debug($@"Device: {_deviceId}. Not enought data was received.");
                return null;
            }

            byte chSum = CalcObidRwCheckSum(dataBytes, dataLen - 1);
            if (dataBytes[10] != chSum)
            {
                return null;
            }

            string cardRfid = string.Concat(dataBytes.Skip(5).Take(dataLen - 5 - 1).Select(b => b.ToString("X2")));
            Logger.Debug($@"Device: {_deviceId}. Card = {cardRfid}, ChSum = {chSum:X2}.");

            return cardRfid;
        }

        private byte CalcObidRwCheckSum(byte[] dataBytes, int bytesCount)
        {
            byte chSum = 0;
            foreach (byte cardByte in dataBytes.Take(bytesCount))
            {
                chSum = (byte) (chSum ^ cardByte);
            }

            return chSum;
        }

        private void UpdateDeviceState(int errorCode, RfidObidRwInJsonState state)
        {
            if (!Program.Devices.ContainsKey(_deviceId))
            {
                return;
            }

            var device = Program.Devices[_deviceId];


            if (device.DeviceState == null)
            {
                if (!Program.DeviceStates.ContainsKey(_deviceId))
                {
                    Program.DeviceStates.TryAdd(_deviceId, new DeviceState());
                }

                device.StateId = _deviceId;
                Program.Devices[_deviceId] = device;
            }

            if (device.DeviceState == null)
            {
                return;
            }

            Program.DeviceStates[_deviceId].ErrorCode = errorCode;
            Program.DeviceStates[_deviceId].LastUpdate = DateTime.Now;
            Program.DeviceStates[_deviceId].InData = state.ToJson();
        }
    }
}