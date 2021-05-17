using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.Model {

	[Table("CardType")]
	public partial class CardType : BaseEntity<long> {

		public CardType() {
			CardSet = new HashSet<Card>();
		}
		
		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public virtual ICollection<Card> CardSet { get; set; }
	}
}
