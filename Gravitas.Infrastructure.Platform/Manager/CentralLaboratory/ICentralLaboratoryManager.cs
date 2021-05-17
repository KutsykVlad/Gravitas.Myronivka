using System.Collections.Generic;
using Gravitas.Model.DomainValue;

namespace Gravitas.Infrastructure.Platform.Manager.CentralLaboratory
{
    public interface ICentralLaboratoryManager
    {
        Dictionary<CentralLabState, string> LabStateName { get; }
        Dictionary<CentralLabState, int> LabStateOrder { get; }
        Dictionary<CentralLabState, string> LabStateClassStyle { get; }
        CentralLabState GetTicketStateInCentralLab(long? ticketId);
    }
}