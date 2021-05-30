using System;

namespace Gravitas.Infrastructure.Platform.Manager.OpVisa
{
    public interface IVisaValidationManager
    {
        bool ValidateEmployeeAccess(int nodeId, Guid employeeId);
    }
}