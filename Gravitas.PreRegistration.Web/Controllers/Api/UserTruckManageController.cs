using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Web.Http;
using Gravitas.PreRegistration.Web.Models;
using Gravitas.PreRegistration.Web.Models.Registry;

namespace Gravitas.PreRegistration.Web.Controllers.Api
{
    public class UserTruckManageController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> PostEditedTrucks([FromBody]UserTruckRegistryVm vm)
        {
            try
            {
                await Task.Run(() =>
                {

                    var phones = vm.Items.Select(t => t.PhoneNo).ToList();

                    var deleteItems =
                        _context.RegisteredTrucks.Where(t => t.PartnerId == vm.PartnerId && !phones.Contains(t.PhoneNo));
                    _context.RegisteredTrucks.RemoveRange(deleteItems);
                    _context.SaveChanges();
                });

                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
    }
}
