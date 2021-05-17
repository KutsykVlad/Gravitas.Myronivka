using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL;
using Gravitas.DAL.Repository;
using Gravitas.Model;
using Gravitas.Model.DomainModel.EmployeeRoles.DTO;
using Gravitas.Model.DomainModel.PhoneDictionary.DAO;
using Gravitas.Model.DomainModel.Queue.DAO;
using Gravitas.Model.Dto;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.OpData.NonStandart;
using Dom = Gravitas.Model.DomainValue.Dom;
using Node = Gravitas.Model.DomainModel.Node.DAO.Node;

namespace Gravitas.Platform.Web.Manager
{
    public class AdminWebManager : IAdminWebManager
    {
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly IQueueSettingsRepository _queueSettingsRepository;
        private readonly INodeRepository _nodeRepository;
        private readonly ITrafficRepository _trafficRepository;
        private readonly IEmployeeRolesRepository _employeeRolesRepository;
        private readonly IPhonesRepository _phonesRepository;
        private readonly GravitasDbContext _context;

        public AdminWebManager(IExternalDataRepository externalDataRepository,
            IQueueSettingsRepository queueSettingsRepository,
            INodeRepository nodeRepository,
            ITrafficRepository trafficRepository,
            IEmployeeRolesRepository employeeRolesRepository,
            IPhonesRepository phonesRepository, 
            GravitasDbContext context)
        {
            _externalDataRepository = externalDataRepository;
            _queueSettingsRepository = queueSettingsRepository;
            _nodeRepository = nodeRepository;
            _trafficRepository = trafficRepository;
            _employeeRolesRepository = employeeRolesRepository;
            _phonesRepository = phonesRepository;
            _context = context;
        }

        
        #region Queue

        public QueuePatternVm GetQueueTable()
        {
            var categories = _queueSettingsRepository.GetCategories();
            var priorities = _queueSettingsRepository.GetPriorities();
            var result = new QueuePatternVm
            {
                Items = _queueSettingsRepository.GetQueuePatternItems()
                    .ToList()
                    .Select(t => new QueuePatternItemVm
                    {
                        QueuePatternItemId = t.Id,
                        Count = t.Count,
                        Priority = t.PriorityId,
                        ReceiverId = t.PartnerId,
                        Category =t.CategoryId,
                        CategoryDescription = categories.FirstOrDefault(category => category.Id == t.CategoryId).Description,
                        PriorityDescription = priorities.FirstOrDefault(priority => priority.Id == t.PriorityId).Description,
                        ReceiverName = t.PartnerId == null? null :_externalDataRepository.GetPartnerDetail(t.PartnerId).ShortName,
                        IsFixed = t.CategoryId != Dom.Queue.Category.Partners
                    })
                    .OrderBy(vm => vm.Category)
                    .ToList()
            };

            result.Count = result.Items.Count;
           
            return result;
        }

        public void UpdateQueuePatternItem(QueuePatternItemVm itemVm)
        {
            _queueSettingsRepository.AddOrUpdate<QueuePatternItem, long>(new QueuePatternItem
            {
                CategoryId =
                   itemVm.Category,
                Id = itemVm.QueuePatternItemId,
                Count = itemVm.Count,
                PartnerId = itemVm.ReceiverId,
                PriorityId =
                    itemVm.Priority
            });
        }

        public void DeleteQueuePatternItem(QueuePatternItemVm itemVm)
        {
            _queueSettingsRepository.Delete<QueuePatternItem, long>(
                _context.QueuePatternItems.First(x => x.Id == itemVm.QueuePatternItemId));
        }

        private int CountInQueue(long nodeId)
        {
            return _trafficRepository.GetNodeTrafficHistory(nodeId).Items.Count(t => t.DepartureTime == null);
        }

        #endregion

        #region NodesEdit

        public NodeEditListVm GetNodesEditTable()
        {
            var vm = new NodeEditListVm
            {
                Items = _nodeRepository.GetQuery<Node, long>()
                    .Select(node => new NodeEditVm
                    {
                        Id = node.Id,
                        Name = node.Name,
                        Quota = node.Quota, 
                        MaximumProcessingTime = node.MaximumProcessingTime
                    })
                    .ToList()
            };

            vm.Count = vm.Items.Count;
            return vm;
        }

        public void UpdateNodeItem(NodeEditVm vm)
        {
            var current = _context.Nodes.FirstOrDefault(x => x.Id == vm.Id);
            if (current != null)
            {
                current.Quota = vm.Quota;
                current.MaximumProcessingTime = vm.MaximumProcessingTime;
                _nodeRepository.Update<Node, long>(current);
            }
        }

        #endregion

