using System;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.Manager.Scale;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using NLog;

namespace Gravitas.Core.DeviceManager
{
    public class ScaleDataValidator
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly double? _incomeDocGrossValue;
        private readonly double _scaleValue;
        private readonly ScaleOpDataType _scaleOpType;
        private readonly ScaleOpData _previousScale;
        private readonly NodeDetails _nodeDetails;
        private readonly bool _isRejected;
        private readonly ScaleValidationResult _result = new ScaleValidationResult();
        public ScaleDataValidator(NodeDetails nodeDetails,
            double scaleValue, 
            ScaleOpDataType scaleOpType,
            ScaleOpData previousScale,
            double? incomeDocGrossValue, 
            bool isRejected)
        {
            _scaleValue = scaleValue;
            _isRejected = isRejected;
            _incomeDocGrossValue = incomeDocGrossValue;
            _scaleOpType = scaleOpType;
            _previousScale = previousScale;
            _nodeDetails = nodeDetails;
        }

        public ScaleValidationResult Validate()
        {
            CheckPerimetr();
            CheckOverloading();
            CheckGrossDifference();
            CheckTareDifference();
            CheckTrailerAvailability();
            CheckRejected();

            return _result;
        }

        private void CheckRejected()
        {
            double valueDifference = 0;
            if (_isRejected)
            {
                valueDifference = Math.Abs(_scaleValue - (_previousScale?.TruckWeightValue ?? 0 + _previousScale?.TrailerWeightValue ?? 0));
                _result.IsRejectedScaleValueValid = valueDifference < 40;
            }

            if (!_result.IsRejectedScaleValueValid)
            {
                _result.ValidationMessage += $"Вага змінилася на {valueDifference} кг. ";
            }
            _logger.Trace($"ScaleDataValidator: CheckRejected: valueDifference = {valueDifference}, isRejectedScaleValueValid = {_result.IsRejectedScaleValueValid}, _scaleValue = {_scaleValue}, _isRejected = {_isRejected}");
        }

        private void CheckTrailerAvailability()
        {
            _result.IsProcessWithoutTrailer = _previousScale == null || !_previousScale.TrailerIsAvailable;
            
            if (!_result.IsProcessWithoutTrailer)
            {
                _result.ValidationMessage += $"Зважування з причепом. ";
            }
            
            _logger.Trace($"ScaleDataValidator: CheckTrailerAvailability: isProcessWithoutTrailer = {_result.IsProcessWithoutTrailer}");
        }

        private void CheckTareDifference()
        {
            double tareDifference = 0;
            if (_scaleOpType == ScaleOpDataType.Tare && _previousScale?.TypeId == ScaleOpDataType.Tare)
            {
                tareDifference = Math.Abs(_scaleValue - (_previousScale.TruckWeightValue ?? 0 + _previousScale.TrailerWeightValue ?? 0));
                _result.IsTareValueValid = tareDifference > 40;
            }

            if (!_result.IsTareValueValid)
            {
                _result.ValidationMessage += $"Тара змінилася на {tareDifference} кг. ";
            }
            
            _logger.Trace($"ScaleDataValidator: CheckTareDifference: tareDifference = {tareDifference}, isTareValueValid = {_result.IsTareValueValid}, _scaleValue = {_scaleValue}, _scaleOpType = {_scaleOpType}, _previousScale?.TypeId = {_previousScale?.TypeId}");
        }

        private void CheckGrossDifference()
        {
            if (_incomeDocGrossValue > 0 && _scaleOpType == ScaleOpDataType.Gross)
            {
                _result.IsGrossValueValid = Math.Abs(_scaleValue - (_incomeDocGrossValue ?? 0)) < GlobalConfigurationManager.MaximumGrossWeightDifference;
            }
            if (!_result.IsGrossValueValid)
            {
                _result.ValidationMessage += $"Відхилення від брутто по документам більше ніж {GlobalConfigurationManager.MaximumGrossWeightDifference} кг. ";
            }
            
            _logger.Trace($"ScaleDataValidator: CheckGrossDifference: _incomeDocGrossValue = {_incomeDocGrossValue}, _scaleOpType = {_scaleOpType}, isGrossValueValid = {_result.IsGrossValueValid}");
        }

        private void CheckOverloading()
        {
            _result.IsNoOverload = _scaleValue <= GlobalConfigurationManager.MaximumScaleWeight;
            if (!_result.IsNoOverload)
            {
                _result.ValidationMessage += @"Перевищення максимальної ваги. ";
            }
            
            _logger.Trace($"ScaleDataValidator: CheckOverloading: maximumScaleWeight = {GlobalConfigurationManager.MaximumScaleWeight}, isNoOverload = {_result.IsNoOverload}");
        }

        private void CheckPerimetr()
        {
            _result.IsPerimetrValid = Program.GetDeviceState(_nodeDetails.Config.DI[NodeData.Config.DI.PerimeterLeft].DeviceId) is DigitalInState plState
                && plState.InData?.Value == true
                && Program.GetDeviceState(_nodeDetails.Config.DI[NodeData.Config.DI.PerimeterRight].DeviceId) is DigitalInState prState
                && prState.InData?.Value == true
                && Program.GetDeviceState(_nodeDetails.Config.DI[NodeData.Config.DI.PerimeterTop].DeviceId) is DigitalInState ptState
                && ptState.InData?.Value == true
                && Program.GetDeviceState(_nodeDetails.Config.DI[NodeData.Config.DI.PerimeterBottom].DeviceId) is DigitalInState pbState
                && pbState.InData?.Value == true;

            if (!_result.IsPerimetrValid)
            {
                _result.ValidationMessage += @"Порушення меж периметру. ";
            }
            
            _logger.Trace($"ScaleDataValidator: CheckOverloading: isPerimetrValid = {_result.IsPerimetrValid}");
        }
    }
}