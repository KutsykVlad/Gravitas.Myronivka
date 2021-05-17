namespace Gravitas.Model
{
    public static partial class Dom
    {
        public static class Route
        {
            public static class Type
            {
                public const int SingleWindow = 0;
                public const int Temporary = 2;
                public const int Reload = 3;
                public const int PartLoad = 4;
                public const int PartUnload = 5;
                public const int Move = 6;
                public const int MixedFeedLoad = 7;
                public const int Reject = 8;
            }
        }
    }
}