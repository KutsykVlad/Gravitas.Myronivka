using AutoMapper;

namespace Gravitas.Platform.Web.AutoMapper
{
    public partial class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            ConfigureExternalData();
            ConfigureNode();
            ConfigureOpData();
            ConfigureTicket();
        }
    }
}