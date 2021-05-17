using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gravitas.Infrastructure.Platform.Manager;

namespace QueueSimulator
{
    class MockConnectManager: IConnectManager
    {
        public bool SendSms(long templateId, long? ticketId)
        {
            Console.WriteLine("Message sent");
            return true;
        }



        public bool SendSms(long templateId, long? ticketId, string phoneNumber = null,
            Dictionary<string, object> parameters = null)
        {
            return true;
        }

        public bool SendEmail(long templateId, string emailAddress = null, object data = null, string attachmentPath = null)
        {
            return true;
        }

        public bool SendEmail(long templateId, long? ticketId, string emailAddress = null) => throw new NotImplementedException();

        public bool SendEmail(long templateId, long? ticketId, string emailAddress = null, object data = null)
        {
            return true;
        }
    }
}
