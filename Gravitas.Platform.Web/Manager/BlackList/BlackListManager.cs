using System;
using System.Linq;
using Gravitas.DAL;
using Gravitas.DAL.Repository;
using Gravitas.Model;
using Gravitas.Model.DomainModel.BlackList.DAO;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager
{
    public class BlackListManager : IBlackListManager
    {
        private readonly IBlackListRepository _blackListRepository;
        private readonly IExternalDataRepository _externalDataRepository;

        public BlackListManager(IBlackListRepository blackListRepository,
            IExternalDataRepository externalDataRepository)
        {
            _blackListRepository = blackListRepository;
            _externalDataRepository = externalDataRepository;
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
                _blackListRepository.Add<DriversBlackListRecord, long>(new DriversBlackListRecord
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
                _blackListRepository.Add<TrailersBlackListRecord, long>(new TrailersBlackListRecord
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
                _blackListRepository.Add<TransportBlackListRecord, long>(new TransportBlackListRecord
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
        
        public void DeleteBlackListDriverRecord(long driverId)
        {
            var driver = _blackListRepository.GetEntity<DriversBlackListRecord, long>(driverId);
            _blackListRepository.Delete<DriversBlackListRecord,long>(driver);
        }

        public void DeleteBlackListTrailerRecord(long trailerId)
        {
            var trailer = _blackListRepository.GetEntity<TrailersBlackListRecord, long>(trailerId);
            _blackListRepository.Delete<TrailersBlackListRecord,long>(trailer);
        }

        public void DeleteBlackListPartnerRecord(string partnerId)
        {
            _blackListRepository.DeletePartner(partnerId);
        }

        public void DeleteBlackListTransportRecord(long transportId)
        {
            var transport = _blackListRepository.GetEntity<TransportBlackListRecord, long>(transportId);
            _blackListRepository.Delete<TransportBlackListRecord, long>(transport);
        }
    }
}