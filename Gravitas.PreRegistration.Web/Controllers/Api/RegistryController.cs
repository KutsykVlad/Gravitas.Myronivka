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
    public class RegistryController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET api/<controller>

        [HttpGet]
        public async Task<IHttpActionResult> GetTruckRecords(string partnerId)
        {
            try
            {
                var list = _context.RegisteredTrucks.Where(t => t.PartnerId == partnerId).Select(item =>
                    new PreRegisterBaseItem()
                    {
                        TrailerNo = item.TrailerNo,
                        RouteId = item.RouteId,
                        PhoneNo = item.PhoneNo,
                        TruckNo = item.TruckNo
                    }).ToList();

                WebRequest request = WebRequest.Create(ConfigurationManager.AppSettings.Get("ApiPath") + "/api/QueueApi/ValidateQueueRecords");
                request.Method = "POST";
                Stream dataStream = request.GetRequestStream();
                byte[] byteArray = ObjectToByteArray(list);
                dataStream.Write(byteArray, 0, byteArray.Length);
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                byte[] responseBytes = new byte[255];
                Int32 bytes = responseStream.Read(responseBytes, 0, responseBytes.Length);
                var responseData = System.Text.Encoding.ASCII.GetString(responseBytes, 0, bytes);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [Serializable]
        public class PreRegisterItem: PreRegisterBaseItem
        {
            public string PartnerName { get; set; }
            public string PartnerId { get; set; }
            public DateTime EntranceTime { get; set; }
        }
        [Serializable]
        public class PreRegisterBaseItem
        {
            public string TruckNo { get; set; }
            public string TrailerNo { get; set; }
            public string PhoneNo { get; set; }
            public long RouteId { get; set; }
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddTruck([FromBody]PreRegisterItem truck)
        {
            try
            {
                await Task.Run(() =>
                {
                   
                    _context.RegisteredTrucks.Add(new RegisteredTruck
                    {
                        EntranceTime = truck.EntranceTime,
                        PartnerId = truck.PartnerId,
                        PartnerName = truck.PartnerName,
                        PhoneNo = truck.PhoneNo,
                        RouteId = truck.RouteId,
                        TrailerNo = truck.TrailerNo,
                        TruckNo = truck.TruckNo
                    });
                    _context.SaveChanges();
                });

                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }


        

        [HttpGet]
        public async Task<IHttpActionResult> DeleteTruckRecord(string truckId)
        {
            try
            {
                await Task.Run(() =>
                {
                    //ApplicationDbContext context = new ApplicationDbContext();
                    //context.RegisteredTrucks.Add(new RegisteredTruck
                    //{
                    //    EntranceTime = truck.EntranceTime,
                    //    PartnerId = truck.PartnerId,
                    //    PartnerName = truck.PartnerName,
                    //    PhoneNo = truck.PhoneNo,
                    //    RouteId = truck.RouteId,
                    //    TrailerNo = truck.TrailerNo,
                    //    TruckNo = truck.TruckNo
                    //});
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