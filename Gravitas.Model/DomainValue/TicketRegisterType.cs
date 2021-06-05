namespace Gravitas.Model.DomainValue
{
    public enum TicketRegisterType
    { 
        None = 0,
        CentralLaboratory,
        SingleWindowQueue,
        SingleWindowInProgress,
        SingleWindowProcessed,
        LoadGuide,
        RejectedLoadGuide,
        RejectedUnloadGuide,
        LoadPointsType1,
        UnloadPoints,
        MixedFeedGuide,
        RejectedMixedFeedLoad,
        RejectedMixedFeedUnLoad,
        MixedFeedLoad,
        LoadPointsType2,
        UnloadGuide,
        SelfServiceLaboratory,
        UnloadQueue,
        DriverCheckIn
    }
}