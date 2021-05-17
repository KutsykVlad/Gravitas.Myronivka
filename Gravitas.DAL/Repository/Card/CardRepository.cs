using System.Linq;
using Gravitas.Model;

namespace Gravitas.DAL
{
    public class CardRepository : BaseRepository<GravitasDbContext>, ICardRepository
    {
        private readonly GravitasDbContext _dbContext;
        public CardRepository(GravitasDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public string GetContainerCardNo(long id)
        {
            var cardNo =_dbContext.Cards.FirstOrDefault(e => e.TicketContainerId == id && e.TypeId == Dom.Card.Type.TicketCard)?.No.ToString()
                    .Substring(2, 4);
           
            return cardNo;
        }
    }
}