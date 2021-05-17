using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.Dto 
{
	public static partial class ExternalData
	{
		public class ProductItem : BaseEntity<string> 
		{
			public string Code { get; set; }
			public string ShortName { get; set; }
			public string FullName { get; set; }
			public bool IsFolder { get; set; }
		}
	}
}