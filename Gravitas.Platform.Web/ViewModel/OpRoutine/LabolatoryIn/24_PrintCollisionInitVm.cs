using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class LaboratoryInVms
    {
        public class PrintCollisionInitVm
        {
            public int NodeId { get; set; }
            public int TicketId { get; set; }

            [Required]
            [DisplayName("Електронна адреса")]
            public string Email1 { get; set; }

            [Required]
            [DisplayName("Контактний номер")]
            public string Phone1 { get; set; }

            public string Email2 { get; set; }
            public string Phone2 { get; set; }
            public string Email3 { get; set; }
            public string Phone3 { get; set; }

            public string Comment { get; set; }

            [DisplayName("Доступні менеджери")] 
            public string Manager1 { get; set; }
            public string Manager2 { get; set; }
            public string Manager3 { get; set; }
            public Dictionary<Guid, Model.DomainModel.ExternalData.Employee.DAO.Employee> ManagerList = new Dictionary<Guid, Model.DomainModel.ExternalData.Employee.DAO.Employee>();
        }
    }
}