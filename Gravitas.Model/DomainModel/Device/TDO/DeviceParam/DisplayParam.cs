namespace Gravitas.Model
{
    public class DisplayParam : BaseJsonConverter<DisplayParam>
    {
        public string IpAddress { get; set; }
        public int IpPort { get; set; }
        public int Timeout { get; set; }
    }
}
