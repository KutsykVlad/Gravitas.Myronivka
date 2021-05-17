using System.Collections.Generic;

namespace Gravitas.Model.DomainModel.Node.TDO.Json
{
    public class NodeConfig
    {
        public NodeConfig()
        {
            DI = new Dictionary<string, DiConfig>();
            DO = new Dictionary<string, DoConfig>();
            Rfid = new Dictionary<string, RfidConfig>();
            Scale = new Dictionary<string, ScaleConfig>();
            LabAnalyser = new Dictionary<string, LabAnalyserConfig>();
            Camera = new Dictionary<string, CameraConfig>();
        }

        public Dictionary<string, DiConfig> DI { get; set; }
        public Dictionary<string, DoConfig> DO { get; set; }
        public Dictionary<string, RfidConfig> Rfid { get; set; }
        public Dictionary<string, ScaleConfig> Scale { get; set; }
        public Dictionary<string, LabAnalyserConfig> LabAnalyser { get; set; }
        public Dictionary<string, CameraConfig> Camera { get; set; }

        public class DiConfig
        {
            public long DeviceId { get; set; }
        }

        public class DoConfig
        {
            public long DeviceId { get; set; }
        }

        public class RfidConfig
        {
            public long DeviceId { get; set; }
            public int Timeout { get; set; }
        }

        public class ScaleConfig
        {
            public long DeviceId { get; set; }
            public int Timeout { get; set; }
        }

        public class LabAnalyserConfig
        {
            public long DeviceId { get; set; }
            public int Timeout { get; set; }
        }

        public class CameraConfig
        {
            public long DeviceId { get; set; }
        }
    }
}