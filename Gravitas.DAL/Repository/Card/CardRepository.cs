using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.Repository.Card
{
    public class CardRepository : BaseRepository, ICardRepository
    {
        private readonly GravitasDbContext _dbContext;
        public CardRepository(GravitasDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public string GetContainerCardNo(long id)
        {
            var cardNo =_dbContext.Cards.FirstOrDefault(e => e.TicketContainerId == id && e.TypeId == CardType.TicketCard)?.No.ToString()
                    .Substring(2, 4);
           
            return cardNo;
        }
    }
}