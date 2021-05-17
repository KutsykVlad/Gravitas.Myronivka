namespace Gravitas.PreRegistration.Api.Models
{
    public class BarChartInfo
    {
        public string ProductTitle { get; set; }
        public string[] Labels { get; set; }
        public int[,] Data { get; set; }
        public int DataCount { get; set; }
    }
}