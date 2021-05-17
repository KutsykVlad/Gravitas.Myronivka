namespace Gravitas.Model
{
    public static partial class Dom
    {
        public static class Device
        {
            public static class Type
            {
                public const int RfidObidRw = 1;
                public const int RfidZebraFx9500Head = 2;
                public const int RfidZebraFx9500Antenna = 3;
                public const int RelayVkmodule2In2Out = 4;
                public const int RelayVkmodule4In0Out = 5;
                public const int DigitalIn = 6;
                public const int DigitalOut = 7;
                public const int ScaleMettlerPT6S3 = 8;
                public const int Led = 9;
                public const int Camera = 10;
                public const int LabFoss = 11;
                public const int LabFoss2 = 15;
                public const int LabBruker = 12;
                public const int Display = 13;
                public const int LabInfrascan = 14;
            }

            public static class Status
            {
                public static class ErrorCode
                {
                    public const int Ok = 0;
                    public const int Timeout = 1;
                    public const int NA = 255;
                }
            }
        }
    }
}