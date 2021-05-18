namespace Gravitas.Infrastructure.Platform.Manager.ReportTool
{
    public interface IReportTool
    {
        string ReplaceTokens(string template, object vm);
    }
}