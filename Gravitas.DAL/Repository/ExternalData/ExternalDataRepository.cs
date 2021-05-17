using System.Linq;
using Gravitas.Model.Dto;

namespace Gravitas.DAL
{
    public class ExternalDataRepository : BaseRepository<GravitasDbContext>, IExternalDataRepository
    {
        private readonly GravitasDbContext _context;

        public ExternalDataRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        //Get Detail
        public ExternalData.AcceptancePointDetail GetAcceptancePointDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.AcceptancePoints.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.AcceptancePointDetail
            {
                Id = dao.Id, Code = dao.Code, Name = dao.Name
            };
            return dto;
        }

        public ExternalData.BudgetDetail GetBudgetDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Budgets.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.BudgetDetail
            {
                Id = dao.Id, Code = dao.Code, Name = dao.Name
            };
            return dto;
        }

        public ExternalData.ContractDetail GetContractDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Contracts.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.ContractDetail
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

        public ExternalData.CropDetail GetCropDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Crops.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.CropDetail
            {
                Id = dao.Id, Code = dao.Code, Name = dao.Name
            };
            return dto;
        }

        public ExternalData.DeliveryBillStatusDetail GetDeliveryBillStatusDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.DeliveryBillStatuses.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.DeliveryBillStatusDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public ExternalData.DeliveryBillTypeDetail GetDeliveryBillTypeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.DeliveryBillTypes.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.DeliveryBillTypeDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public ExternalData.EmployeeDetail GetEmployeeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Employees.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.EmployeeDetail
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

        public ExternalData.ExternalEmployeeDetail GetExternalEmployeeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Employees.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.ExternalEmployeeDetail
            {
                Id = dao.Id, Code = dao.Code, ShortName = dao.ShortName, FullName = dao.FullName
            };
            return dto;
        }

        public ExternalData.FixedAssetDetail GetFixedAssetDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.FixedAssets.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.FixedAssetDetail
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

        public ExternalData.LabImpurityСlassifierDetail GetLabImpurityСlassifierDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.LabImpurityСlassifiers.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.LabImpurityСlassifierDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public ExternalData.LabHumidityСlassifierDetail GetLabHumidityСlassifierDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.LabHumidityСlassifiers.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.LabHumidityСlassifierDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public ExternalData.LabInfectionedСlassifierDetail GetLabInfectionedСlassifierDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.LabInfectionedСlassifiers.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.LabInfectionedСlassifierDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public ExternalData.MeasureUnitDetail GetMeasureUnitDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.MeasureUnits.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.MeasureUnitDetail
            {
                Id = dao.Id, Code = dao.Code, ShortName = dao.ShortName, FullName = dao.FullName
            };
            return dto;
        }

        public ExternalData.OriginTypeDetail GetOriginTypeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.OriginTypes.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.OriginTypeDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public ExternalData.OrganisationDetail GetOrganisationDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Organisations.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.OrganisationDetail
            {
                Id = dao.Id,
                Code = dao.Code,
                ShortName = dao.ShortName,
                FullName = dao.FullName,
                Address = dao.Address
            };
            return dto;
        }

        public ExternalData.PartnerDetail GetPartnerDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Partners.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.PartnerDetail
            {
                Id = dao.Id,
                Code = dao.Code,
                ShortName = dao.ShortName,
                FullName = dao.FullName,
                Address = dao.Address
            };
            return dto;
        }

        public ExternalData.ProductDetail GetProductDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Products.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.ProductDetail
            {
                Id = dao.Id, Code = dao.Code, ShortName = dao.ShortName, FullName = dao.FullName
            };
            return dto;
        }

        public ExternalData.ReasonForRefundDetail GetReasonForRefundDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.ReasonForRefunds.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.ReasonForRefundDetail
            {
                Id = dao.Id, Code = dao.Code, Name = dao.Name
            };
            return dto;
        }

        public ExternalData.RouteDetail GetRouteDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Routes.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.RouteDetail
            {
                Id = dao.Id, Code = dao.Code, Name = dao.Name
            };
            return dto;
        }

        public ExternalData.StockDetail GetStockDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Stocks.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.StockDetail
            {
                Id = dao.Id,
                Code = dao.Code,
                ShortName = dao.ShortName,
                FullName = dao.FullName,
                Address = dao.Address
            };
            return dto;
        }

        public ExternalData.SubdivisionDetail GetSubdivisionDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.Subdivisions.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.SubdivisionDetail
            {
                Id = dao.Id,
                Code = dao.Code,
                ShortName = dao.ShortName,
                FullName = dao.FullName,
                Address = dao.Address
            };
            return dto;
        }

        public ExternalData.SupplyTransportTypeDetail GetSupplyTransportTypeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.SupplyTransportTypes.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.SupplyTransportTypeDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public ExternalData.SupplyTypeDetail GetSupplyTypeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.SupplyTypes.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.SupplyTypeDetail
            {
                Id = dao.Id, Name = dao.Name
            };
            return dto;
        }

        public ExternalData.YearOfHarvestDetail GetYearOfHarvestDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var dao = _context.YearOfHarvests.FirstOrDefault(x=> x.Id == id);
            if (dao == null) return null;
            var dto = new ExternalData.YearOfHarvestDetail
            {
                Id = dao.Id, Code = dao.Code, Name = dao.Name
            };
            return dto;
        }

        public ExternalData.ProductItems GetProductChildItems(string parentId)
        {
            var result = new ExternalData.ProductItems
            {
                Items = _context.Products
                    .Where(e => e.ParentId == parentId)
                    .Select(a =>
                    new ExternalData.ProductItem
                    {
                        Id = a.Id, ShortName = a.ShortName, IsFolder = a.IsFolder
                    })
                    .OrderBy(t => t.ShortName)
                    .ToList()
            };
            return result;
        }

        public ExternalData.ProductItems GetProductItems()
        {
            var dao = _context.Products.Where(e => !e.IsFolder);
            var dto = new ExternalData.ProductItems
            {
                Items = dao.Select(e => new ExternalData.ProductItem
                {
                    Id = e.Id, Code = e.Code, ShortName = e.ShortName, FullName = e.FullName
                }).ToList()
            };
            return dto;
        }

        // Get Items

        public ExternalData.BudgetItems GetBudgetItems()
        {
            var dao = _context.Budgets.Where(e => !e.IsFolder);
            var dto = new ExternalData.BudgetItems
            {
                Items = dao.Select(e => new ExternalData.BudgetItem
                {
                    Id = e.Id, Code = e.Code, Name = e.Name
                }).ToList()
            };
            return dto;
        }

        public ExternalData.EmployeeItems GetEmployeeItems()
        {
            var dao = _context.Employees.Where(e => !e.IsFolder);
            var dto = new ExternalData.EmployeeItems
            {
                Items = dao.Select(e => new ExternalData.EmployeeItem
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

        public ExternalData.OrganisationItems GetOrganisationItems()
        {
            var dao = _context.Organisations.Where(e => !e.IsFolder);
            var dto = new ExternalData.OrganisationItems
            {
                Items = dao.Select(e => new ExternalData.OrganisationItem
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

        public ExternalData.FixedTrailerItems GetFixedTrailerItems(int pageNo, int perPageNo)
        {
            var dao = _context.FixedAssets.Where(e => !e.IsFolder);
            var dto = new ExternalData.FixedTrailerItems
            {
                Items = dao.Select(e => new ExternalData.FixedTrailerItem
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

        public ExternalData.PartnerItems GetFilteredPagePartnerItems(int pageNo, int perPageNo, string filter)
        {
            var dao = _context.Partners.Where(x => !x.IsFolder)
                .AsEnumerable()
                .Where(e =>
                 (string.IsNullOrEmpty(filter) || e.ShortName.ToLower().Contains(filter.ToLower()) ||
                                e.FullName.ToLower().Contains(filter.ToLower())))
                .Skip(perPageNo * (pageNo - 1))
                .Take(perPageNo);
            var dto = new ExternalData.PartnerItems
            {
                Items = dao.Select(e => new ExternalData.PartnerItem
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

        public ExternalData.EmployeeItems GetFilteredPageEmployeeItems(int pageNo, int perPageNo, string filter)
        {
            var dao = _context.Employees
                .Where(x => !x.IsFolder)
                .AsEnumerable()
                .Where(e =>
                 (string.IsNullOrEmpty(filter) || e.ShortName.ToLower().Contains(filter.ToLower()) ||
                                e.FullName.ToLower().Contains(filter.ToLower()))).Skip(perPageNo * (pageNo - 1)).Take(perPageNo);
            var dto = new ExternalData.EmployeeItems
            {
                Items = dao.Select(e => new ExternalData.EmployeeItem
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

        public ExternalData.StockItems GetFilteredPageStockItems(int pageNo, int perPageNo, string filter)
        {
            var dao = _context.Stocks
                .Where(x => !x.IsFolder)
                .AsEnumerable()
                .Where(e =>
                (string.IsNullOrEmpty(filter) || e.ShortName.ToLower().Contains(filter.ToLower()) ||
                                e.FullName.ToLower().Contains(filter.ToLower()))).Skip(perPageNo * (pageNo - 1)).Take(perPageNo);
            var dto = new ExternalData.StockItems
            {
                Items = dao.Select(e => new ExternalData.StockItem
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

        public ExternalData.FixedTrailerItems GetFilteredPageFixedTrailerItems(int pageNo, int perPageNo, string filter)
        {
            var dao = _context.FixedAssets
                .Where(x => !x.IsFolder)
                .AsEnumerable()
                .Where(e =>
                (string.IsNullOrEmpty(filter) || e.RegistrationNo.ToLower().Contains(filter.ToLower())
                )).Skip(perPageNo * (pageNo - 1)).Take(perPageNo);
            var dto = new ExternalData.FixedTrailerItems
            {
                Items = dao.Select(e => new ExternalData.FixedTrailerItem
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

        public ExternalData.FixedAssetItems GetFilteredPageFixedAssetItems(int pageNo, int perPageNo, string filter)
        {
            var dao = _context.FixedAssets
                .Where(x => !x.IsFolder)
                .AsEnumerable()
                .Where(e =>
                (string.IsNullOrEmpty(filter) || e.RegistrationNo.ToLower().Contains(filter.ToLower())
                )).Skip(perPageNo * (pageNo - 1)).Take(perPageNo);
            var dto = new ExternalData.FixedAssetItems
            {
                Items = dao.Select(e => new ExternalData.FixedAssetItem
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

        public ExternalData.ProductItems GetFilteredPageProductItems(int pageNo, int perPageNo, string filter)
        {
            var dao = _context.Products
                .Where(x => !x.IsFolder)
                .AsEnumerable()
                .Where(e =>
                (string.IsNullOrEmpty(filter) || e.FullName.ToLower().Contains(filter.ToLower()) ||
                                e.ShortName.ToLower().Contains(filter.ToLower())
                )).Skip(perPageNo * (pageNo - 1)).Take(perPageNo);
            var dto = new ExternalData.ProductItems
            {
                Items = dao.Select(e => new ExternalData.ProductItem
                {
                    Id = e.Id, Code = e.Code, ShortName = e.ShortName, FullName = e.FullName
                }).ToList()
            };
            return dto;
        }

        public ExternalData.PartnerItems GetPartnerItems()
        {
            var dao = _context.Partners.Where(e => !e.IsFolder);
            var dto = new ExternalData.PartnerItems
            {
                Items = dao.Select(e => new ExternalData.PartnerItem
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

        public ExternalData.StockItems GetStockItems()
        {
            var dao = _context.Stocks.Where(e => !e.IsFolder);
            var dto = new ExternalData.StockItems
            {
                Items = dao.Select(e => new ExternalData.StockItem
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

        public ExternalData.SupplyTransportTypeItems GetSupplyTransportTypeItems()
        {
            var dao = GetQuery<Model.ExternalData.SupplyTransportType, string>();
            var dto = new ExternalData.SupplyTransportTypeItems
            {
                Items = dao.Select(e => new ExternalData.SupplyTransportTypeItem
                {
                    Id = e.Id, Name = e.Name
                }).ToList()
            };
            return dto;
        }

        public ExternalData.LabHumidityСlassifierItems GetLabHumidityСlassifierItems()
        {
            var dao = GetQuery<Model.ExternalData.LabHumidityСlassifier, string>();
            var dto = new ExternalData.LabHumidityСlassifierItems
            {
                Items = dao.Select(e => new ExternalData.LabHumidityСlassifierItem
                {
                    Id = e.Id, Name = e.Name
                }).ToList()
            };
            return dto;
        }

        public ExternalData.LabImpurityСlassifierItems GetLabImpurityСlassifierItems()
        {
            var dao = GetQuery<Model.ExternalData.LabImpurityСlassifier, string>();
            var dto = new ExternalData.LabImpurityСlassifierItems
            {
                Items = dao.Select(e => new ExternalData.LabImpurityСlassifierItem
                {
                    Id = e.Id, Name = e.Name
                }).ToList()
            };
            return dto;
        }

        public ExternalData.LabInfectionedСlassifierItems GetLabInfectionedСlassifierItems()
        {
            var dao = GetQuery<Model.ExternalData.LabInfectionedСlassifier, string>();
            var dto = new ExternalData.LabInfectionedСlassifierItems
            {
                Items = dao.Select(e => new ExternalData.LabInfectionedСlassifierItem
                {
                    Id = e.Id, Name = e.Name
                }).ToList()
            };
            return dto;
        }

        public ExternalData.LabDeviceResultTypeItems GetLabDevResultTypeItems()
        {
            var dao = GetQuery<Model.ExternalData.LabDeviceResultType, string>();
            var dto = new ExternalData.LabDeviceResultTypeItems
            {
                Items = dao.Select(e => new ExternalData.LabDeviceResultTypeItem
                {
                    Id = e.Id, Name = e.Name
                }).ToList()
            };
            return dto;
        }

        public ExternalData.BudgetItems GetBudgetChildItem(string parentId)
        {
            var result = new ExternalData.BudgetItems
            {
                Items = _context.Budgets.Where(e => e.ParentId == parentId)
                    .Select(a => new ExternalData.BudgetItem
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Code = a.Code,
                        IsFolder = a.IsFolder
                    }).OrderBy(t => t.Name).ToList()
            };
            return result;
        }

        public ExternalData.PartnerItems GetPartnerChildItems(string parentId)
        {
            var result = new ExternalData.PartnerItems
            {
                Items = _context.Partners
                    .Where(e => e.ParentId == parentId)
                    .Select(a => new ExternalData.PartnerItem
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

        public ExternalData.EmployeeItems GetEmployeeChildItems(string parentId)
        {
            var result = new ExternalData.EmployeeItems
            {
                Items = _context.Employees
                    .Where(e => e.ParentId == parentId)
                    .Select(a => new ExternalData.EmployeeItem
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