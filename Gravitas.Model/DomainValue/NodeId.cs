namespace Gravitas.Model.DomainValue
{
    public enum NodeIdValue
    {
        // Driver checkin
        DriverCheckIn = 10,
        
        // Single window
        SingleWindowFirstType1 = 91,
        SingleWindowFirstType2 = 92,

        // Security
        SecurityIn1 = 310,
        SecurityOut1 = 320,
        SecurityIn2 = 311,
        SecurityOut2 = 321,
        SecurityReview1 = 331,

        // Unload laboratory
        Laboratory0 = 599,
        Laboratory3 = 503,
        Laboratory4 = 504,
        Laboratory5 = 505,
        
        // Weighbridges
        Weighbridge1 = 701,
        Weighbridge2 = 702,
        Weighbridge3 = 703,
        Weighbridge4 = 704,
        Weighbridge5 = 705,
        
        // Central laboratory
        CentralLaboratoryGetShrot = 201,
        CentralLaboratoryGetOil1 = 202,
        CentralLaboratoryGetOil2 = 203,
        CentralLaboratoryGetCustomStore = 204,
        CentralLaboratoryGetTruckRamp = 205,
        CentralLaboratoryProcess1 = 210,
        CentralLaboratoryProcess2 = 211,
        CentralLaboratoryProcess3 = 212,
        CentralLaboratoryProcess4 = 213,
        
        // Mixed feed
        MixedFeedManager = 100,
        MixedFeedLoad1 = 111,
        MixedFeedLoad2 = 112,
        MixedFeedLoad3 = 113,
        MixedFeedLoad4 = 114,
        MixedFeedGuide = 110,
        
        // Unload points
        // елеватор 4, 5
        UnloadPointGuideEl45 = 800,
        UnloadPoint10 = 810,
        UnloadPoint11 = 811,
        UnloadPoint12 = 812,
        UnloadPoint13 = 813,
        UnloadPoint14 = 814,
        // елеватор 2, 3
        UnloadPointGuideEl23 = 801,
        UnloadPoint20 = 820,
        UnloadPoint21 = 821,
        UnloadPoint22 = 822,
        // шрот
        UnloadPoint30 = 830,
        // склади
        UnloadPoint31 = 831,
        UnloadPoint32 = 832,
        // склад тарировки
        UnloadPoint40 = 840,
        // нижня територія
        UnloadPointGuideLowerArea = 802,
        UnloadPoint50 = 850,
        // чекпоінт
        UnloadPoint60 = 860,
        UnloadPoint61 = 861,
        
        // Load points
        // елеватор 2, 3
        LoadPointGuideEl23 = 900,
        LoadPoint10 = 910, 
        LoadPoint11 = 911,
        LoadPoint12 = 912,
        // елеватор 4, 5
        LoadPointGuideEl45 = 901,
        LoadPoint20 = 920,
        LoadPoint21 = 921,
        LoadPoint22 = 922,
        // шрот
        LoadPointGuideShrotHuskOil = 902,
        LoadPoint30 = 930,
        LoadPoint31 = 931,
        LoadPoint32 = 932,
        LoadPoint33 = 933,
        // олія
        LoadPoint40 = 940,
        LoadPoint41 = 941,
        // склад тарировки
        LoadPointGuideTareWarehouse = 903,
        LoadPoint50 = 950,
        // МПЗ
        LoadPointGuideMPZ = 904,
        LoadPoint60 = 960,
        LoadPoint61 = 961,
        LoadPoint62 = 962,
        // склади
        LoadPoint70 = 970,
        LoadPoint71 = 971,
        LoadPoint73 = 972,
        // нижня територія
        LoadPointGuideLowerArea = 905,
        LoadPoint72 = 980,
    }
}