using System.Collections.Generic;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.Infrastructure.Platform.Manager.Routes
{
    public interface IRoutesInfrastructure
    {
        string MarkPassedNodes(int ticketId, int routeId, bool disableAppend = false);
        bool IsNodeAvailable(int nodeId, int routeTemplateId);
        IEnumerable<RouteTemplate> GetRouteTemplates(RouteType state, int? nodeId = null);
        string NormalizeRoute(string routeJson);
        void MoveForward(int ticketId, int nodeId);
        void MoveBack(int ticketId);
        bool SetSecondaryRoute(int ticketId, int nodeId, RouteType type);
        List<int> GetNodesInGroup(int? routeId, NodeGroup groupId);
        List<RouteNodes> GetRouteForPrintout(int ticketId);
        int GetNodeProcess(int ticketId, int nodeId);
        bool IsLastScaleProcess(int ticketId);
        bool IsRouteWithoutGuide(int ticketId);
        void AddDestinationOpData(int ticketId, int nodeId);
        List<int> GetNextNodes(int ticketId);
        bool IsTicketRejected(int ticketId);
        void AssignSingleUnloadPoint(int ticketId, int nodeId);
    }
}