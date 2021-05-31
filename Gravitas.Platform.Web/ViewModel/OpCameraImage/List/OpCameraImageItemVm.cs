using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public class OpCameraImageItemVm
    {
        [DisplayName("ID")] public int Id { get; set; }

        [DisplayName("Дата\\Час")] public DateTime? DateTime { get; set; }
        public string ImagePath { get; set; }
        [DisplayName("Джерело")] public string SourceDeviceName { get; set; }
    }
}