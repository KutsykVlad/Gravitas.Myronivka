namespace Gravitas.Model
{
    public static partial class Dom
    {
        public static class Queue
        {
            public static class Priority
            {
                public const int High = 1;
                public const int Medium = 2;
                public const int Low = 3;
            }

            public static class Category
            {
                public const int Company = 0;
                public const int Partners = 1;
                public const int Others = 2;
                public const int MixedFeedLoad = 3;
                public const int PreRegisterCategory = 4;
            }
        }
    }
}
