using System;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.CollisionCoordination.Models {
    public class CollisionRequest {
        [Required(ErrorMessage="Guid is required")]
        public Guid Guid { get; set; }
        
        [Required(ErrorMessage="Accept is required")]
        public bool Accept { get; set; }       
    }
}