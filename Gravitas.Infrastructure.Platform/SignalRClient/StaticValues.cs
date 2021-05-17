using System;
using Microsoft.AspNet.SignalR.Client;
using NLog;

namespace Gravitas.Infrastructure.Platform.SignalRClient
{
    public class SignalRClient
    {
        public class MainHub
        {
            private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
            private readonly string _hub = @"MainHub";
            private readonly string _url;

            public MainHub(string url)
            {
                _url = url;
            }

            public void Reload(string groupId)
            {
                InvokeHubMethod(_url, _hub, "Reload", groupId);
            }

            public void StopSpinner(string groupId)
            {
                InvokeHubMethod(_url, _hub, "StopSpinner", groupId);
            }

            public void UpdateProcessingMessage(string groupId)
            {
                InvokeHubMethod(_url, _hub, "UpdateProcessingMessage", groupId);
            }

            public void StartSpinner(string groupId)
            {
                InvokeHubMethod(_url, _hub, "StartSpinner", groupId);
            }

            private static void InvokeHubMethod(string url, string hub, string method, params object[] parameters)
            {
                try
                {
                    var connection = new HubConnection(url);
                    var myHub = connection.CreateHubProxy(hub);
                    connection.Start().Wait();
                    myHub.Invoke(method, parameters).Wait();
                    connection.Stop();
                }
                catch (Exception e)
                {
                    Logger.Error($"InvokeHubMethod: Error while invoke hub method, Error: {e}");
                }
            }
        }
    }
}