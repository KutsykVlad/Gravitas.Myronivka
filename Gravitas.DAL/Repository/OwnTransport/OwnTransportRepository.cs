using System;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.OwnTransport.DTO;

namespace Gravitas.DAL.Repository.OwnTransport
{
    public class OwnTransportRepository : BaseRepository<GravitasDbContext>, IOwnTransportRepository
    {
        private readonly GravitasDbContext _context;

        public OwnTransportRepository( GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<OwnTransportVm> GetList()
        {
            var list = GetQuery<Model.DomainModel.OwnTransport.DAO.OwnTransport, long>()
                .Select(x => new OwnTransportVm
                {
                    Id = x.Id, 
                    Card = x.CardId,
                    TruckNo = x.TruckNo,
                    TrailerNo = x.TrailerNo
                });
            return list;
        }

        public bool Add(OwnTransportVm model)
        {
            var item = GetFirstOrDefault<Model.DomainModel.OwnTransport.DAO.OwnTransport, long>(x => 
                x.CardId == model.Card || x.TruckNo == model.TruckNo);

            if (item != null) return false;
            
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var card = GetEntity<Card, string>(model.Card);
                    if (card == null) throw new Exception("Card not found");
                    card.IsOwn = true;
                    Update<Card, string>(card);
                    
                    Add<Model.DomainModel.OwnTransport.DAO.OwnTransport, long>(new Model.DomainModel.OwnTransport.DAO.OwnTransport
                    {
                        CardId = model.Card,
                        TruckNo = model.TruckNo,
                        TrailerNo = model.TrailerNo
                    });
                   
                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine(e);
                    return false;
                }
            }
        }
        
        public void Remove(long id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var ownTransport = GetEntity<Model.DomainModel.OwnTransport.DAO.OwnTransport, long>(id);
                    if (ownTransport == null) return;
                    
                    var card = GetEntity<Card, string>(ownTransport.CardId);
                    if (card == null) throw new Exception("Card not found");
                    card.IsOwn = false;
                    Update<Card, string>(card);

                    Delete<Model.DomainModel.OwnTransport.DAO.OwnTransport, long>(ownTransport);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine(e);
                }
            }
        }
    }
}