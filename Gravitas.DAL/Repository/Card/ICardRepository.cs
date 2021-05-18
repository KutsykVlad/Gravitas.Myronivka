using Gravitas.DAL.Repository._Base;

namespace Gravitas.DAL.Repository.Card
{
    public interface ICardRepository : IBaseRepository
    {
        string GetContainerCardNo(long id);
    }
}