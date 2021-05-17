namespace Gravitas.Model
{
    public class DriversBlackListRecord : BaseEntity<long>
    {
        public string Name { get; set; }
        
        public string Comment { get; set; }
    }
}
