using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.Infrastructure.Platform.ApiClient.Devices;
using Gravitas.Infrastructure.Platform.Manager.Settings;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.Device
{
    public class DeviceWebManager : IDeviceWebManager
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly INodeRepository _nodeRepository;
        private readonly ISettings _settings;
        private readonly GravitasDbContext _context;

        public DeviceWebManager(
            INodeRepository nodeRepository,
            IDeviceRepository deviceRepository,
            ISettings settings, 
            GravitasDbContext context)
        {
            _nodeRepository = nodeRepository;
            _deviceRepository = deviceRepository;
            _settings = settings;
            _context = context;
        }

        public DeviceType? GetDeviceType(int deviceId)
        {
            return _context.Devices.First(x => x.Id == deviceId)?.TypeId;
        }

        public DeviceStateVms.WeighbridgeStateVm GetWeightbridgeStateVm(int nodeId)
        {
            var nodeDetail = _nodeRepository.GetNodeDto(nodeId);

            if (!nodeDetail.Config.Scale.TryGetValue(NodeData.Config.Scale.Scale1, out var scaleConfig)
                || !(DeviceSyncManager.GetDeviceState(scaleConfig.DeviceId) is ScaleState scaleState)
                || scaleState.InData == null)
                return null;

            if (!nodeDetail.Config.DI.TryGetValue(NodeData.Config.DI.PerimeterLeft, out var plConfig)
                || !(DeviceSyncManager.GetDeviceState(plConfig.DeviceId) is DigitalInState plState)
                || plState.InData == null)
                return null;

            if (!nodeDetail.Config.DI.TryGetValue(NodeData.Config.DI.PerimeterRight, out var prConfig)
                || !(DeviceSyncManager.GetDeviceState(prConfig.DeviceId) is DigitalInState prState)
                || prState.InData == null)
                return null;

            if (!nodeDetail.Config.DI.TryGetValue(NodeData.Config.DI.PerimeterTop, out var ptConfig)
                || !(DeviceSyncManager.GetDeviceState(ptConfig.DeviceId) is DigitalInState ptState)
                || ptState.InData == null)
                return null;

            if (!nodeDetail.Config.DI.TryGetValue(NodeData.Config.DI.PerimeterBottom, out var pbConfig)
                || !(DeviceSyncManager.GetDeviceState(pbConfig.DeviceId) is DigitalInState pbState)
                || pbState.InData == null)
                return null;

            if (DateTime.Now - scaleState.LastUpdate > TimeSpan.FromSeconds(scaleConfig.Timeout)) return null;

            var pTimeout = DateTime.Now - plState.LastUpdate > TimeSpan.FromSeconds(scaleConfig.Timeout)
                           || DateTime.Now - prState.LastUpdate > TimeSpan.FromSeconds(scaleConfig.Timeout)
                           || DateTime.Now - ptState.LastUpdate > TimeSpan.FromSeconds(scaleConfig.Timeout)
                           || DateTime.Now - pbState.LastUpdate > TimeSpan.FromSeconds(scaleConfig.Timeout);

            var result = new DeviceStateVms.WeighbridgeStateVm
            {
                ScaleIsError = scaleState.InData.IsScaleError,
                ScaleIsZero = scaleState.InData.IsZero,
                ScaleIsImmobile = scaleState.InData.IsImmobile,
                ScaleIsGross = scaleState.InData.IsGross,
                ScaleTime = scaleState.LastUpdate,
                ScaleValue = scaleState.InData.Value,
                PerimeterLeft = pTimeout || plState.InData.Value,
                PerimeterRight = pTimeout || prState.InData.Value,
                PerimeterTop = pTimeout || ptState.InData.Value,
                PerimeterBottom = pTimeout || pbState.InData.Value
            };

            return result;
        }

        public DeviceStateVms.LabFossStateVm GetLabFossStateVm(int deviceId)
        {
            if (!(_deviceRepository.GetDeviceState(deviceId) is LabFossState labFossState)
                || !_deviceRepository.IsDeviceStateValid(out _, labFossState))
                return null;

            var result = new DeviceStateVms.LabFossStateVm
            {
                AnalysisTime = labFossState.InData.AnalysisTime,
                ProductName = labFossState.InData.ProductName,
                HumidityValue = labFossState.InData.HumidityValue,
                ProteinValue = labFossState.InData.ProteinValue,
                OilValue = labFossState.InData.OilValue,
                FibreValue = labFossState.InData.FibreValue
            };

            return result;
        }

        public DeviceStateVms.LabBrukerStateVm GetLabBrukerStateVm(int deviceId)
        {
            if (!(_deviceRepository.GetDeviceState(deviceId) is LabBrukerState labBrukerState)
                || !_deviceRepository.IsDeviceStateValid(out _, labBrukerState))
                return null;

            var result = new DeviceStateVms.LabBrukerStateVm
            {
                SampleName = labBrukerState.InData.SampleName,
                SampleMass = labBrukerState.InData.SampleMass,
                Result1 = labBrukerState.InData.Result1,
                Result2 = labBrukerState.InData.Result2,
                AcquisitionDate = labBrukerState.InData.AcquisitionDate,
                Comment = labBrukerState.InData.Comment
            };

            return result;
        }

        public DeviceStateVms.LabAnalyserStateDialogVm GetLabAnalyserStateDialogVm(int deviceId, int nodeId)
        {
            var deviceState = _deviceRepository.GetDeviceState(deviceId);
            if (!_deviceRepository.IsDeviceStateValid(out _, deviceState))
                return null;

            switch (deviceState)
            {
                case LabFossState labFossState:
                    var labFossResult = new DeviceStateVms.LabAnalyserStateDialogVm
                    {
                        NodeId = nodeId,
                        IsActualData =
                            DateTime.Now - labFossState.InData.AnalysisTime < TimeSpan.FromMinutes(_settings.LabDataExpireMinutes),
                        DeviceId = deviceId,
                        DeviceName = _context.Devices.First(x => x.Id == deviceId)?.Name
                                     ?? "Невідомий пристрій",
                        AnalysisTime = labFossState.InData.AnalysisTime,
                        SampleName = labFossState.InData.ProductName,
                        ValueList = new List<DeviceStateVms.LabAnalyserValueVm>
                        {
                            new DeviceStateVms.LabAnalyserValueVm
                            {
                                Id = 1,
                                Name = "Вологість",
                                Value = labFossState.InData.HumidityValue,
                                TargetId = Model.DomainValue.ExternalData.LabDevResultType.SaveAsComment
                            },
                            new DeviceStateVms.LabAnalyserValueVm
                            {
                                Id = 1,
                                Name = "Протеїн",
                                Value = labFossState.InData.ProteinValue,
                                TargetId = Model.DomainValue.ExternalData.LabDevResultType.SaveAsComment
                            },
                            new DeviceStateVms.LabAnalyserValueVm
                            {
                                Id = 1,
                                Name = "Олійність",
                                Value = labFossState.InData.OilValue,
                                TargetId = Model.DomainValue.ExternalData.LabDevResultType.SaveAsComment
                            },
                            new DeviceStateVms.LabAnalyserValueVm
                            {
                                Id = 1,
                                Name = "Fibre",
                                Value = labFossState.InData.FibreValue,
                                TargetId = Model.DomainValue.ExternalData.LabDevResultType.SaveAsComment
                            }
                        }
                    };
                    return labFossResult;

                case LabBrukerState labBrukerState:
                    var labBrukerResult = new DeviceStateVms.LabAnalyserStateDialogVm
                    {
                        NodeId = nodeId,
                        IsActualData =
                            DateTime.Now - labBrukerState.InData.AcquisitionDate <
                            TimeSpan.FromMinutes(_settings.LabDataExpireMinutes),
                        DeviceId = deviceId,
                        DeviceName = _context.Devices.First(x => x.Id == deviceId)?.Name ?? "Невідомий пристрій",
                        AnalysisTime = labBrukerState.InData.AcquisitionDate,
                        SampleName = labBrukerState.InData.SampleName,
                        ValueList = new List<DeviceStateVms.LabAnalyserValueVm>
                        {
                            new DeviceStateVms.LabAnalyserValueVm
                            {
                                Id = 1,
                                Name = "Результат 1",
                                Value = labBrukerState.InData.Result1,
                                TargetId = Model.DomainValue.ExternalData.LabDevResultType.SaveAsComment
                            },
                            new DeviceStateVms.LabAnalyserValueVm
                            {
                                Id = 1,
                                Name = "Результат 2",
                                Value = labBrukerState.InData.Result2,
                                TargetId = Model.DomainValue.ExternalData.LabDevResultType.SaveAsComment
                            }
                        }
                    };
                    return labBrukerResult;

                case LabInfrascanState labInfrascanState:
                    var labInfrascanResult = new DeviceStateVms.LabAnalyserStateDialogVm
                    {
                        NodeId = nodeId,
                        IsActualData =
                            DateTime.Now - labInfrascanState.InData.AnalysisTime <
                            TimeSpan.FromMinutes(_settings.LabDataExpireMinutes),
                        DeviceId = deviceId,
                        DeviceName = _context.Devices.First(x => x.Id == deviceId)?.Name ?? "Невідомий пристрій",
                        AnalysisTime = labInfrascanState.InData.AnalysisTime,
                        ValueList = new List<DeviceStateVms.LabAnalyserValueVm>
                        {
                            new DeviceStateVms.LabAnalyserValueVm
                            {
                                Id = 1,
                                Name = "Білок",
                                Value = labInfrascanState.InData.ProteinValue,
                                TargetId = Model.DomainValue.ExternalData.LabDevResultType.SaveAsComment
                            },
                            new DeviceStateVms.LabAnalyserValueVm
                            {
                                Id = 1,
                                Name = "Жир",
                                Value = labInfrascanState.InData.FatValue,
                                TargetId = Model.DomainValue.ExternalData.LabDevResultType.SaveAsComment
                            },
                            new DeviceStateVms.LabAnalyserValueVm
                            {
                                Id = 1,
                                Name = "Волога",
                                Value = labInfrascanState.InData.HumidityValue,
                                TargetId = Model.DomainValue.ExternalData.LabDevResultType.SaveAsComment
                            },
                            new DeviceStateVms.LabAnalyserValueVm
                            {
                                Id = 1,
                                Name = "Клітковина",
                                Value = labInfrascanState.InData.CelluloseValue,
                                TargetId = Model.DomainValue.ExternalData.LabDevResultType.SaveAsComment
                            }
                        }
                    };
                    return labInfrascanResult;
            }

            return null;
        }

        public DeviceStateVms.LabInfroscanStateVm GetLabInfrascanStateVm(int deviceId)
        {
            if (!(_deviceRepository.GetDeviceState(deviceId) is LabInfrascanState labInfrascanState)
                || !_deviceRepository.IsDeviceStateValid(out _, labInfrascanState))
                return null;

            var result = new DeviceStateVms.LabInfroscanStateVm
            {
                ProteinValue = labInfrascanState.InData.ProteinValue,
                FatValue = labInfrascanState.InData.FatValue,
                HumidityValue = labInfrascanState.InData.HumidityValue,
                CelluloseValue = labInfrascanState.InData.CelluloseValue,
                Date = labInfrascanState.InData.AnalysisTime
            };

            return result;
        }
    }
}