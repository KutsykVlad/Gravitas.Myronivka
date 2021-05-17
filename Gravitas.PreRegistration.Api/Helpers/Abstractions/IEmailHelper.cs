using Gravitas.PreRegistration.Api.Models;

namespace Gravitas.PreRegistration.Api.Helpers.Abstractions
{
    public interface IEmailHelper
    {
        void SendEmail(CompanyInfo info);
    }
}
