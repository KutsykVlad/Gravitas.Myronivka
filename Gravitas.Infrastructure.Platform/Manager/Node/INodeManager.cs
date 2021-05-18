using System;
using System.Collections.Generic;

namespace Gravitas.Infrastructure.Platform.Manager.Node
{
    public interface INodeManager
    {
        bool IsNodeExpired(int nodeId, TimeSpan timeout);
        void ChangeNodeState(int nodeId, bool state);
        string GetNodeName(int nodeId);
        List<int> GetEndPointNodes();
    }
}