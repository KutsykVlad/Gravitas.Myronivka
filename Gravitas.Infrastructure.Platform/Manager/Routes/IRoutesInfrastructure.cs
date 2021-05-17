using System.Collections.Generic;
using Gravitas.Model;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.Infrastructure.Platform.Manager.Routes
{
    public interface IRoutesInfrastructure
    {
        string MarkPassedNodes(long ticketId, long routeId, bool disableAppend = false);
        bool IsNodeAvailable(long nodeId, long routeTemplateId);
        IEnumerable<RouteTemplate> GetRouteTemplates(int state, long? nodeId = null);
        string NormalizeRoute(string routeJson);
        void MoveForward(long ticketId, long nodeId);
        void MoveBack(long ticketId);
        bool SetSecondaryRoute(long ticketId, long nodeId, int type);
        List<long> GetNodesInGroup(long? routeId, NodeGroup groupId);
        List<RouteNodes> GetRouteForPrintout(long ticketId);
        int GetNodeProcess(long ticketId, long nodeId);
        bool IsLastScaleProcess(long ticketId);
        bool IsRouteWithoutGuide(long ticketId);
        void AddDestinationOpData(long ticketId, long nodeId);
        List<long> GetNextNodes(long ticketId);
        bool IsTicketRejected(long ticketId);
        void AssignSingleUnloadPoint(long ticketId, long nodeId);
    }
}