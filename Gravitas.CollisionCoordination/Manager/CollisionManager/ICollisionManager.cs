using System;
using System.Collections.Generic;
using Gravitas.CollisionCoordination.Messages;

namespace Gravitas.CollisionCoordination.Manager.CollisionManager
{
    public interface ICollisionManager
    {
        IMessage CreateEmail(int ticketId, List<string> contactData);
        IMessage CreateSms(int ticketId);
        void Approve(Guid opDataId, string approvedBy);
        void Disapprove(Guid opDataId, string approvedBy);
        bool IsOpDataValid(Guid opDataId);
        void SendCentralLabNotification(Guid opDataId, bool approved);
    }
}