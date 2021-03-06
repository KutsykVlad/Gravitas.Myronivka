namespace Gravitas.Model
{
    public static class NodeData
    {
        public static class Config
        {
            public static class DI
            {
                public const string Barrier = "DI_Barrier";
                public const string LoopIncoming = "DI_LoopIncomming";
                public const string LoopOutgoing = "DI_LoopOutgoing";

                public const string PerimeterLeft = "DI_PerimeterLeft";
                public const string PerimeterRight = "DI_PerimeterRight";
                public const string PerimeterTop = "DI_PerimeterTop";
                public const string PerimeterBottom = "DI_PerimeterBottom";

                public const string SiloGate = "DI_SiloGate";
            }

            public static class DO
            {
                public const string Barrier = "DO_Barrier";
                public const string EmergencyOff = "DO_EmergencyOff";
                public const string TrafficLightIncoming = "DO_TrafficLightIncomming";
                public const string TrafficLightOutgoing = "DO_TrafficLightOutgoing";
                public const string RfidCheckFirst = "DO_RfidCheckFirst";
                public const string RfidCheckSecond = "DO_RfidCheckSecond";
            }

            public static class Rfid
            {
                public const string TableReader = "Rfid_TableReader";
                public const string OnGateReader = "Rfid_OnGateReader";
                public const string LongRangeReader = "Rfid_LongRangeReader";
                public const string LongRangeReader2 = "Rfid_LongRangeReader2";
            }

            public static class Scale
            {
                public const string Scale1 = "Scale_One";
            }

            public static class LabAnalyser
            {
                public const string Foss2 = "Lab_Foss2";
                public const string Foss = "Lab_Foss";
                public const string Bruker = "Lab_Bruker";
                public const string Infrascan = "Lab_Infrascan";
            }

            public static class Camera
            {
                public const string Camera1 = "Camera_One";
                public const string Camera2 = "Camera_Two";
                public const string Camera3 = "Camera_Three";
            }
        }

        public static class ActiveState
        {
            public const int Active = 1;
            public const int NotActive = 2;
            public const int InWork = 3;
        }
    }
    
    public enum ProcessingMsgType
    {
        None = 0,
        Success = 1,
        Info = 2,
        Warning = 3,
        Error = 4,
    }
}