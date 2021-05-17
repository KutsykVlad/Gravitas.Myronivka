using System.Collections.Generic;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.Dto;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

namespace Gravitas.Infrastructure.Platform.Manager
{
    public interface IOpRoutineManager
    {
        bool IsRfidCardValid(out NodeProcessingMsgItem errMsgItem, Card card, int cardTypeId);

        bool IsEmployeeBindedRfidCardValid(out NodeProcessingMsgItem errMsgItem, Card card, bool isEmployeeBinded = true);
        bool IsEmployeeSignValid(out NodeProcessingMsgItem errMsgItem, Card card, long nodeId);

        void LogNonStandardOp(Node nodeDto, NonStandartOpData opData);

        void UpdateProcessingMessage(long nodeId, NodeProcessingMsgItem msgItem);

        void UpdateProcessingMessage(IEnumerable<long> nodeIds, NodeProcessingMsgItem msgItem);
    }
}