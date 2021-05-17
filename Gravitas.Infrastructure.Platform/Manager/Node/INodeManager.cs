using System;
using System.Collections.Generic;

namespace Gravitas.Infrastructure.Platform.Manager
{
    public interface INodeManager
    {
        bool IsNodeExpired(long nodeId, TimeSpan timeout);
        void ChangeNodeState(long nodeId, bool state);
        string GetNodeName(long nodeId);
        List<long> GetEndPointNodes();
    }
}