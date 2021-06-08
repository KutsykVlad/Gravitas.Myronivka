namespace Gravitas.Core.DeviceManager.Card
{
    public static class CardHelper
    {
        public static bool IsFree(this Model.DomainModel.Card.DAO.Card card)
        {
            return card != null;
        }
    }
}