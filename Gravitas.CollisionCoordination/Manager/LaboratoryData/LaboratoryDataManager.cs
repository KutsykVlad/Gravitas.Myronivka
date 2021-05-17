using System;
using System.Linq;
using Gravitas.CollisionCoordination.Models;
using Gravitas.DAL;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Model;
using NLog;

namespace Gravitas.CollisionCoordination.Manager.LaboratoryData
{
    public class LaboratoryDataManager : ILaboratoryDataManager
    {
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly IOpDataRepository _opDataRepository;
        private readonly GravitasDbContext _context;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public LaboratoryDataManager(IOpDataRepository opDataRepository, IExternalDataRepository externalDataRepository, GravitasDbContext context)
        {
            _opDataRepository = opDataRepository;
            _externalDataRepository = externalDataRepository;
            _context = context;
        }

        public LaboratoryDataVm CollectData(long ticketId, string approvedBy)
        {
            var data = new LaboratoryDataVm();

            var opData = _context.LabFacelessOpDatas.Where(
                                              item => item.TicketId == ticketId
                                                      && item.StateId == Dom.OpDataState.Collision)
                                          .OrderByDescending(e => e.CheckInDateTime)
                                          .FirstOrDefault();
            if (opData == null)
            {
                _logger.Error($"LaboratoryDataManager: no such OpData in TicketId = {ticketId}");
                return data;
            }

            data.CheckOutDateTime = opData.CheckOutDateTime;
            data.LaboratoryComment = opData.Comment;
            data.ImpurityValue = opData.ImpurityValue;
            data.HumidityValue = opData.HumidityValue;
            data.IsInfectionedClassId = opData.InfectionedClassId;
            data.EffectiveValue = opData.EffectiveValue;
            data.AnalysisReleaseUserName =
                opData.OpVisaSet.OrderByDescending(e => e.DateTime).FirstOrDefault()?.Employee
                                 ?.ShortName ??
                string.Empty;

            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(opData.TicketId);
            if (singleWindowOpData != null)
            {
                data.ProductName = _externalDataRepository.GetProductDetail(singleWindowOpData.ProductId)?.ShortName ??
                                   string.Empty;

                data.SenderName = !string.IsNullOrWhiteSpace(singleWindowOpData.ReceiverId)
                    ? _externalDataRepository.GetStockDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? _externalDataRepository.GetSubdivisionDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? _externalDataRepository.GetPartnerDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? singleWindowOpData.CustomPartnerName
                    : string.Empty;
                
                if (singleWindowOpData.IsThirdPartyCarrier)
                {
                    data.TransportNo = singleWindowOpData.HiredTransportNumber;
                    data.TrailerNo = singleWindowOpData.HiredTrailerNumber;
                }
                else
                {
                    data.TransportNo =
                        _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId)?.RegistrationNo ??
                        string.Empty;
                    data.TrailerNo =
                        _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId)?.RegistrationNo ??
                        string.Empty;
                }
            }

            data.LaboratoryEmail = _externalDataRepository.GetEntity<ExternalData.Employee, string>(
                opData.OpVisaSet.OrderByDescending(e => e.DateTime).FirstOrDefault()?.Employee?.Id)?.Email;
            
            data.SingleWindowComment = singleWindowOpData?.Comments;
            data.LaboratoryComment = opData.CollisionComment;
            
            data.AutoGeneratedComment = opData.Comment;

            data.ApproveLink =
                $"{GlobalConfigurationManager.CollisionsConfirmHost}/api/collision/confirm?opDataId={opData.Id}&approved=true&approvedBy={approvedBy}";

            data.DisapproveLink =
                $"{GlobalConfigurationManager.CollisionsConfirmHost}/api/collision/confirm?opDataId={opData.Id}&approved=false&approvedBy={approvedBy}";

            return data;
        }

        public LaboratoryDataVm CollectCentralLaboratoryData(long ticketId, string approvedBy)
        {
            var data = new LaboratoryDataVm();

            var opData = _context.CentralLabOpDatas.Where(
                                              item => item.TicketId == ticketId
                                                      && item.StateId == Dom.OpDataState.Collision)
                                          .OrderByDescending(e => e.CheckInDateTime)
                                          .FirstOrDefault();
            if (opData == null)
            {
                _logger.Error($"LaboratoryDataManager: no such OpData in TicketId = {ticketId}");
                return data;
            }

            data.CheckOutDateTime = opData.SampleCheckOutTime;
            data.AnalysisReleaseUserName =
                opData.OpVisaSet.Where(x => x.OpRoutineStateId == Dom.OpRoutine.CentralLaboratoryProcess.State.AddSampleVisa)
                      .OrderByDescending(x => x.DateTime)
                      .FirstOrDefault()?
                      .Employee?.ShortName ?? string.Empty;
            data.LaboratoryWorker = opData.OpVisaSet.Where(x => x.OpRoutineStateId == Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInitVisa)
                                          .OrderByDescending(x => x.DateTime)
                                          .FirstOrDefault()?
                                          .Employee?.ShortName ?? string.Empty;

            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(opData.TicketId);
            if (singleWindowOpData != null)
            {
                data.ProductName = _externalDataRepository.GetProductDetail(singleWindowOpData.ProductId)?.ShortName ??
                                   string.Empty;

                data.SenderName = !string.IsNullOrWhiteSpace(singleWindowOpData.ReceiverId)
                    ? _externalDataRepository.GetStockDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? _externalDataRepository.GetSubdivisionDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? _externalDataRepository.GetPartnerDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? singleWindowOpData.CustomPartnerName
                    : string.Empty;

                if (singleWindowOpData.IsThirdPartyCarrier)
                {
                    data.TransportNo = singleWindowOpData.HiredTransportNumber;
                    data.TrailerNo = singleWindowOpData.HiredTrailerNumber;
                }
                else
                {
                    data.TransportNo =
                        _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId)?.RegistrationNo ??
                        string.Empty;
                    data.TrailerNo =
                        _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId)?.RegistrationNo ??
                        string.Empty;
                }
            }

            data.LaboratoryEmail = _externalDataRepository.GetEntity<ExternalData.Employee, string>(
                opData.OpVisaSet.OrderByDescending(e => e.DateTime).FirstOrDefault()?.Employee?.Id)?.Email;

            data.LaboratoryComment = $"Коментар майстра: {opData.CollisionComment}, Коментар лаборанта: {opData.LaboratoryComment}";

            data.ApproveLink =
                $"{GlobalConfigurationManager.CollisionsConfirmHost}/api/collision/confirm?opDataId={opData.Id}&approved=true&approvedBy={approvedBy}";

            data.DisapproveLink =
                $"{GlobalConfigurationManager.CollisionsConfirmHost}/api/collision/confirm?opDataId={opData.Id}&approved=false&approvedBy={approvedBy}";

//            data.LabolatoryFilePath = _externalDataRepository
//                .GetQuery<TicketFile, long>(file => file.TicketId == ticketId && file.TypeId == Dom.TicketFile.Type.LabCertificate).LastOrDefault()?.FilePath;

            return data;
        }
    }
}