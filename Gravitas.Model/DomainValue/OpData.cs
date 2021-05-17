namespace Gravitas.Model
{
    public static partial class Dom
    {
        public static class OpDataState
        {
            // Processing states
            public const int Init = 1;
            public const int Processing = 2;
            public const int Collision = 3;
            public const int CollisionApproved = 4;
            public const int CollisionDisapproved = 5;

            // Finished states
            public const int Processed = 10;
            public const int Rejected = 11;
            public const int Canceled = 12;
            public const int PartLoad = 13;
            public const int PartUnload = 14;
            public const int Reload = 15;
        }

        public static class ScaleOpData
        {
            public static class Type
            {
                public const int Tare = 1;
                public const int Gross = 2;
            }
        }
    }
}