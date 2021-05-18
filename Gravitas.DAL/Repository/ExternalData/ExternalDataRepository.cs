using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DTO.Detail;
using Gravitas.Model.DomainModel.ExternalData.Budget.DTO.Detail;
using Gravitas.Model.DomainModel.ExternalData.Budget.DTO.List;
using Gravitas.Model.DomainModel.ExternalData.Contract.DTO;
using Gravitas.Model.DomainModel.ExternalData.Crop.DTO;
using Gravitas.Model.DomainModel.ExternalData.DeliveryBill.DTO;
using Gravitas.Model.DomainModel.ExternalData.Employee.DTO.Detail;
using Gravitas.Model.DomainModel.ExternalData.Employee.DTO.List;
using Gravitas.Model.DomainModel.ExternalData.ExternalUser.DTO;
using Gravitas.Model.DomainModel.ExternalData.FixedAsset.DTO.Detail;
using Gravitas.Model.DomainModel.ExternalData.FixedAsset.DTO.List;
using Gravitas.Model.DomainModel.ExternalData.LabClassifier.DAO;
using Gravitas.Model.DomainModel.ExternalData.LabClassifier.DTO.Detail;
using Gravitas.Model.DomainModel.ExternalData.LabClassifier.DTO.List;
using Gravitas.Model.DomainModel.ExternalData.MeasureUnit.DTO;
using Gravitas.Model.DomainModel.ExternalData.Organization.DTO.Detail;
using Gravitas.Model.DomainModel.ExternalData.Organization.DTO.List;
using Gravitas.Model.DomainModel.ExternalData.Partner.DTO.Detail;
using Gravitas.Model.DomainModel.ExternalData.Partner.DTO.List;
using Gravitas.Model.DomainModel.ExternalData.Product.DTO;
using Gravitas.Model.DomainModel.ExternalData.Product.List;
using Gravitas.Model.DomainModel.ExternalData.ReasonForRefund.DTO;
using Gravitas.Model.DomainModel.ExternalData.Route.DTO;
using Gravitas.Model.DomainModel.ExternalData.Stock.DTO.Detail;
using Gravitas.Model.DomainModel.ExternalData.Stock.DTO.List;
using Gravitas.Model.DomainModel.ExternalData.Subdivision.DTO;
using Gravitas.Model.DomainModel.ExternalData.SupplyTransportType.DAO;
using Gravitas.Model.DomainModel.ExternalData.SupplyTransportType.DTO.Detail;
using Gravitas.Model.DomainModel.ExternalData.SupplyTransportType.DTO.List;
using Gravitas.Model.DomainModel.ExternalData.SupplyType.DTO;
using Gravitas.Model.DomainModel.ExternalData.YearOfHarvest.DTO;

namespace Gravitas.DAL.Repository.ExternalData
{
    public class ExternalDataRepository : BaseRepository, IExternalDataRepository
    {
        private readonly GravitasDbContext _context;

        public ExternalDataRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        //Get Detail
        public AcceptancePointDetail GetAcceptancePointDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.AcceptancePoints.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new AcceptancePointDetail
            {
                Id = dao.Id, Code = dao.Code, Name = dao.Name
            };
            return dto;
        }

