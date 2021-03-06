namespace Gravitas.Model.DomainValue
{
    public static class OrganizationUnitType
    {
        public static class Organization
        {
            public const int Id = 1;
            public const int MHP = 2;
        }

        public static class Plant
        {
            public const int Id = 2;
            public const int KKZ = 1;
        }

        public static class Department
        {
            public const int Id = 3;
        }

        public static class Area
        {
            public const int Id = 4;
            public const int MainArea = 1;
            public const int UpperArea = 2;
            public const int CarFleet = 3;
        }

        public static class Sector
        {
            public const int Id = 5;
        }

        public static class Workstantion
        {
            public const int Id = 6;
            public const int Elevator1 = 1;
            public const int Elevator2 = 2;
            public const int Elevator3 = 3;
            public const int Elevator4_5 = 4;
            public const int ShrotLoad = 5;
            public const int TareWarehouse = 6;
            public const int OilLoad = 7;
            public const int CustomWarehouse = 8;
            public const int MixedFeedLoad = 9;
            public const int Husk = 10;
            public const int Stores = 11;
            public const int MPZ = 12;
            public const int TMC = 13;
        }
    }
}