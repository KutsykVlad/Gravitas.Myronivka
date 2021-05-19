using System;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.BlackList;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.Model.DomainModel.BlackList.DAO;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.BlackList
{
    public class BlackListManager : IBlackListManager
    {
        private readonly IBlackListRepository _blackListRepository;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly GravitasDbContext _context;

        public BlackListManager(IBlackListRepository blackListRepository,
            IExternalDataRepository externalDataRepository,
            GravitasDbContext context)
        {
            _blackListRepository = blackListRepository;
            _externalDataRepository = externalDataRepository;
            _context = context;
        }

        public BlackListTableVm GetBlackListTable()
        {
            var blackListRegistry = _blackListRepository.GetBlackListDto();
            return new BlackListTableVm
            {
                Partners = new BlackListPartnersListVm
                {
                    Items = blackListRegistry.Partners
                        .Select(x => new BlackListPartnerRecordVm
                        {
                            Id = x .Partner.Id,
                            PartnerName = x .Partner.FullName,
                            Comment = x .Comment
                        })
                        .OrderBy(x => x.PartnerName)
                        .ToList(),
                    PartnerToAdd = new BlackListPartnerRecordToAddVm
                    {
                        Id = "",
                        Partners = _externalDataRepository.GetPartnerItems()
                    }
                },
                Drivers = new BlackListDriversListVm
                {
                    Items = blackListRegistry.Drivers
                        .Select(x => new BlackListDriverRecordVm
                        {
                            Id = x.Id,
                            Surname = x.Name,
                            Comment = x.Comment
                        })
                        .OrderBy(x => x.Surname)
                        .ToList(),
                    DriverToAdd = new BlackListDriverRecordVm
                    {
                        Surname = String.Empty
                    }
                },
                Trailers = new BlackListTrailersListVm
                {
                    Items = blackListRegistry.Trailers
                        .Select(record=> new BlackListTrailerRecordVm
                        {
                            Id = record.Id,
                            TrailerNumber = record.TrailerNo,
                            Comment = record.Comment
                        })
                        .OrderBy(x => x.TrailerNumber)
                        .ToList(),
                    TrailerToAdd = new BlackListTrailerRecordVm
                    {
                        TrailerNumber = string.Empty
                    }
                },
                Transport = new BlackListTransportListVm
                {
                    Items = blackListRegistry.Transport
                        .Select(record=> new BlackListTransportRecordVm
                        {
                            Id = record.Id,
                            TransportNumber = record.TransportNo,
                            Comment = record.Comment
                        })
                        .OrderBy(x => x.TransportNumber)
                        .ToList(),
                    TransportToAdd = new BlackListTransportRecordVm
                    {
                        TransportNumber = string.Empty
                    }
                }
            };
        }

        public (bool, string) AddPartnerRecord(BlackListPartnerRecordToAddVm partner)
        {
            try
            {
                _blackListRepository.AddPartner(new PartnersBlackListRecord
                {
                    Comment = partner.Comment,
                    PartnerId = partner.Id
                });
            }
            catch (Exception e)
            {
                return (false, $@"Виникла помилка при спробі внесення партнера у чорний список : {e}");
            }

            return (true, @"Партнера успішно внесено у чорний список");
        }

        public (bool, string) AddDriverRecord(BlackListDriverRecordVm driver)
        {
            try
            {
                _blackListRepository.Add<DriversBlackListRecord, int>(new DriversBlackListRecord
                {
                    Name = driver.Surname, 
                    Comment = driver.Comment
                });
            }
            catch (Exception e)
            {
                return (false, $@"Виникла помилка при спробі внесення водія у чорний список : {e}");
            }

            return (true, @"Водія успішно внесено у чорний список");
        }

        public (bool, string) AddTrailerRecord(BlackListTrailerRecordVm trailer)
        {
            try
            {
                _blackListRepository.Add<TrailersBlackListRecord, int>(new TrailersBlackListRecord
                {
                    TrailerNo = trailer.TrailerNumber, 
                    Comment = trailer.Comment
                });
            }
            catch (Exception e)
            {
                return (false, $@"Виникла помилка при спробі внесення трейлеру у чорний список : {e}");
            }

            return (true, @"Трейлер успішно внесено у чорний список");
            
        }

        public (bool, string) AddTransportRecord(BlackListTransportRecordVm trailer)
        {
            try
            {
                _blackListRepository.Add<TransportBlackListRecord, int>(new TransportBlackListRecord
                {
                    TransportNo = trailer.TransportNumber,
                    Comment = trailer.Comment
                });
            }
            catch (Exception e)
            {
                return (false, $@"Виникла помилка при спробі внесення авто у чорний список : {e}");
            }

            return (true, @"Авто успішно внесено у чорний список");
        }
        
        public void DeleteBlackListDriverRecord(int driverId)
        {
            var driver = _context.DriversBlackListRecords.First(x => x.Id ==driverId);
            _blackListRepository.Delete<DriversBlackListRecord, int>(driver);
        }

        public void DeleteBlackListTrailerRecord(int trailerId)
        {
            var trailer = _context.TrailersBlackListRecords.First(x => x.Id == trailerId);
            _blackListRepository.Delete<TrailersBlackListRecord, int>(trailer);
        }

        public void DeleteBlackListPartnerRecord(string partnerId)
        {
            _blackListRepository.DeletePartner(partnerId);
        }

        public void DeleteBlackListTransportRecord(int transportId)
        {
            var transport = _context.TransportBlackListRecords.First(x => x.Id == transportId);
            _blackListRepository.Delete<TransportBlackListRecord, int>(transport);
        }
    }
}