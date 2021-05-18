using System.Data.Entity.Migrations;
using Gravitas.DAL.DbContext;
using Gravitas.Model;

namespace Gravitas.DAL.TestData
{
    public static partial class TestData
    {
        public static void Deploy(GravitasDbContext context)
        {
//            ExternalData(context);
//			Card(context);

            // DeviceParam(context);
            //DeviceState(context);
            //Device(context);

//            CardAssignment(context);
//            QueuePatternItems(context);
//            MixedFeedDevices(context);

            context.SaveChanges();
        }
    }
}