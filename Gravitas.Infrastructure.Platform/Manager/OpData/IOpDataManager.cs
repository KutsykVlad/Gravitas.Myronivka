using System;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpDataEvent.DAO;
using Gravitas.Model.DomainModel.OpDataEvent.DTO;
using Gravitas.Model.Dto;
using LabFacelessOpData = Gravitas.Model.Dto.LabFacelessOpData;

namespace Gravitas.Infrastructure.Platform.Manager
{
    public interface IOpDataManager
    {
        SingleWindowOpData FetchRouteResults(long ticketId);
        SingleWindowOpDataDetail GetSingleWindowOpDataDetail(Guid id);
        LabFacelessOpData GetLabFacelessOpDataDto(Guid id);
        OpDataEventDto[] GetEvents(long ticketId, int? eventType = null);
        void AddEvent(OpDataEvent opDataEvent);
        BasicTicketContainerData GetBasicTicketData(long ticketId);
    }
}