using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravitas.Model
{
    public class CentralLabOpData : BaseOpData
    {
        public  DateTime? SampleCheckInDateTime { get; set; }
        public DateTime? SampleCheckOutTime { get; set; }
        public string LaboratoryComment { get; set; }
        public string CollisionComment { get; set; }
    }
}
