using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.Infrastructure.Platform.Manager.Scale
{
    public interface IScaleManager
    {
        OnLoadScaleValidationDataModel GetLoadWeightValidationData(int ticketId);
        bool IsTareMoreGross(Model.DomainModel.Node.TDO.Detail.NodeDetails nodeDetailsDto, bool isTruckWeighting, ScaleOpData scaleOpData);
        bool IsScaleStateOk(ScaleState scaleState, int nodeId);
        double? GetPartLoadUnloadValue(int ticketId);
        string IsWeightResultsValid(OnLoadScaleValidationDataModel scaleValidationDataModel, int ticketId);
    }
}