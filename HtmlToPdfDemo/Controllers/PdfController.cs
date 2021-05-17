using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using IronPdf;

namespace HtmlToPdfDemo.Controllers
{   
  [Route("pdf")]
  public class PdfController : ApiController
  {
    [HttpGet][Route("pdf/createpdf", Name = "CreatePdf")]
    public HttpResponseMessage CreatePdf()
    {
      var renderer = new HtmlToPdf();
      var pdf = renderer.RenderUrlAsPdf(Request.RequestUri.ToString().ToLower().Replace("pdf/createpdf", "PrintForm"));
      var result = new HttpResponseMessage(HttpStatusCode.OK)
      {
        Content = new ByteArrayContent(pdf.BinaryData)
      };

      result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
      {
        FileName = "HtmlToPDF_time.pdf"
      };
      result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

      return result;
    }
  }
}
