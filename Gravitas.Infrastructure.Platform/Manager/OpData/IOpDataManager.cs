using System;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpData.TDO.Detail;
using Gravitas.Model.DomainModel.OpDataEvent.DAO;
using Gravitas.Model.DomainModel.OpDataEvent.DTO;
using LabFacelessOpData = Gravitas.Model.DomainModel.OpData.TDO.Detail.LabFacelessOpData;

namespace Gravitas.Infrastructure.Platform.Manager.OpData
{
    public interface IOpDataManager
    {
        SingleWindowOpData FetchRouteResults(int ticketId);
        SingleWindowOpDataDetail GetSingleWindowOpDataDetail(Guid id);
        LabFacelessOpData GetLabFacelessOpDataDto(Guid id);
        OpDataEventDto[] GetEvents(int ticketId, int? eventType = null);
        void AddEvent(OpDataEvent opDataEvent);
        BasicTicketContainerData GetBasicTicketData(int ticketId);
    }
}