namespace Gravitas.Platform.Web.Manager.Report
{
    public interface IReportTool
    {
        string ReplaceTokens(string template, object vm);
    }
}