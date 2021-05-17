using Gravitas.Infrastructure.Common.Configuration;

namespace Gravitas.Infrastructure.Platform.SignalRClient
{
    public static class SignalRInvoke
    {
        public static void ReloadHubGroup(long groupId)
        {
            new SignalRClient.MainHub(GlobalConfigurationManager.SignalRHost).Reload(groupId.ToString("D"));
        }

        public static void UpdateProcessingMessage(long groupId)
        {
            new SignalRClient.MainHub(GlobalConfigurationManager.SignalRHost).UpdateProcessingMessage(groupId.ToString("D"));
        }

        public static void StopSpinner(long groupId)
        {
            new SignalRClient.MainHub(GlobalConfigurationManager.SignalRHost).StopSpinner(groupId.ToString("D"));
        }

        public static void StartSpinner(long groupId)
        {
            new SignalRClient.MainHub(GlobalConfigurationManager.SignalRHost).StartSpinner(groupId.ToString("D"));
        }
    }
}