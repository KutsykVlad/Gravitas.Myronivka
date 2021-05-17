using Microsoft.AspNetCore.Mvc;

namespace JobScheduler.Controllers
{
  public class PingController : Controller
  {
    [HttpGet]
    [Route("/sceduler/refresh")]
    public ActionResult Ping()
    {
      return Ok();
    } 
  }
}