using System.IO;

namespace Gravitas.Platform.Web.Manager.Report
{
    public interface IReportWebManager
    {
        MemoryStream GenerateReportById(object vm, string templateUri, string orientation = null, string pageSize = null);
    }
}
