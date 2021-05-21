using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Newtonsoft.Json;

namespace Gravitas.Core.Manager.RfidObidRwAutoAnswer
{
    public class RfidObidRwManager : IRfidObidRwManager
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private string GetInformationAsync(IPAddress ipAddress, int port, int deviceId)
        {
            try
            {
                using (var tcpClient = new TcpClient())
                {
                    tcpClient.Connect(ipAddress, port);

                    var stream = tcpClient.GetStream();
                    stream.ReadTimeout = 5000;

                    var bytes = new byte[1024];
                    var bytesCount = stream.Read(bytes, 0, 1024);

                    var rfid = Parse(bytes, bytesCount, deviceId);
                    return rfid;
                }
            }
            catch (System.IO.IOException)
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

        public string GetCard(int deviceId)
        {
            if (!Program.Devices.ContainsKey(deviceId))
            {
                return null;
            }

            var deviceParams = Program.DeviceParams[deviceId];

            var param = JsonConvert.DeserializeObject<RfidObidRwParam>(deviceParams.ParamJson);
            if (param == null)
            {
                return null;
            }

            if (!IPAddress.TryParse(param.IpAddress, out var ipAddress))
            {
                return null;
            }

            return GetInformationAsync(ipAddress, param.Port, deviceId);
        }

        private string Parse(byte[] dataBytes, int bytesCount, int deviceId)
        {
            const int dataLen = 11;

            if (bytesCount < dataLen)
            {
                Logger.Debug($@"Device: {deviceId}. Not enought data was received.");
                return null;
            }

            var chSum = CalcObidRwCheckSum(dataBytes, dataLen - 1);
            if (dataBytes[10] != chSum)
            {
                return null;
            }

            string cardRfid = string.Concat(dataBytes.Skip(5).Take(dataLen - 5 - 1).Select(b => b.ToString("X2")));
            Logger.Debug($@"Device: {deviceId}. Card = {cardRfid}, ChSum = {chSum:X2}.");

            return cardRfid;
        }

        private byte CalcObidRwCheckSum(byte[] dataBytes, int bytesCount)
        {
            byte chSum = 0;
            foreach (var cardByte in dataBytes.Take(bytesCount))
            {
                chSum = (byte) (chSum ^ cardByte);
            }

            return chSum;
        }
    }
}