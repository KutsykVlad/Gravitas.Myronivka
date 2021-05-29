using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Attribute;
using Gravitas.Platform.Web.ViewModel.Dashboard;
using NLog;

namespace Gravitas.Platform.Web.Controllers.Api
{
    [AllowCrossSiteJson]
    public class DashboardController : ApiController
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IOpDataRepository _opDataRepository;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly INodeRepository _nodeRepository;
        private readonly GravitasDbContext _context;
        
        public DashboardController(IOpDataRepository opDataRepository, 
            IRoutesInfrastructure routesInfrastructure, 
            IExternalDataRepository externalDataRepository, 
            INodeRepository nodeRepository, 
            GravitasDbContext context)
        {
            _opDataRepository = opDataRepository;
            _routesInfrastructure = routesInfrastructure;
            _externalDataRepository = externalDataRepository;
            _nodeRepository = nodeRepository;
            _context = context;
        }

        [HttpGet]
        public IHttpActionResult Test()
        {
            return Json(5);
        }
        
        [HttpGet]
        public IHttpActionResult GetAllInQueue()
        {
            try
            {
                var outside = _context.Cards.Where(x =>
                        x.TicketContainerId.HasValue && x.TypeId == CardType.TicketCard)
                    .Select(x => new
                    {
                        TicketContainerId = x.TicketContainerId.Value,
                        Tickets = _context.Tickets.Where(z => z.TicketContainerId == x.TicketContainerId).ToList()
                    })
                    .OrderBy(x => x.TicketContainerId)
                    .Where(x => x.Tickets.Any(z => z.StatusId == TicketStatus.ToBeProcessed)
                                && x.Tickets.All(z => z.StatusId != TicketStatus.Completed
                                                      && z.StatusId != TicketStatus.Processing
                                                      && z.StatusId != TicketStatus.Closed))
                    .Select(x => _context.SingleWindowOpDatas.FirstOrDefault(z => z.TicketContainerId == x.TicketContainerId
                                                                                  && z.StateId == OpDataState.Processed))
                    .AsEnumerable()
                    .Select(x =>
                    {
                        var truck = new TruckViewModel
                        {
                            PhoneNo = x.ContactPhoneNo,
                            DocumentTypeId = ExternalData.DeliveryBill.Type.Incoming == x.DocumentTypeId ? 0 : 1,
                            LastNodeId = _opDataRepository.GetLastOpData(x.TicketId).NodeId.Value,
                            FutureNodeIds = _routesInfrastructure.GetNextNodes(x.TicketId.Value)
                        };
                        if (x.IsThirdPartyCarrier)
                        {
                            truck.TruckNo = x.HiredTransportNumber;
                            truck.TrailerNo = x.HiredTrailerNumber;
                            truck.DriverName = x.HiredDriverCode;
                        }
                        else
                        {
                            var f = _context.FixedAssets
                                .Where(z => z.Id == x.TransportId || z.Id == x.TrailerId)
                                .Select(z => new
                                {
                                    z.Id,
                                    z.RegistrationNo
                                })
                                .ToList();
                            var e = _context.Employees
                                .Where(z => z.Id == x.DriverOneId || z.Id == x.DriverTwoId)
                                .Select(z => new
                                {
                                    z.Id,
                                    z.ShortName
                                })
                                .ToList();
                            truck.TruckNo = f.FirstOrDefault(z => z.Id == x.TransportId)?.RegistrationNo;
                            truck.TrailerNo = f.FirstOrDefault(z => z.Id == x.TrailerId)?.RegistrationNo;
                            truck.DriverName =
                                $"{e.FirstOrDefault(z => z.Id == x.DriverOneId)?.ShortName} / " +
                                $"{e.FirstOrDefault(z => z.Id == x.DriverTwoId)?.ShortName}";
                        }

                        return truck;
                    })
                    .ToList();

                return Json(outside);
            }
            catch (Exception e)
            {
                _logger.Error($"Dashboard: GetAllInQueue: {e}");
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        public IHttpActionResult GetOutsideTrucksToNodes([FromUri]int[] nodeIds)
        {
            try
            {
                var r = nodeIds.ToDictionary(nodeId => nodeId, nodeId => 0);

                var containers = GetOutsideContainers();
                foreach (var containerId in containers)
                {
                    var ticket = _context.Tickets.First(z => z.TicketContainerId == containerId && z.StatusId == TicketStatus.ToBeProcessed);
                            
                    var nodeAvailableList = nodeIds.Where(z => _routesInfrastructure.IsNodeAvailable(z, ticket.RouteTemplateId.Value)).ToList();

                    foreach (var nodeId in nodeAvailableList)
                    {
                        r[nodeId]++;
                    }
                }
            
                return Json(r);
            }
            catch (Exception e)
            {
                _logger.Error($"Dashboard: GetOutsideTrucksToNodes: {e}");
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        public IHttpActionResult GetAllInside()
        {
            try
            {
                var result = _context.Cards.Where(x =>
                        x.TicketContainerId.HasValue && x.TypeId == CardType.TicketCard)
                    .Select(x => new
                    {
                        TicketContainerId = x.TicketContainerId.Value,
                        Tickets = _context.Tickets.Where(z => z.TicketContainerId == x.TicketContainerId).ToList()
                    })
                    .OrderBy(x => x.TicketContainerId)
                    .Where(x => x.Tickets.Any(z => z.StatusId == TicketStatus.Processing))
                    .Select(x => _context.SingleWindowOpDatas.FirstOrDefault(z => z.TicketContainerId == x.TicketContainerId
                                                                                  && z.StateId == OpDataState.Processed))
                    .AsEnumerable()
                    .Select(x =>
                    {
                        var truck = new TruckViewModel
                        {
                            PhoneNo = x.ContactPhoneNo,
                            DocumentTypeId = ExternalData.DeliveryBill.Type.Incoming == x.DocumentTypeId ? 0 : 1,
                            LastNodeId = _opDataRepository.GetLastOpData(x.TicketId).NodeId.Value,
                            FutureNodeIds = _routesInfrastructure.GetNextNodes(x.TicketId.Value)
                        };
                        if (x.IsThirdPartyCarrier)
                        {
                            truck.TruckNo = x.HiredTransportNumber;
                            truck.TrailerNo = x.HiredTrailerNumber;
                            truck.DriverName = x.HiredDriverCode;
                        }
                        else
                        {
                            var f = _context.FixedAssets
                                .Where(z => z.Id == x.TransportId || z.Id == x.TrailerId)
                                .Select(z => new
                                {
                                    z.Id,
                                    z.RegistrationNo
                                })
                                .ToList();
                            var e = _context.Employees
                                .Where(z => z.Id == x.DriverOneId || z.Id == x.DriverTwoId)
                                .Select(z => new
                                {
                                    z.Id,
                                    z.ShortName
                                })
                                .ToList();
                            truck.TruckNo = f.FirstOrDefault(z => z.Id == x.TransportId)?.RegistrationNo;
                            truck.TrailerNo = f.FirstOrDefault(z => z.Id == x.TrailerId)?.RegistrationNo;
                            truck.DriverName =
                                $"{e.FirstOrDefault(z => z.Id == x.DriverOneId)?.ShortName} / " +
                                $"{e.FirstOrDefault(z => z.Id == x.DriverTwoId)?.ShortName}";
                        }

                        return truck;
                    })
                    .ToList();
                
                return Json(result);
            }
            catch (Exception e)
            {
                _logger.Error($"Dashboard: GetAllInside: {e}");
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        public IHttpActionResult GetNodeLoad(long nodeId)
        {
            try
            {
                var dateTo = DateTime.Now.AddHours(-1);
                var opDatas = 
                    (from opData in _context.SingleWindowOpDatas.Where(x => x.CheckOutDateTime > dateTo)
                    join ticket in _context.Tickets on opData.TicketId equals ticket.Id
                    select new { ticket, opData })
                    .AsEnumerable()
                    .Where(x =>
                    {
                        if (x.ticket.StatusId == TicketStatus.Closed || x.ticket.StatusId == TicketStatus.Completed)
                        {
                            var load = _opDataRepository.GetLastProcessed<LoadPointOpData>(x.ticket.Id);
                            var unload = _opDataRepository.GetLastProcessed<UnloadPointOpData>(x.ticket.Id);

                            return load?.NodeId == nodeId || unload?.NodeId == nodeId;
                        }

                        return false;
                    })
                    .Where(x => x.opData.NetValue.HasValue && x.opData.NetValue > 0)
                    .Select(x => x.opData.NetValue);

                var sum = 0;
                sum = opDatas.Aggregate(sum, (current, item) => (int) (current + item.Value));
            
                return Json(sum);
            }
            catch (Exception e)
            {
                _logger.Error($"Dashboard: GetNodeLoad: {e}");
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        public IHttpActionResult GetProcessedProducts(string text = null)
        {
            try
            {
                var opDatas = _context.SingleWindowOpDatas.Where(x => !string.IsNullOrEmpty(x.ProductId))
                    .Select(x => x.ProductId)
                    .Distinct()
                    .ToList()
                    .Select(x =>
                    {
                        var product = _externalDataRepository.GetProductDetail(x)?.ShortName;
                        return new ProductViewModel
                        {
                            Id = x,
                            Title = product
                        };
                    })
                    .Where(x => text == null || x.Title.ToLower().Contains(text.ToLower()));
            
                return Json(opDatas);
            }
            catch (Exception e)
            {
                _logger.Error($"Dashboard: GetProcessedProducts: {e}");
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        public IHttpActionResult GetStatisticData()
        {
            try
            {
                var tickets = _context.Tickets.Where(x =>
                        x.StatusId == TicketStatus.Closed || x.StatusId == TicketStatus.Completed)
                    .Select(x => x.Id)
                    .ToList();

                var date = DateTime.Now.AddHours(-24);
                var model = new StatisticModel
                {
                    AllInside = GetInsideContainers().Count,
                    AllOutside = GetOutsideContainers().Count,
                    ProcessedFromBeginning = tickets.Count,
                    ProcessedAtLastDay = _context.SingleWindowOpDatas
                        .Where(x => x.CheckOutDateTime > date)
                        .AsEnumerable()
                        .Count(x => tickets.Contains(x.TicketId.Value))
                };
            
                return Json(model);
            }
            catch (Exception e)
            {
                _logger.Error($"Dashboard: GetStatisticData: {e}");
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetNodes()
        {
            try
            {
                var nodes = _context.Nodes
                    .AsNoTracking()
                    .ToList()
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        State = x.IsActive ? (_nodeRepository.GetNodeDto(x.Id).Context.TicketId.HasValue ? 2 : 1) : 0
                    })
                    .ToList();
                ;
            
                return Json(nodes);
            }
            catch (Exception e)
            {
                _logger.Error($"Dashboard: GetNodes: {e}");
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        public IHttpActionResult GetProductData([FromUri]string[] productIds = null, DateTime? from = null, DateTime? to = null)
        {
            try
            {
                if (productIds == null || !productIds.Any())
                {
                    return Ok();
                }
                var opDatas = _context.SingleWindowOpDatas
                    .Where(x => productIds.Any(z => z == x.ProductId))
                    .Where(x => from == null || x.CheckOutDateTime >= from.Value)
                    .Where(x => to == null || x.CheckOutDateTime <= to.Value)
                    .Where(x => x.NetValue.HasValue && x.NetValue > 0)
                    .Select(x => new
                    {
                        x.ProductId,
                        Value = x.NetValue,
                        Time = x.CheckOutDateTime
                    })
                    .OrderBy(x => x.Time)
                    .ToList();
                
                return Json(opDatas);
            }
            catch (Exception e)
            {
                _logger.Error($"Dashboard: GetNodes: {e}");
                return BadRequest(e.Message);
            }
        }

        private List<int> GetOutsideContainers()
        {
            var containersWithCard = _context.Cards.Where(x =>
                    x.TicketContainerId.HasValue && x.TypeId == CardType.TicketCard)
                .Select(x => new
                {
                    TicketContainerId = x.TicketContainerId.Value,
                    Tickets = _context.Tickets.Where(z => z.TicketContainerId == x.TicketContainerId).ToList()
                })
                .OrderBy(x => x.TicketContainerId)
                .Where(x => x.Tickets.Any(z => z.StatusId == TicketStatus.ToBeProcessed)
                            && x.Tickets.All(z => z.StatusId != TicketStatus.Completed
                                                  && z.StatusId != TicketStatus.Processing
                                                  && z.StatusId != TicketStatus.Closed))
                .Select(x => x.TicketContainerId)
                .ToList();

            return containersWithCard;
        }
        
        private List<int> GetInsideContainers()
        {
            var containersWithCard = _context.Cards.Where(x =>
                    x.TicketContainerId.HasValue && x.TypeId == CardType.TicketCard)
                .Select(x => new
                {
                    TicketContainerId = x.TicketContainerId.Value,
                    Tickets = _context.Tickets.Where(z => z.TicketContainerId == x.TicketContainerId).ToList()
                })
                .OrderBy(x => x.TicketContainerId)
                .Where(x => x.Tickets.Any(z => z.StatusId == TicketStatus.Processing))
                .Select(x => x.TicketContainerId)
                .ToList();

            return containersWithCard;
        }
    }
}