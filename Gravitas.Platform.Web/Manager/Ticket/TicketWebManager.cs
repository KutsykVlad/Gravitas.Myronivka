using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Card;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.ExternalData.FixedAsset.DAO;
using Gravitas.Model.DomainModel.ExternalData.Product.DAO;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.LabAverageRates;
using Gravitas.Platform.Web.ViewModel.Ticket.History;
using NLog;
using CardType = Gravitas.Model.DomainValue.CardType;
using TicketFileType = Gravitas.Model.DomainValue.TicketFileType;
using TicketStatus = Gravitas.Model.DomainValue.TicketStatus;

namespace Gravitas.Platform.Web.Manager.Ticket
{
    public class TicketWebManager : ITicketWebManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly GravitasDbContext _context;
        private readonly ICardRepository _cardRepository;
        private readonly IConnectManager _connectManager;
        private readonly INodeRepository _nodeRepository;
        private readonly IOpDataRepository _opDataRepository;
        private readonly ITicketRepository _ticketRepository;

        public TicketWebManager(GravitasDbContext context,
            ITicketRepository ticketRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            ICardRepository cardRepository,
            IConnectManager connectManager)
        {
            _context = context;
            _ticketRepository = ticketRepository;
            _nodeRepository = nodeRepository;
            _opDataRepository = opDataRepository;
            _cardRepository = cardRepository;
            _connectManager = connectManager;
        }

        public TicketItemsVm GetTicketItemsVm(int containerId)
        {
            var dto = _ticketRepository.GetTicketItems(containerId);
            var vm = Mapper.Map<TicketItemsVm>(dto);
            return vm;
        }

        public SingleWindowVms.GetTicketVm GetTicketVm(int nodeId)
        {
            var ticketStatuses = new List<TicketStatus>
            {
                TicketStatus.Processing,
                TicketStatus.ToBeProcessed,
                TicketStatus.Closed,
                TicketStatus.Completed,
                TicketStatus.New
            };
            var vm = new SingleWindowVms.GetTicketVm();

            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketContainerId == null) return vm;

            var tickets = _context.Tickets
                .Where(x => x.TicketContainerId == nodeDto.Context.TicketContainerId.Value)
                .ToList();

            var ticket = ticketStatuses
                .Select(status => tickets
                    .FirstOrDefault(x => x.StatusId == status))
                .FirstOrDefault(t => t != null);

            vm.NodeId = nodeId;
            vm.IsEditable = !SingleWindowReadonly.All.Contains((NodeIdValue)nodeId);
            vm.TicketContainerId = nodeDto.Context.TicketContainerId.Value;
            vm.TicketList = GetTicketItemsVm(nodeDto.Context.TicketContainerId.Value);
            vm.CardNumber = _cardRepository.GetFirstOrDefault<Card, string>(item =>
                item.TicketContainerId == nodeDto.Context.TicketContainerId.Value &&
                item.TypeId == CardType.TicketCard)?.No.ToString().Remove(0, 2);

            if (ticket == null) return vm;

            var singleWindowOpData = _context.SingleWindowOpDatas.AsNoTracking().First(x => x.TicketId == ticket.Id);

            vm.IsThirdPartyCarrier = singleWindowOpData.IsThirdPartyCarrier;

            if (singleWindowOpData.IsThirdPartyCarrier)
            {
                vm.TransportNo = singleWindowOpData.HiredTransportNumber;
                vm.TrailerNo = singleWindowOpData.HiredTrailerNumber;
            }
            else
            {
                var fixedAssets = _context.FixedAssets
                    .AsNoTracking()
                    .Where(x => x.Id == singleWindowOpData.TransportId || x.Id == singleWindowOpData.TrailerId)
                    .Select(x => new
                    {
                        x.Id,
                        x.RegistrationNo
                    })
                    .ToList();
                vm.TransportNo =
                    fixedAssets.FirstOrDefault(x => x.Id == singleWindowOpData.TransportId)?.RegistrationNo ??
                    string.Empty;
                vm.TrailerNo =
                    fixedAssets.FirstOrDefault(x => x.Id == singleWindowOpData.TrailerId)?.RegistrationNo ??
                    string.Empty;
            }

