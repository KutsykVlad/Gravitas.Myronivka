using System.Collections.Generic;

namespace Gravitas.CollisionCoordination.Messages
{
    public interface IMessage
    {
        bool Send(List<string> contacts, int? templateId = null);
    }
}