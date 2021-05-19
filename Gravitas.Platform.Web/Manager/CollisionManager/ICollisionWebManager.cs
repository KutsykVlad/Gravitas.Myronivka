using System.Collections.Generic;
using Gravitas.Model.DomainValue;

namespace Gravitas.Platform.Web.Manager.CollisionManager
{
    public interface ICollisionWebManager
    {
        bool SendToConfirmation(long ticketId, IEnumerable<string> phoneList, IEnumerable<string> emailList, EmailTemplate? templateId = null);
    }
}