            return vm;
        }

        public bool UploadFile(int nodeId, HttpPostedFileBase upload, TicketFileType typeId = TicketFileType.Other)
        {
            if (upload == null) return false;

            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketId == null) return false;

            if (_ticketRepository.GetFirstOrDefault<TicketFile, int>(item =>
                    item.TicketId == nodeDto.Context.TicketId && item.TypeId == typeId) != null) return false;

            var fileName = GenerateTicketFilePath(
                GlobalConfigurationManager.TicketFilesPath,
                $"{GlobalConfigurationManager.TicketFilesPrefix}File-TicketId-{nodeDto.Context.TicketId.Value}");

            using (Stream fileStream = File.Create(fileName))
            {
                upload.InputStream.CopyTo(fileStream);
            }

            var ticketFile = new TicketFile
            {
                DateTime = DateTime.Now,
                Name = upload.FileName,
                FilePath = fileName,
                TypeId = typeId,
                TicketId = nodeDto.Context.TicketId.Value
            };
            _ticketRepository.Add<TicketFile, int>(ticketFile);

            return true;
        }

        public bool SendReplySms(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context.TicketId)
                                     ?? _opDataRepository.GetLastOpData<SingleWindowOpData>(nodeDto.Context.TicketId,
                                         null);

            Logger.Debug($"SendReplySms: Trying to send sms on {singleWindowOpData.ContactPhoneNo}");

            var card = _context.Cards.First(x => x.TicketContainerId == nodeDto.Context.TicketContainerId);
            _connectManager.SendSms(SmsTemplate.QueueRegistrationSms, nodeDto.Context.TicketId, singleWindowOpData.ContactPhoneNo, cardId: card.Id);

            Logger.Info($"SendReplySms: Sms has been send on {singleWindowOpData.ContactPhoneNo}");
            return true;
        }

        public TicketHistoryItems GetHistoryList(int pageNumber, int pageSize, string query, DateTime? date)
        {
            query = query != null ? query.ToUpper() : string.Empty;
            var nextDay = date?.AddDays(1);

            var data = (from i in _context.Set<SingleWindowOpData>()
                        join t in _context.Set<FixedAsset>() on i.TransportId equals t.Id into transportJoin
                        join tr in _context.Set<FixedAsset>() on i.TrailerId equals tr.Id into trailerJoin
                        join p in _context.Set<Product>() on i.ProductId equals p.Id into productJoin
                        join s in _context.Stocks on i.ReceiverId equals s.Id into stockJoin
                        join p in _context.Partners on i.ReceiverId equals p.Id into partnerJoin
                        join sub in _context.Subdivisions on i.ReceiverId equals sub.Id into subDivisionJoin
                        join tic in _context.Tickets on i.TicketId equals tic.Id into ticketJoin
                        from ticket in ticketJoin.DefaultIfEmpty()
                        from transport in transportJoin.DefaultIfEmpty()
                        from trailer in trailerJoin.DefaultIfEmpty()
                        from product in productJoin.DefaultIfEmpty()
                        from stock in stockJoin.DefaultIfEmpty()
                        from partner in partnerJoin.DefaultIfEmpty()
                        from subDivision in subDivisionJoin.DefaultIfEmpty()
                        where i.CheckOutDateTime.HasValue &&
                              (!date.HasValue || i.CheckOutDateTime.HasValue
                                && (i.CheckOutDateTime.Value > date && i.CheckOutDateTime.Value < nextDay.Value)) &&
                              ((transport != null && !i.IsThirdPartyCarrier ? transport.RegistrationNo.Contains(query) : i.HiredTransportNumber.Contains(query)) ||
                              (trailer != null && !i.IsThirdPartyCarrier ? trailer.RegistrationNo.Contains(query) : i.HiredTrailerNumber.Contains(query)))
                        orderby i.EditDate descending
                        select new TicketHistoryItem
                        {
                            TicketId = i.TicketId.Value,
                            TicketContainerId = ticket.TicketContainerId,
                            IsThirdPartyCarrier = i.IsThirdPartyCarrier,
                            ProductName = product != null ? product.ShortName : string.Empty,
                            EditDateTime = i.EditDate.ToString(),
                            PartnerName = i.ReceiverId != null && i.ReceiverId.HasValue ?
                                           stock.ShortName
                                               ?? stock.ShortName
                                               ?? partner.ShortName
                                               ?? subDivision.ShortName
                                               ?? "- Хибний ключ -" :
                                           string.Empty,
                            TransportNo = i.IsThirdPartyCarrier ?
                                               i.HiredTransportNumber :
                                               transport != null ? transport.RegistrationNo ?? string.Empty : string.Empty,
                            TrailerNo = i.IsThirdPartyCarrier ?
                                               i.HiredTrailerNumber :
                                               trailer != null ? trailer.RegistrationNo ?? string.Empty : string.Empty,
                        });

            var listCount = data.Count();
            var items = data
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var lastPage = (int)Math.Ceiling(decimal.Divide(listCount, pageSize));

            return new TicketHistoryItems
            {
                Items = items,
                PrevPage = Math.Max(pageNumber - 1, 1),
                NextPage = Math.Min(pageNumber + 1, lastPage),
                ItemsCount = listCount,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Query = query,
                Date = date,
            };
        }

        public TicketHistoryDetails GetHistoryDetails(int ticketId, int ticketContainerId)
        {
            var data = (from item in _context.Set<SingleWindowOpData>()
                        join t in _context.Set<FixedAsset>() on item.TransportId equals t.Id into transportJoin
                        join tr in _context.Set<FixedAsset>() on item.TrailerId equals tr.Id into trailerJoin
                        from transport in transportJoin.DefaultIfEmpty()
                        from trailer in trailerJoin.DefaultIfEmpty()
                        where item.TicketId == ticketId
                        select new TicketHistoryDetails
                        {
                            TicketId = ticketId,
                            TicketContainerId = ticketContainerId,
                            DeliveryBillId = item.DeliveryBillId,
                            TransportNo = item.IsThirdPartyCarrier ?
                                          item.HiredTransportNumber :
                                          transport != null ? transport.RegistrationNo ?? string.Empty : string.Empty,
                            TrailerNo = item.IsThirdPartyCarrier ?
                                        item.HiredTrailerNumber :
                                        trailer != null ? trailer.RegistrationNo ?? string.Empty : string.Empty,
                        }).FirstOrDefault();

            data.CardNumber = (from i in _context.Cards
                               where i.TicketContainerId == ticketContainerId &&
                                     i.TypeId == CardType.TicketCard
                               select i).FirstOrDefault()?.No.ToString().Remove(0, 2);
            return data;
        }

        public LabAverageRatesItems GetLabAverageRatesVm(DateTime date)
        {
            var vm = new LabAverageRatesItems
            {
                Items = new List<LabAverageRatesItem>()
            };

            var dateTo = date.AddDays(1);

            var opDataList = _context.LabFacelessOpDatas
                .AsNoTracking()
                .Where(item =>
                    item.CheckOutDateTime.HasValue
                    && item.CheckOutDateTime.Value > date
                    && item.CheckOutDateTime.Value < dateTo
                    && item.StateId == OpDataState.Processed)
                .AsEnumerable()
                .Select(item =>
                {
                    return (from i in _context.SingleWindowOpDatas.AsNoTracking()
                            join p in _context.Products.AsNoTracking() on i.ProductId equals p.Id into productJoin
                            join s in _context.Stocks.AsNoTracking() on i.ReceiverId equals s.Id into stockJoin
                            join p in _context.Partners.AsNoTracking() on i.ReceiverId equals p.Id into partnerJoin
                            join sub in _context.Subdivisions.AsNoTracking() on i.ReceiverId equals sub.Id into subDivisionJoin
                            from product in productJoin.DefaultIfEmpty()
                            from stock in stockJoin.DefaultIfEmpty()
                            from partner in partnerJoin.DefaultIfEmpty()
                            from subDivision in subDivisionJoin.DefaultIfEmpty()
                            where i.TicketId == item.TicketId
                            select new
                            {
                                ParnterName = i.ReceiverId != null && i.ReceiverId.HasValue ?
                                              stock.ShortName
                                                  ?? stock.ShortName
                                                  ?? partner.ShortName
                                                  ?? subDivision.ShortName
                                                  ?? "- Хибний ключ -" :
                                              string.Empty,
                                Nomenclature = product != null ? product.ShortName : null,
                                Classifier = item.HumidityClassId,
                                HumidityVal = item.HumidityValue,
                                ImpurityVal = item.ImpurityValue,
                                EffectiveVal = item.EffectiveValue,
                                TicketId = item.TicketId,
                                CheckOutDateTime = item.CheckOutDateTime,
                                Netto = (from weight in _context.ScaleOpDatas
                                         where weight.TicketId == item.TicketId &&
                                               weight.StateId == OpDataState.Processed &&
                                               weight.TypeId == ScaleOpDataType.Gross
                                         select weight.TruckWeightValue).OrderByDescending(k => k.Value).FirstOrDefault()
                                         -
                                         (from weight in _context.ScaleOpDatas
                                          where weight.TicketId == item.TicketId &&
                                                weight.StateId == OpDataState.Processed &&
                                                weight.TypeId == ScaleOpDataType.Tare
                                          select weight.TruckWeightValue).OrderByDescending(k => k.Value).FirstOrDefault()
                            }).FirstOrDefault();
                });

            var opDataItems = opDataList
                .GroupBy(item => item.ParnterName)
                .Select(item => new
                {
                    PartnerName = item.Key,
                    Nomenclature = item.GroupBy(x => x.Nomenclature)
                        .Select(x => new
                        {
                            Nomenclature = x.Key,
                            Classifier = x.GroupBy(y => y.Classifier)
                                .Select(z => new
                                {
                                    Classifier = z.Key,
                                    HumidityVal = z.Sum(y => y.HumidityVal * y.Netto) / z.Sum(c => c.Netto),
                                    ImpurityVal = z.Sum(y => y.ImpurityVal * y.Netto) / z.Sum(c => c.Netto),
                                    EffectiveVal = z.Sum(y => y.EffectiveVal * y.Netto) / z.Sum(c => c.Netto),
                                    Components = z.Select(c =>
                                    {
                                        return (from data in _context.SingleWindowOpDatas.AsNoTracking()
                                                join t in _context.FixedAssets.AsNoTracking() on data.TransportId equals t.Id into transportJoin
                                                join tr in _context.FixedAssets.AsNoTracking() on data.TrailerId equals tr.Id into trailerJoin
                                                from transport in transportJoin.DefaultIfEmpty()
                                                from trailer in trailerJoin.DefaultIfEmpty()
                                                where data.TicketId == c.TicketId
                                                select new LabAverageRatesComponent
                                                {
                                                    CheckOutDateTime = c.CheckOutDateTime,
                                                    TransportNo = data.IsThirdPartyCarrier ?
                                                                   data.HiredTransportNumber :
                                                                   transport != null ? transport.RegistrationNo ?? string.Empty : string.Empty,
                                                    TrailerNo = data.IsThirdPartyCarrier ?
                                                                   data.HiredTrailerNumber :
                                                                   trailer != null ? trailer.RegistrationNo ?? string.Empty : string.Empty,
                                                    EffectiveValue = c.EffectiveVal,
                                                    HumidityValue = c.HumidityVal,
                                                    ImpurityValue = c.ImpurityVal
                                                }).FirstOrDefault();
                                    })
                                })
                                .Where(c => !string.IsNullOrWhiteSpace(c.Classifier))
                        })
                });

            foreach (var opData in opDataItems)
                foreach (var nomenclature in opData.Nomenclature)
                    foreach (var classifier in nomenclature.Classifier)
                    {
                        vm.Items.Add(
                            new LabAverageRatesItem
                            {
                                Classifier = classifier.Classifier,
                                EffectiveValue = (float?)classifier.EffectiveVal,
                                HumidityValue = (float?)classifier.HumidityVal,
                                ImpurityValue = (float?)classifier.ImpurityVal,
                                Nomenclature = nomenclature.Nomenclature,
                                PartnerName = opData.PartnerName,
                                Components = classifier.Components.ToList()
                            });
                    }

            return vm;
        }

        private string GenerateTicketFilePath(string dir, string prefix)
        {
            var now = DateTime.Now;
            dir = Path.Combine(dir, $"{now.Year:0000}", $"{now.Month:00}", $"{now.Day:00}");
            Directory.CreateDirectory(dir);
            var path = Path.Combine(
                dir,
                $"{prefix}-{now.Year:0000}{now.Month:00}{now.Day:00}-{now.Hour:00}{now.Minute:00}{now.Second:00}{now.Millisecond:000}.jpeg");
            return path;
        }
    }
}