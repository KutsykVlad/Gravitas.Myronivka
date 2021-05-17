using Gravitas.Model;
using Gravitas.Model.Dto;
using Node = Gravitas.Model.Dto.Node;

namespace Gravitas.Infrastructure.Platform.Manager.Scale
{
    public interface IScaleManager
    {
        OnLoadScaleValidationDataModel GetLoadWeightValidationData(long ticketId);
        bool IsTareMoreGross(Node nodeDto, bool isTruckWeighting, ScaleOpData scaleOpData);
        bool IsScaleStateOk(ScaleState scaleState, long nodeId);
        double? GetPartLoadUnloadValue(long ticketId);
        string IsWeightResultsValid(OnLoadScaleValidationDataModel scaleValidationDataModel, long ticketId);
    }
}