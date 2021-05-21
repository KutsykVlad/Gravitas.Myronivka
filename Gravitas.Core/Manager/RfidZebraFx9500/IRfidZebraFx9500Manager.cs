using System.Collections.Generic;

namespace Gravitas.Core.Manager.RfidZebraFx9500
{
    public interface IRfidZebraFx9500Manager
    {
        List<string> GetCard(int deviceId);
    }
}