namespace Gravitas.Model.DomainModel.OpDataEvent.DTO
{
    public class OpDataEventDto
    {
        public int PlatformNumber { get; set; }
        public string User { get; set; }
        public string Period { get; set; }
        public string TypeOfTransaction { get; set; }
        public string Cause { get; set; }
        public double? Weight { get; set; }
    }
}