using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Gravitas.Infrastrructure.Common;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Newtonsoft.Json;

namespace Gravitas.Core.Manager.VkModuleSocket2
{
    public class VkModuleSocket2Manager : IVkModuleSocket2Manager
    {
        private static string GetWebResponse(HttpWebRequest request)
        {
            string responseStr = null;

            try
            {
                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    var objStream = response.GetResponseStream();

                    if (response.StatusCode == HttpStatusCode.OK)
                        using (var stream = new MemoryStream())
                        {
                            var buffer = new byte[2048]; // read in chunks of 2KB
                            int bytesRead;
                            while ((bytesRead = objStream.Read(buffer, 0, buffer.Length)) > 0)
                                stream.Write(buffer, 0, bytesRead);
                            responseStr = Encoding.ASCII.GetString(stream.ToArray());
                        }
                }
            }
            catch
            {
                /*ignore*/
            }

            return responseStr;
        }

        private static VkModule2In2OutStateXml GetVkModuleSocket2IoState(IPAddress host, string user, string pass)
        {
            VkModule2In2OutStateXml state;

            var request = (HttpWebRequest) WebRequest.Create($"http://{host}/protect/status.xml");
            request.Credentials = new NetworkCredential(user, pass);
            request.Timeout = 5000;

            try
            {
                var responseStr = GetWebResponse(request);
                state = SerializationHelper.DeserializeFromString<VkModule2In2OutStateXml>(responseStr);
                state.ErrCode = 0;
            }
            catch
            {
                state = new VkModule2In2OutStateXml();
            }

            return state;
        }

        private VkModuleI2O2State VkModuleI2O2XmlToJsonState(VkModule2In2OutStateXml xml)
        {
            VkModuleI2O2State json;

            if (xml != null)
                json = new VkModuleI2O2State
                {
                    LastUpdate = DateTime.Now,
                    ErrorCode = xml.ErrCode,
                    InData = new VkModuleI2O2InJsonState
                    {
                        DigitalIn = new Dictionary<int, DigitalInJsonState>
                        {
                            {1, new DigitalInJsonState {Value = xml.In0}},
                            {2, new DigitalInJsonState {Value = xml.In1}}
                        }
                    },
                    OutData = new VkModuleI2O2OutJsonState
                    {
                        DigitalOut = new Dictionary<int, DigitalOutJsonState>
                        {
                            {1, new DigitalOutJsonState {Value = xml.Out0}},
                            {2, new DigitalOutJsonState {Value = xml.Out1}}
                        }
                    }
                };
            else
                json = new VkModuleI2O2State
                {
                    LastUpdate = DateTime.Now,
                    ErrorCode = 255,
                    InData = new VkModuleI2O2InJsonState
                    {
                        DigitalIn = new Dictionary<int, DigitalInJsonState>
                        {
                            {1, new DigitalInJsonState {Value = false}},
                            {2, new DigitalInJsonState {Value = false}}
                        }
                    },
                    OutData = new VkModuleI2O2OutJsonState
                    {
                        DigitalOut = new Dictionary<int, DigitalOutJsonState>
                        {
                            {1, new DigitalOutJsonState {Value = false}},
                            {2, new DigitalOutJsonState {Value = false}}
                        }
                    }
                };

            return json;
        }

        public bool GetState(int deviceId)
        {
            if (!Program.Devices.ContainsKey(deviceId) || !Program.Devices[deviceId].ParentDeviceId.HasValue)
            {
                return false;
            }

            var parent = Program.Devices[Program.Devices[deviceId].ParentDeviceId.Value];
            var deviceParam = Program.DeviceParams[parent.Id];
            if (deviceParam == null) return false;

            var param = JsonConvert.DeserializeObject<VkModuleSocketXParam>(deviceParam.ParamJson);
            if (param == null) return false;

            if (!IPAddress.TryParse(param.IpAddress, out var ipAddress)) return false;

            var xmlState = GetVkModuleSocket2IoState(ipAddress, param.Login, param.Password);

            var tmpDbState = VkModuleI2O2XmlToJsonState(xmlState);
            return tmpDbState.InData.DigitalIn[deviceId].Value;
        }

        public void SetState(int deviceId, bool value)
        {
            if (!Program.Devices.ContainsKey(deviceId) || !Program.Devices[deviceId].ParentDeviceId.HasValue)
            {
                return;
            }

            var parent = Program.Devices[Program.Devices[deviceId].ParentDeviceId.Value];
            var deviceParam = Program.DeviceParams[parent.Id];
            if (deviceParam == null) return;

            var parentParam = JsonConvert.DeserializeObject<VkModuleSocketXParam>(deviceParam.ParamJson);
            if (parentParam == null) return;

            if (!IPAddress.TryParse(parentParam.IpAddress, out var ipAddress)) return;

            var xmlState = GetVkModuleSocket2IoState(ipAddress, parentParam.Login, parentParam.Password);
            
            var param = JsonConvert.DeserializeObject<DigitalInOutParam>(Program.DeviceParams[deviceId].ParamJson);
            if (param.No == 0)
            {
                
                xmlState.Out0 = value;
            }
            else
            {
                xmlState.Out1 = value;
            }
            var request =
                (HttpWebRequest) WebRequest.Create($"http://{ipAddress}/protect/leds.cgi?led={param.No}&timeout={5000}");
            request.Credentials = new NetworkCredential(parentParam.Login, parentParam.Password);
            request.Timeout = 5000;

            GetWebResponse(request);
        }
    }
}