using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class MixedFeedManageVms
    {
        public class EditVm
        {
            [DisplayName("Силос")] public int Id { get; set; }

            [DisplayName("Активний")] public bool IsActive { get; set; }

            [DisplayName("Проїзд")] public string Drive { get; set; }

            [DisplayName("Черга завантаження")] public int LoadQueue { get; set; }

            [DisplayName("Кількість, т.")] public float SiloWeight { get; set; }

            [DisplayName("Пусте, м.")] public float SiloEmpty { get; set; }

            [DisplayName("Заповнене, м.")] public float SiloFull { get; set; }

            [DisplayName("Продукція")] public string ProductName { get; set; }
            public Guid? ProductId { get; set; }

            [DisplayName("Специфікація")] public string Specification { get; set; }
        }
    }
}