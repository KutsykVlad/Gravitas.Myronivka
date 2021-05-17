using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Stock.DAO
{
    public class Stock : BaseEntity<string>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool IsFolder { get; set; }
        public string ParentId { get; set; }
        public string CustomerId { get; set; }
    }
}