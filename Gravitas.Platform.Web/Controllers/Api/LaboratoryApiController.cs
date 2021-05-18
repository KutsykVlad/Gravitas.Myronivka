using System;
using System.Threading.Tasks;
using System.Web.Http;
using Gravitas.DAL;
using Gravitas.DAL.Repository.ExternalData;

namespace Gravitas.Platform.Web.Controllers.Api
{
    public class LaboratoryApiController : ApiController
    {
        private readonly IExternalDataRepository _externalDataRepository;
        public LaboratoryApiController(IExternalDataRepository externalDataRepository)
        {
            _externalDataRepository = externalDataRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetEmployeeData(string id)
        {
            try
            {
                var data = await Task.Run(() => _externalDataRepository.GetEmployeeDetail(id));
                return data != null ? (IHttpActionResult) Ok(data) : BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}