namespace Gravitas.Infrastructure.Platform.ApiClient.SmsMobizon
{
    public interface ISmsMobizonApiClient
    {
        SmsMobizonApiClient.GetBalanceDto.Response GetBalance();
        SmsMobizonApiClient.GetMessageStatusDto.Response GetMessageStatus(string[] ids);
        SmsMobizonApiClient.SendMessageDto.Response SendSms(string recipient, string text);
    }
}