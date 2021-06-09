using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.OwnTransport.Models;
using Gravitas.Infrastructure.Common.Helper;

namespace Gravitas.DAL.Repository.OwnTransport
{
    public class OwnTransportRepository : IOwnTransportRepository
    {
        private readonly GravitasDbContext _context;

        public OwnTransportRepository( GravitasDbContext context)
        {
            _context = context;
        }

        public List<OwnTransportViewModel> GetList()
        {
            return _context.OwnTransports
                .Include(nameof(Model.DomainModel.Card.DAO.Card))
                .AsEnumerable()
                .Select(x => new OwnTransportViewModel
                {
                    Id = x.Id,
                    CardNo = x.Card.No,
                    Driver = x.Driver,
                    TruckNo = x.TruckNo,
                    TrailerNo = x.TrailerNo,
                    ExpirationDate = x.ExpirationDate,
                    TypeName = x.TypeId.GetDescription(),
                    LongRangeCardId = x.LongRangeCardId
                })
                .ToList();
        }

        public void AddOrUpdate(OwnTransportViewModel model)
        {
            var item = model.Id.HasValue
                ? _context.OwnTransports.FirstOrDefault(x => x.Id == model.Id)
                : null;

            if (item == null)
            {
                item = new Model.DomainModel.OwnTransport.DAO.OwnTransport();
                _context.OwnTransports.Add(item);
            }
            
            item.CardId = _context.Cards.First(x => x.No == model.CardNo).Id;
            item.Driver = model.Driver;
            item.TruckNo = model.TruckNo;
            item.TrailerNo = model.TrailerNo;
            item.ExpirationDate = model.ExpirationDate;
            item.TypeId = model.TypeId;
            item.LongRangeCardId = model.LongRangeCardId;
         
            _context.SaveChanges();
        }
        
        public void Remove(int id)
        {
            var item = _context.OwnTransports.First(x => x.Id == id);
            _context.OwnTransports.Remove(item);
            _context.SaveChanges();
        }

        public OwnTransportViewModel GetById(int id)
        {
            return _context.OwnTransports
                .Include(nameof(Model.DomainModel.Card.DAO.Card))
                .Where(x => x.Id == id)
                .AsEnumerable()
                .Select(x => new OwnTransportViewModel
                {
                    Id = x.Id,
                    CardNo = x.Card.No,
                    Driver = x.Driver,
                    TruckNo = x.TruckNo,
                    TrailerNo = x.TrailerNo,
                    ExpirationDate = x.ExpirationDate,
                    TypeName = x.TypeId.GetDescription(),
                    TypeId = x.TypeId,
                    LongRangeCardId = x.LongRangeCardId
                })
                .FirstOrDefault();
        }
    }
}