namespace Gravitas.Infrastructure.Platform.Manager.Connect
{
    public class TelegramMessage
    {
        public string BotToken { get; set; }
        public string ChatId { get; set; }
        public string Message { get; set; }
    }
}