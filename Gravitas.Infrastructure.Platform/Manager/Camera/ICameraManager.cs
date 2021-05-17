using System.Collections.Generic;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.Dto;

namespace Gravitas.Infrastructure.Platform.Manager
{
    public interface ICameraManager
    {
        bool TakeSnapshot(string host, string user, string pass, string path);
        bool TakeSnapshot(CameraParam param, string filename);

        List<long> GetSnapshots(NodeConfig nodeConfig);
    }
}