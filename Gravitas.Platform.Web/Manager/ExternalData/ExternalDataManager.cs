using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.ExternalData
{
    public class ExternalDataWebManager : IExternalDataWebManager
    {
        private readonly ExternalDataRepository _externalDataRepository;
        private readonly GravitasDbContext _context;

        public ExternalDataWebManager(ExternalDataRepository externalDataRepository, GravitasDbContext context)
        {
            _externalDataRepository = externalDataRepository;
            _context = context;
        }

        public OrganisationItemsVm GetOrganisationItemsVm()
        {
            var dto = _externalDataRepository.GetOrganisationItems();
            var vm = Mapper.Map<OrganisationItemsVm>(dto);
            return vm;
        }

        public ProductItemsVm GetProductItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null)
        {
            var dto = _externalDataRepository.GetFilteredPageProductItems(pageNo, perPageNo, filter);
            var vm = Mapper.Map<ProductItemsVm>(dto);
            return vm;
        }

        public EmployeeItemsVm GetEmployeeItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null)
        {
            var dto = _externalDataRepository.GetFilteredPageEmployeeItems(pageNo, perPageNo, filter);
            var vm = Mapper.Map<EmployeeItemsVm>(dto);
            return vm;
        }

        public EmployeeItemVm GetEmployeeItemVm(string id)
        {
            var employee = _context.Employees.Where(e => e.Id == id)
                .Select(e => new EmployeeItemVm
                {
                    Id = e.Id,
                    Code = e.Code,
                    ShortName = e.ShortName,
                    FullName = e.FullName,
                    Position = e.Position,
                    Email = e.Email,
                    PhoneNo = e.PhoneNo
                }).FirstOrDefault();
            return employee;
        }

        public FixedTrailerItemsVm GetFixedTrailerItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null)
        {
            var dto = _externalDataRepository.GetFilteredPageFixedTrailerItems(pageNo, perPageNo, filter);
            var vm = Mapper.Map<FixedTrailerItemsVm>(dto);
            return vm;
        }

        public FixedTrailerItemVm GetFixedTrailerItemVm(string id)
        {
            var trailer = _context.FixedAssets.Where(e => e.Id == id)
                .Select(e => new FixedTrailerItemVm
                {
                    Id = e.Id,
                    Code = e.Code,
                    Model = e.Model,
                    Brand = e.Brand,
                    RegistrationNo = e.RegistrationNo,
                    TypeCode = e.TypeCode
                }).FirstOrDefault();
            return trailer;
        }

        public FixedAssetItemsVm GetFixedAssetItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null)
        {
            var dto = _externalDataRepository.GetFilteredPageFixedAssetItems(pageNo, perPageNo, filter);
            var vm = Mapper.Map<FixedAssetItemsVm>(dto);
            return vm;
        }

        public StockItemsVm GetStockItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null)
        {
            var dto = _externalDataRepository.GetFilteredPageStockItems(pageNo, perPageNo, filter);
            var vm = Mapper.Map<StockItemsVm>(dto);
            return vm;
        }

        public StockItemVm GetStockItemVm(string id)
        {
            var stockItem = _context.Stocks.Where(e => e.Id == id)
                .Select(e => new StockItemVm
                {
                    Id = e.Id,
                    Code = e.Code,
                    ShortName = e.ShortName,
                    FullName = e.FullName,
                    Address = e.Address
                })
                .First();
            return stockItem;
        }

        public FixedAssetItemVm GetFixedAssetItem(string id)
        {
            var item =
                _context.FixedAssets.Where(t => t.Id == id)
                    .Select(t => new FixedAssetItemVm
                    {
                        Id = t.Id,
                        Code = t.Code,
                        Model = t.Model,
                        RegistrationNo = t.RegistrationNo,
                        TypeCode = t.TypeCode,
                        Brand = t.Brand
                    }).FirstOrDefault();
            return item;
        }

        public PartnerItemVm GetPartnerItem(string carrierCode)
        {
            var partner = 
                _context.Partners.Where(t => t.Code == carrierCode)
                    .Select(t =>
                    new PartnerItemVm
                    {
                        Id = t.Id,
                        Code = t.Code,
                        ShortName = t.ShortName,
                        FullName = t.FullName,
                        Address = t.Address
                    }).FirstOrDefault();
            return partner;
        }


        public PartnerItemsVm GetPartnerItemsVm(int page, int perPageNo, string filter)
        {
            var dto =
                _externalDataRepository.GetFilteredPagePartnerItems(page, perPageNo, filter);
            var vm = Mapper.Map<PartnerItemsVm>(dto);
            return vm;
        }

        public BudgetItemsVm GetBudgetItemsVm()
        {
            var dto = _externalDataRepository.GetBudgetItems();
            var vm = Mapper.Map<BudgetItemsVm>(dto);
            return vm;
        }

        public SupplyTransportTypeItemsVm GetSupplyTransportTypeItemsVm()
        {
            var dto = _externalDataRepository.GetSupplyTransportTypeItems();
            var vm = Mapper.Map<SupplyTransportTypeItemsVm>(dto);
            return vm;
        }

        public LabHumidityСlassifierItemsVm GetLabHumidityСlassifierItemsVm()
        {
            var dto = _externalDataRepository.GetLabHumidityСlassifierItems();
            var vm = Mapper.Map<LabHumidityСlassifierItemsVm>(dto);
            return vm;
        }

        public LabImpurityСlassifierItemsVm GetLabImpurityСlassifierItemsVm()
        {
            var dto = _externalDataRepository.GetLabImpurityСlassifierItems();
            var vm = Mapper.Map<LabImpurityСlassifierItemsVm>(dto);
            return vm;
        }

        public LabInfectionedСlassifierItemsVm GetLabInfectionedСlassifierItemsVm()
        {
            var dto = _externalDataRepository.GetLabInfectionedСlassifierItems();
            var vm = Mapper.Map<LabInfectionedСlassifierItemsVm>(dto);
            return vm;
        }

        public LabEffectiveClassifierItemsVm GetLabEffectiveСlassifierItemsVm()
        {
            return new LabEffectiveClassifierItemsVm
            {
                Items = new List<LabEffectiveClassifierItemVm>
                {
                    new LabEffectiveClassifierItemVm
                    {
                        Name = LabEffectiveClassifier.DryWet
                    },
                    new LabEffectiveClassifierItemVm
                    {
                        Name = LabEffectiveClassifier.WetDry
                    }
                }
            };
        }

        public BudgetItemsVm GetBudgetChildren(string parentId = "00000000-0000-0000-0000-000000000000")
        {
            var dto = _externalDataRepository.GetBudgetChildItem(parentId);
            var vm = Mapper.Map<BudgetItemsVm>(dto);
            return vm;
        }

        public ProductItemsVm GetProductChildren(string parentId = "00000000-0000-0000-0000-000000000000")
        {
            var dto = _externalDataRepository.GetProductChildItems(parentId);
            var vm = Mapper.Map<ProductItemsVm>(dto);
            return vm;
        }

        public EmployeeItemsVm GetEmployeeChildren(string parentId = "00000000-0000-0000-0000-000000000000")
        {
            var dto = _externalDataRepository.GetEmployeeChildItems(parentId);
            var vm = Mapper.Map<EmployeeItemsVm>(dto);
            return vm;
        }

        public PartnerItemsVm GetPartnerChildren(string parentId = "00000000-0000-0000-0000-000000000000")
        {
            var dto = _externalDataRepository.GetPartnerChildItems(parentId);
            var vm = Mapper.Map<PartnerItemsVm>(dto);
            return vm;
        }
    }
}