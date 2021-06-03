using System;

namespace Gravitas.Infrastructure.Platform.ApiClient
{
    public interface IOneCApiService
    {
        void UpdateOneCData(Guid singleWindowOpDataId, bool closeDeliveryBill);
    }
}