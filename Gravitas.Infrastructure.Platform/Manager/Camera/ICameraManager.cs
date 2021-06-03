using System.Collections.Generic;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Gravitas.Model.DomainModel.Node.TDO.Json;

namespace Gravitas.Infrastructure.Platform.Manager.Camera
{
    public interface ICameraManager
    {
        bool TakeSnapshot(string host, string user, string pass, string path);
        bool TakeSnapshot(CameraParam param, string filename);
        List<int> GetSnapshots(NodeConfig nodeConfig);
    }
}