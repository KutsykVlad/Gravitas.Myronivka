using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.Device._Base
{
    public class BaseEntityVm<T>
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }
    }
}