        #region Traffic

        
        public NodesTrafficListVm GetWholeNodeTrafficTable()
        {
            var vm = new NodesTrafficListVm
            {
                Items = _nodeRepository.GetQuery<Node, long>()
                    .Select(t => new NodeTrafficListVm
                    {
                        NodeName = t.Name, 
                        NodeId = t.Id
                    }),
                TerminatedItems = GetTerminatedTraffic()
            };

            foreach (var item in vm.Items)
            {
                item.Items = _trafficRepository.GetNodeTrafficHistory(item.NodeId).Items
                    .Select(tmp => new NodeTrafficItem
                    {
                        TicketContainerId = tmp.TicketContainerId,
                        EntranceTime = tmp.EntranceTime,
                        DepartureTime = tmp.DepartureTime,
                        NodeId = item.NodeId,
                        NodeName = item.NodeName
                    });

                var handledRecords = item.Items.Where(s => s.DepartureTime != null).ToArray();

                if (handledRecords.Any())
                {
                    var avg = handledRecords.Average(i => (i.DepartureTime - i.EntranceTime).Value.Minutes);
                    item.AverageTime = (long)avg;
                }
                else
                {
                    item.AverageTime = 0;
                }
                

                item.ElementsInQueue = CountInQueue(item.NodeId);
                item.Count = item.Items.Count();
            }

            vm.WholeList = true;

            return vm;
        }

        public NodesTrafficListVm GetCurrentNodeTrafficTable()
        {
            var vm = new NodesTrafficListVm
            {
                Items = _nodeRepository.GetQuery<Node, long>()
                    .Select(t => new NodeTrafficListVm
                    {
                        NodeName = t.Name,
                        NodeId = t.Id, 
                        AverageTime = t.MaximumProcessingTime
                    }),
                TerminatedItems = GetTerminatedTraffic()
            };

            foreach (var item in vm.Items)
            {
                item.Items = _trafficRepository.GetNodeTrafficHistory(item.NodeId).Items
                    .Where(t => t.DepartureTime == null).Select(t=> new NodeTrafficItem
                    {
                        NodeId = t.NodeId,
                        EntranceTime = t.EntranceTime,
                        NodeName = t.Node.Name,
                        TicketContainerId = t.TicketContainerId
                    }).ToList();
                item.ElementsInQueue = CountInQueue(item.NodeId);

                item.Count = item.Items.Count();
            }

            vm.WholeList = false;

            return vm;
        }

        private IQueryable<NodeTrafficItem> GetTerminatedTraffic()
        {
            var result = _context.TrafficHistories
                .Where(t => t.DepartureTime == null)
                .AsEnumerable()
                .Where(t => (t.EntranceTime == null || DateTime.Now.Subtract(t.EntranceTime.Value).TotalMinutes > t.Node.MaximumProcessingTime))
                .Select(x => new NodeTrafficItem
                {
                    NodeId = x.NodeId,
                    NodeName = x.Node.Name, 
                    EntranceTime = x.EntranceTime,
                    TicketContainerId = x.TicketContainerId

                });
            return result.AsQueryable();
        }

        #endregion

        #region Roles
        
        public RoleTableVm GetRoleTableVm()
        {
            var nodes = _nodeRepository.GetNodeItems();

            var result = new RoleTableVm
            {
                Roles = _employeeRolesRepository.GetRoles().Items
                    .Select(t => new RoleVm
                    {
                        Assignments = t.Nodes.Select(a => new AssignmentVm
                        {
                            NodeInfo = new NodeInfo
                            {
                                NodeId = a,
                                NodeName = nodes.Items.FirstOrDefault(f=>f.Id == a)?.Name
                            },
                            IsAssigned = true
                        }).ToList(),
                        RoleId = t.RoleId,
                        RoleName = t.Name
                    })
                    .OrderBy(x => x.RoleName)
                    .ToList()
            };

            foreach (var role in result.Roles)
            {
                var assignments = role.Assignments.Select(t => t.NodeInfo.NodeId).ToList();
                foreach (var node in nodes.Items)
                {
                    if (!assignments.Contains(node.Id))
                    {
                        role.Assignments.Add(new AssignmentVm
                        {
                            NodeInfo = new NodeInfo
                            {
                                NodeId = node.Id,
                                NodeName = node.Name
                            },
                            IsAssigned = false
                        });
                    }
                }
            }

            return result;
        }

        public void UpdateRole(RoleVm role)
        {
            var roleToUpdate = new RoleDetail();
            roleToUpdate.Name = role.RoleName;
            roleToUpdate.RoleId = role.RoleId;
            var assignments = role.Assignments.Where(t => t.IsAssigned).ToList();
            roleToUpdate.Nodes = assignments.Select(t => t.NodeInfo.NodeId.Value).ToList();

            _employeeRolesRepository.UpdateRole(roleToUpdate);
        }

        public void CreateRole(RoleVm role)
        {
            _employeeRolesRepository.AddRole(new RoleDetail
            {
                Name = role.RoleName,
                Nodes = role.Assignments.Where(t => t.IsAssigned).Select(a => a.NodeInfo.NodeId.Value).ToList()
            });
        }

        public void DeleteRole(long roleId)
        {
            _employeeRolesRepository.DeleteRole(roleId);
        }

        #endregion

        #region EmployeePhones

        public List<PhoneDictionary> GetEmployeePhones()
        {
            var items = _phonesRepository.GetAll();
            return items;
        }

        public void UpdateRolePhone(PhoneDictionary employee)
        {
            _phonesRepository.Update<PhoneDictionary, long>(new PhoneDictionary
            {
                Id = employee.Id,
                PhoneNumber = employee.PhoneNumber,
                EmployeePosition = employee.EmployeePosition
            });
        }

        #endregion
    }
}