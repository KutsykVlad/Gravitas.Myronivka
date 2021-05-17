namespace Gravitas.Core.DeviceManager.Card
{
    public static class CardHelper
    {
        public static bool IsFree(this Model.Card card)
        {
            return card != null && !card.IsOwn;
        }
    }
}