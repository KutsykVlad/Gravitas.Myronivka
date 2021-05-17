using System.Collections.Generic;

namespace Gravitas.Platform.Web.Manager.CollisionManager
{
    public interface ICollisionWebManager
    {
        bool SendToConfirmation(long ticketId, IEnumerable<string> phoneList, IEnumerable<string> emailList, int? templateId = null);
    }
}