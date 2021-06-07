using Gravitas.Infrastructure.Common.Configuration;

namespace Gravitas.Infrastructure.Platform.SignalRClient
{
    public static class SignalRInvoke
    {
        public static void ReloadHubGroup(int groupId)
        {
            new SignalRClient.MainHub(GlobalConfigurationManager.SignalRHost).Reload(groupId.ToString("D"));
        }

        public static void UpdateProcessingMessage(int groupId)
        {
            new SignalRClient.MainHub(GlobalConfigurationManager.SignalRHost).UpdateProcessingMessage(groupId.ToString("D"));
        }

        public static void StopSpinner(int groupId)
        {
            new SignalRClient.MainHub(GlobalConfigurationManager.SignalRHost).StopSpinner(groupId.ToString("D"));
        }

        public static void StartSpinner(int groupId)
        {
            new SignalRClient.MainHub(GlobalConfigurationManager.SignalRHost).StartSpinner(groupId.ToString("D"));
        }
        
        public static void UpdateDriverCheckIn(int value)
        {
            new SignalRClient.MainHub(GlobalConfigurationManager.SignalRHost).UpdateDriverCheckIn(value.ToString("D"));
        }
    }
}