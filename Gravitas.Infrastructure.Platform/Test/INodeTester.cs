using System.Collections.Generic;

namespace Gravitas.Infrastructure.Platform.Test
{
    public interface INodeTester
    {
        void PutRfidCard(long deviceId, string cardId);
        void PutZebraCard(long deviceId, string cardId);
        void SendGetRequest(string url);
        void SendPostRequest(string url, Dictionary<string, string> obj);
    }
}