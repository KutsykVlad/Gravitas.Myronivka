using System;
using System.Collections.Generic;
using Gravitas.CollisionCoordination.Manager.CollisionManager;
using Gravitas.CollisionCoordination.Models;
using NLog;

namespace Gravitas.CollisionCoordination.Helpers
{
    public static class CollisionDataHelper
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public static bool SendNotifications(this CollisionData collisionData, ICollisionManager collisionManager)
        {
            try
            {
                var email = collisionManager.CreateEmail(collisionData.TicketId.Value, GetContactData(collisionData.EmailList, collisionData.PhoneList));
                _logger.Info($"Collision coordination: Email was created. TicketId={collisionData.TicketId}");
        
                var sms = collisionManager.CreateSms(collisionData.TicketId.Value);
                _logger.Info($"Collision coordination: Sms was created. TicketId={collisionData.TicketId}");
        
                email.Send(collisionData.EmailList, collisionData.TemplateId);
                sms.Send(collisionData.PhoneList, collisionData.TemplateId);

                _logger.Info($"Collision coordination: coordination data accepted. TicketId={collisionData.TicketId}");
            }
            catch (Exception e)
            {
                _logger.Error($"Collision coordination: Error while sending sms messages or emails. TicketId={collisionData.TicketId}, Error={e}");
                return false;
            }

            return true;
        }
        
        private static List<string> GetContactData(List<string> confirmationDataEmailList, List<string> confirmationDataPhoneList)
        {
            var result = new List<string>();
            for (var i = 0; i <= 2; i++)
            {
                result.Add(i < confirmationDataEmailList.Count ? confirmationDataEmailList[i] : string.Empty);
                result.Add(i < confirmationDataPhoneList.Count ? confirmationDataPhoneList[i] : string.Empty);
            }

            return result;
        }
    }
}