using System;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.OwnTransport.DTO;

namespace Gravitas.DAL.Repository.OwnTransport
{
    public class OwnTransportRepository : BaseRepository, IOwnTransportRepository
    {
        private readonly GravitasDbContext _context;

        public OwnTransportRepository( GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<OwnTransportVm> GetList()
        {
            var list = GetQuery<Model.DomainModel.OwnTransport.DAO.OwnTransport, int>()
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
            var item = GetFirstOrDefault<Model.DomainModel.OwnTransport.DAO.OwnTransport, int>(x => 
                x.CardId == model.Card || x.TruckNo == model.TruckNo);

            if (item != null) return false;
            
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var card = _context.Cards.FirstOrDefault(x => x.Id == model.Card);
                    if (card == null) throw new Exception("Card not found");
                    card.IsOwn = true;
                    Update<Model.DomainModel.Card.DAO.Card, string>(card);
                    
                    Add<Model.DomainModel.OwnTransport.DAO.OwnTransport, int>(new Model.DomainModel.OwnTransport.DAO.OwnTransport
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
        
        public void Remove(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var ownTransport = _context.OwnTransports.FirstOrDefault(x => x.Id == id);
                    if (ownTransport == null) return;
                    
                    var card = _context.Cards.FirstOrDefault(x => x.Id == ownTransport.CardId);
                    if (card == null) throw new Exception("Card not found");
                    card.IsOwn = false;
                    Update<Model.DomainModel.Card.DAO.Card, string>(card);

                    Delete<Model.DomainModel.OwnTransport.DAO.OwnTransport, int>(ownTransport);
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