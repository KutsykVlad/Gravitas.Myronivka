using System;
using System.ComponentModel;
using Gravitas.Model;

namespace Gravitas.Platform.Web.ViewModel
{
    public class SiloDetailVm
    {
        public int Id { get; set; }
			
        [DisplayName("Активний")]
        public bool IsActive { get; set; }
			
        [DisplayName("Проїзд")]
        public int Drive { get; set; }
			
        [DisplayName("Черга")]
        public int LoadQueue { get; set; }
			
        [DisplayName("Кількість")]
        public float SiloWeight { get; set; }
			
        [DisplayName("Пусте")]
        public float SiloEmpty { get; set; }
			
        [DisplayName("Заповнене")]
        public float SiloFull { get; set; }
			
        [DisplayName("Продукція")]
        public Guid? ProductId { get; set; }
	    public string ProductName { get; set; }
			
        [DisplayName("Специфікація")]
        public string Specification { get; set; }
    }
}