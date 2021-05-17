namespace Gravitas.Infrastructure.Platform.Manager.Scale
{
    public class OnLoadScaleValidationDataModel
    {
        public bool IsOverLoad { get; set; }
        public bool IsUnderLoad { get; set; }
        public double WeightOnTruck { get; set; }
        public double WeightDifference { get; set; }
    }
}