        public BudgetDetail GetBudgetDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Budgets.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new BudgetDetail
            {
                Id = dao.Id, Code = dao.Code, Name = dao.Name
            };
            return dto;
        }

        public ContractDetail GetContractDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Contracts.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ContractDetail
            {
                Id = dao.Id,
                Code = dao.Code,
                Name = dao.Name,
                StartDateTime = dao.StartDateTime,
                StopDateTime = dao.StopDateTime,
                ManagerId = dao.ManagerId
            };
            return dto;
        }

        public CropDetail GetCropDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Crops.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new CropDetail
            {
                Id = dao.Id, Code = dao.Code, Name = dao.Name
            };
            return dto;
        }

        public DeliveryBillStatusDetail GetDeliveryBillStatusDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.DeliveryBillStatuses.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new DeliveryBillStatusDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public DeliveryBillTypeDetail GetDeliveryBillTypeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.DeliveryBillTypes.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new DeliveryBillTypeDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public EmployeeDetail GetEmployeeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Employees.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new EmployeeDetail
            {
                Id = dao.Id,
                Code = dao.Code,
                ShortName = dao.ShortName,
                FullName = dao.FullName,
                Position = dao.Position,
                Email = dao.Email,
                PhoneNo = dao.PhoneNo
            };
            return dto;
        }

        public ExternalEmployeeDetail GetExternalEmployeeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Employees.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalEmployeeDetail
            {
                Id = dao.Id, Code = dao.Code, ShortName = dao.ShortName, FullName = dao.FullName
            };
            return dto;
        }

        public FixedAssetDetail GetFixedAssetDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.FixedAssets.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new FixedAssetDetail
            {
                Id = dao.Id,
                Code = dao.Code,
                Brand = dao.Brand,
                Model = dao.Model,
                TypeCode = dao.TypeCode,
                RegistrationNo = dao.RegistrationNo
            };
            return dto;
        }

        public LabImpurityСlassifierDetail GetLabImpurityСlassifierDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.LabImpurityСlassifiers.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new LabImpurityСlassifierDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public LabHumidityСlassifierDetail GetLabHumidityСlassifierDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.LabHumidityСlassifiers.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new LabHumidityСlassifierDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public LabInfectionedСlassifierDetail GetLabInfectionedСlassifierDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.LabInfectionedСlassifiers.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new LabInfectionedСlassifierDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public MeasureUnitDetail GetMeasureUnitDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.MeasureUnits.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new MeasureUnitDetail
            {
                Id = dao.Id, Code = dao.Code, ShortName = dao.ShortName, FullName = dao.FullName
            };
            return dto;
        }

        public OriginTypeDetail GetOriginTypeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.OriginTypes.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new OriginTypeDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public OrganisationDetail GetOrganisationDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Organisations.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new OrganisationDetail
            {
                Id = dao.Id,
                Code = dao.Code,
                ShortName = dao.ShortName,
                FullName = dao.FullName,
                Address = dao.Address
            };
            return dto;
        }

        public PartnerDetail GetPartnerDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Partners.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new PartnerDetail
            {
                Id = dao.Id,
                Code = dao.Code,
                ShortName = dao.ShortName,
                FullName = dao.FullName,
                Address = dao.Address
            };
            return dto;
        }

        public ProductDetail GetProductDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Products.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ProductDetail
            {
                Id = dao.Id, Code = dao.Code, ShortName = dao.ShortName, FullName = dao.FullName
            };
            return dto;
        }

        public ReasonForRefundDetail GetReasonForRefundDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.ReasonForRefunds.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ReasonForRefundDetail
            {
                Id = dao.Id, Code = dao.Code, Name = dao.Name
            };
            return dto;
        }

        public RouteDetail GetRouteDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Routes.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new RouteDetail
            {
                Id = dao.Id, Code = dao.Code, Name = dao.Name
            };
            return dto;
        }

        public StockDetail GetStockDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Stocks.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new StockDetail
            {
                Id = dao.Id,
                Code = dao.Code,
                ShortName = dao.ShortName,
                FullName = dao.FullName,
                Address = dao.Address
            };
            return dto;
        }

        public SubdivisionDetail GetSubdivisionDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Subdivisions.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new SubdivisionDetail
            {
                Id = dao.Id,
                Code = dao.Code,
                ShortName = dao.ShortName,
                FullName = dao.FullName,
                Address = dao.Address
            };
            return dto;
        }

        public SupplyTransportTypeDetail GetSupplyTransportTypeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.SupplyTransportTypes.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new SupplyTransportTypeDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public SupplyTypeDetail GetSupplyTypeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.SupplyTypes.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new SupplyTypeDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public YearOfHarvestDetail GetYearOfHarvestDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.YearOfHarvests.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new YearOfHarvestDetail
            {
                Id = dao.Id, Code = dao.Code, Name = dao.Name
            };
            return dto;
        }

        public ProductItems GetProductChildItems(string parentId)
        {
            var result = new ProductItems
            {
                Items = _context.Products
                    .Where(e => e.ParentId == parentId)
                    .Select(a =>
                    new ProductItem
                    {
                        Id = a.Id, ShortName = a.ShortName, IsFolder = a.IsFolder
                    })
                    .OrderBy(t => t.ShortName)
                    .ToList()
            };
            return result;
        }

        public ProductItems GetProductItems()
        {
            var dao = _context.Products.Where(e => !e.IsFolder);
            var dto = new ProductItems
            {
                Items = dao.Select(e => new ProductItem
                {
                    Id = e.Id, Code = e.Code, ShortName = e.ShortName, FullName = e.FullName
                }).ToList()
            };
            return dto;
        }

        // Get Items

        public BudgetItems GetBudgetItems()
        {
            var dao = _context.Budgets.Where(e => !e.IsFolder);
            var dto = new BudgetItems
            {
                Items = dao.Select(e => new BudgetItem
                {
                    Id = e.Id, Code = e.Code, Name = e.Name
                }).ToList()
            };
            return dto;
        }

        public EmployeeItems GetEmployeeItems()
        {
            var dao = _context.Employees.Where(e => !e.IsFolder);
            var dto = new EmployeeItems
            {
                Items = dao.Select(e => new EmployeeItem
                {
                    Id = e.Id,
                    Code = e.Code,
                    ShortName = e.ShortName,
                    FullName = e.FullName,
                    Position = e.Position,
                    Email = e.Email,
                    PhoneNo = e.PhoneNo
                }).ToList()
            };
            return dto;
        }

        public OrganisationItems GetOrganisationItems()
        {
            var dao = _context.Organisations.Where(e => !e.IsFolder);
            var dto = new OrganisationItems
            {
                Items = dao.Select(e => new OrganisationItem
                {
                    Id = e.Id,
                    Code = e.Code,
                    ShortName = e.ShortName,
                    FullName = e.FullName,
                    Address = e.Address
                }).ToList()
            };
            return dto;
        }

        public FixedTrailerItems GetFixedTrailerItems(int pageNo, int perPageNo)
        {
            var dao = _context.FixedAssets.Where(e => !e.IsFolder);
            var dto = new FixedTrailerItems
            {
                Items = dao.Select(e => new FixedTrailerItem
                {
                    Id = e.Id,
                    Code = e.Code,
                    TypeCode = e.TypeCode,
                    Model = e.Model,
                    Brand = e.Brand,
                    RegistrationNo = e.RegistrationNo
                }).Skip(perPageNo * pageNo).Take(perPageNo).ToList()
            };
            return dto;
        }

        public PartnerItems GetFilteredPagePartnerItems(int pageNo, int perPageNo, string filter)
        {
            var dao = _context.Partners.Where(x => !x.IsFolder)
                .AsEnumerable()
                .Where(e =>
                 (string.IsNullOrEmpty(filter) || e.ShortName.ToLower().Contains(filter.ToLower()) ||
                                e.FullName.ToLower().Contains(filter.ToLower())))
                .Skip(perPageNo * (pageNo - 1))
                .Take(perPageNo);
            var dto = new PartnerItems
            {
                Items = dao.Select(e => new PartnerItem
                {
                    Id = e.Id,
                    Code = e.Code,
                    ShortName = e.ShortName,
                    FullName = e.FullName,
                    Address = e.Address
                }).ToList()
            };
            return dto;
        }

        public EmployeeItems GetFilteredPageEmployeeItems(int pageNo, int perPageNo, string filter)
        {
            var dao = _context.Employees
                .Where(x => !x.IsFolder)
                .AsEnumerable()
                .Where(e =>
                 (string.IsNullOrEmpty(filter) || e.ShortName.ToLower().Contains(filter.ToLower()) ||
                                e.FullName.ToLower().Contains(filter.ToLower()))).Skip(perPageNo * (pageNo - 1)).Take(perPageNo);
            var dto = new EmployeeItems
            {
                Items = dao.Select(e => new EmployeeItem
                {
                    Id = e.Id,
                    Code = e.Code,
                    ShortName = e.ShortName,
                    FullName = e.FullName,
                    Position = e.Position,
                    Email = e.Email,
                    PhoneNo = e.PhoneNo
                }).ToList()
            };
            return dto;
        }

        public StockItems GetFilteredPageStockItems(int pageNo, int perPageNo, string filter)
        {
            var dao = _context.Stocks
                .Where(x => !x.IsFolder)
                .AsEnumerable()
                .Where(e =>
                (string.IsNullOrEmpty(filter) || e.ShortName.ToLower().Contains(filter.ToLower()) ||
                                e.FullName.ToLower().Contains(filter.ToLower()))).Skip(perPageNo * (pageNo - 1)).Take(perPageNo);
            var dto = new StockItems
            {
                Items = dao.Select(e => new StockItem
                {
                    Id = e.Id,
                    Code = e.Code,
                    ShortName = e.ShortName,
                    FullName = e.FullName,
                    Address = e.Address
                }).ToList()
            };
            return dto;
        }

        public FixedTrailerItems GetFilteredPageFixedTrailerItems(int pageNo, int perPageNo, string filter)
        {
            var dao = _context.FixedAssets
                .Where(x => !x.IsFolder)
                .AsEnumerable()
                .Where(e =>
                (string.IsNullOrEmpty(filter) || e.RegistrationNo.ToLower().Contains(filter.ToLower())
                )).Skip(perPageNo * (pageNo - 1)).Take(perPageNo);
            var dto = new FixedTrailerItems
            {
                Items = dao.Select(e => new FixedTrailerItem
                {
                    Id = e.Id,
                    Code = e.Code,
                    TypeCode = e.TypeCode,
                    Model = e.Model,
                    Brand = e.Brand,
                    RegistrationNo = e.RegistrationNo
                }).ToList()
            };
            return dto;
        }

        public FixedAssetItems GetFilteredPageFixedAssetItems(int pageNo, int perPageNo, string filter)
        {
            var dao = _context.FixedAssets
                .Where(x => !x.IsFolder)
                .AsEnumerable()
                .Where(e =>
                (string.IsNullOrEmpty(filter) || e.RegistrationNo.ToLower().Contains(filter.ToLower())
                )).Skip(perPageNo * (pageNo - 1)).Take(perPageNo);
            var dto = new FixedAssetItems
            {
                Items = dao.Select(e => new FixedAssetItem
                {
                    Id = e.Id,
                    Code = e.Code,
                    TypeCode = e.TypeCode,
                    Model = e.Model,
                    Brand = e.Brand,
                    RegistrationNo = e.RegistrationNo
                }).ToList()
            };
            return dto;
        }

        public ProductItems GetFilteredPageProductItems(int pageNo, int perPageNo, string filter)
        {
            var dao = _context.Products
                .Where(x => !x.IsFolder)
                .AsEnumerable()
                .Where(e =>
                (string.IsNullOrEmpty(filter) || e.FullName.ToLower().Contains(filter.ToLower()) ||
                                e.ShortName.ToLower().Contains(filter.ToLower())
                )).Skip(perPageNo * (pageNo - 1)).Take(perPageNo);
            var dto = new ProductItems
            {
                Items = dao.Select(e => new ProductItem
                {
                    Id = e.Id, Code = e.Code, ShortName = e.ShortName, FullName = e.FullName
                }).ToList()
            };
            return dto;
        }

        public PartnerItems GetPartnerItems()
        {
            var dao = _context.Partners.Where(e => !e.IsFolder);
            var dto = new PartnerItems
            {
                Items = dao.Select(e => new PartnerItem
                {
                    Id = e.Id,
                    Code = e.Code,
                    ShortName = e.ShortName,
                    FullName = e.FullName,
                    Address = e.Address
                }).ToList()
            };
            return dto;
        }

        public StockItems GetStockItems()
        {
            var dao = _context.Stocks.Where(e => !e.IsFolder);
            var dto = new StockItems
            {
                Items = dao.Select(e => new StockItem
                {
                    Id = e.Id,
                    Code = e.Code,
                    ShortName = e.ShortName,
                    FullName = e.FullName,
                    Address = e.Address
                }).ToList()
            };
            return dto;
        }

        public SupplyTransportTypeItems GetSupplyTransportTypeItems()
        {
            var dao = GetQuery<SupplyTransportType, string>();
            var dto = new SupplyTransportTypeItems
            {
                Items = dao.Select(e => new SupplyTransportTypeItem
                {
                    Id = e.Id, Name = e.Name
                }).ToList()
            };
            return dto;
        }

        public LabHumidityСlassifierItems GetLabHumidityСlassifierItems()
        {
            var dao = GetQuery<LabHumidityСlassifier, string>();
            var dto = new LabHumidityСlassifierItems
            {
                Items = dao.Select(e => new LabHumidityСlassifierItem
                {
                    Id = e.Id, Name = e.Name
                }).ToList()
            };
            return dto;
        }

        public LabImpurityСlassifierItems GetLabImpurityСlassifierItems()
        {
            var dao = GetQuery<LabImpurityСlassifier, string>();
            var dto = new LabImpurityСlassifierItems
            {
                Items = dao.Select(e => new LabImpurityСlassifierItem
                {
                    Id = e.Id, Name = e.Name
                }).ToList()
            };
            return dto;
        }

        public LabInfectionedСlassifierItems GetLabInfectionedСlassifierItems()
        {
            var dao = GetQuery<LabInfectionedСlassifier, string>();
            var dto = new LabInfectionedСlassifierItems
            {
                Items = dao.Select(e => new LabInfectionedСlassifierItem
                {
                    Id = e.Id, Name = e.Name
                }).ToList()
            };
            return dto;
        }

        public LabDeviceResultTypeItems GetLabDevResultTypeItems()
        {
            var dao = GetQuery<LabDeviceResultType, string>();
            var dto = new LabDeviceResultTypeItems
            {
                Items = dao.Select(e => new LabDeviceResultTypeItem
                {
                    Id = e.Id, Name = e.Name
                }).ToList()
            };
            return dto;
        }

        public BudgetItems GetBudgetChildItem(string parentId)
        {
            var result = new BudgetItems
            {
                Items = _context.Budgets.Where(e => e.ParentId == parentId)
                    .Select(a => new BudgetItem
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Code = a.Code,
                        IsFolder = a.IsFolder
                    }).OrderBy(t => t.Name).ToList()
            };
            return result;
        }

        public PartnerItems GetPartnerChildItems(string parentId)
        {
            var result = new PartnerItems
            {
                Items = _context.Partners
                    .Where(e => e.ParentId == parentId)
                    .Select(a => new PartnerItem
                    {
                        Id = a.Id,
                        Code = a.Code,
                        ShortName = a.ShortName,
                        FullName = a.FullName,
                        Address = a.Address,
                        IsFolder = a.IsFolder
                    }).ToList()
            };
            return result;
        }

        public EmployeeItems GetEmployeeChildItems(string parentId)
        {
            var result = new EmployeeItems
            {
                Items = _context.Employees
                    .Where(e => e.ParentId == parentId)
                    .Select(a => new EmployeeItem
                    {
                        Id = a.Id,
                        Code = a.Code,
                        ShortName = a.ShortName,
                        FullName = a.FullName,
                        Email = a.Email,
                        PhoneNo = a.PhoneNo,
                        Position = a.Position,
                        IsFolder = a.IsFolder
                    }).ToList()
            };
            return result;
        }
    }
}