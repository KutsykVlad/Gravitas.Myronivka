using System.IO;
using System.Text;
using Codaxy.WkHtmlToPdf;

namespace Gravitas.Platform.Web.Manager.Report
{
    public class ReportWebManager : IReportWebManager
    {
        private readonly IReportTool _reportTool;

        public ReportWebManager(IReportTool reportTool)
        {
            _reportTool = reportTool;
        }

        public MemoryStream GenerateReportById(object vm, string templateUri, string orientation = null, string pageSize = null)
        {
            var encoding = Encoding.GetEncoding(1251);
            var template = File.ReadAllText(templateUri, encoding);

            template = _reportTool.ReplaceTokens(template, vm);

            return GenerateFile(template, encoding, orientation, pageSize);
        }

        private static MemoryStream GenerateFile(string template, Encoding encoding, string orientation, string pageSize)
        {
            var mStream = new MemoryStream();
            var doc = new PdfDocument
            {
                Html = template
            };

            PdfConvert.ConvertHtmlToPdf(doc, new PdfOutput
            {
                OutputStream = mStream
            }, encoding, orientation, pageSize);

            return mStream;
        }
    }
}