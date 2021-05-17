using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public class BaseEntityVm<T>
    {
        [DisplayName("ID")]
        public T Id { get; set; }
    }
}