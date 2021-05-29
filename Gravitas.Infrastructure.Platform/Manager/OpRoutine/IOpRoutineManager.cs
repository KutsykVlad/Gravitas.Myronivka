using System.Collections.Generic;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using CardType = Gravitas.Model.DomainValue.CardType;

namespace Gravitas.Infrastructure.Platform.Manager.OpRoutine
{
    public interface IOpRoutineManager
    {
        bool IsRfidCardValid(out NodeProcessingMsgItem errMsgItem, Card card, CardType cardTypeId);

        bool IsEmployeeBindedRfidCardValid(out NodeProcessingMsgItem errMsgItem, Card card, bool isEmployeeBinded = true);
        bool IsEmployeeSignValid(out NodeProcessingMsgItem errMsgItem, Card card, int nodeId);

        void LogNonStandardOp(Model.DomainModel.Node.TDO.Detail.NodeDetails nodeDetailsDto, NonStandartOpData opData);

        void UpdateProcessingMessage(int nodeId, NodeProcessingMsgItem msgItem);

        void UpdateProcessingMessage(IEnumerable<int> nodeIds, NodeProcessingMsgItem msgItem);
    }
}