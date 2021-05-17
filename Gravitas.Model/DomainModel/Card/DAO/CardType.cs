using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.Card.DAO
{
    [Table("CardType")]
    public class CardType : BaseEntity<int>
    {
        public CardType()
        {
            CardSet = new HashSet<Card>();
        }

        [Required] [StringLength(50)] 
        public string Name { get; set; }

        public virtual ICollection<Card> CardSet { get; set; }
    }
}