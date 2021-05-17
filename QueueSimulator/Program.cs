using Gravitas.Infrastructure.Platform.Manager.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMock;
using Gravitas.DAL;
using Gravitas.DAL.Repository.Queue;
using Gravitas.DAL.UnitOfWork;
using Gravitas.Infrastructure.Platform.ApiClient.Messages;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.Display;
using Gravitas.Model;


namespace QueueSimulator
{
    class Program
    {

        public static int ticketContainer1 = 354;
        public static int ticketContainer2 = 71;
        public static int ticketContainer3 = 71;

        public static int ticket1 = 86;
        public static int ticket2 = 84;
        public static int ticket3 = 85;

        //Set additional properties
        private static void PutAdditionalProperties(ref SmsMessageData data, Dictionary<string, object> parameters)
        {
            if (parameters == null) return;

            var t = data.GetType();
            foreach (var propertyName in parameters.Keys)
            {

                Console.WriteLine($"Property: {propertyName}");

                var propertyInfo = t.GetProperty(propertyName);
                if (propertyInfo == null) throw new Exception($"Property not found {propertyName}");

                propertyInfo.SetValue(data, parameters[propertyName], null);
            }

        }

        static void Main(string[] args)
        {
            SmsMessageData s = new SmsMessageData();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("EntranceTime", "ooooooo");
            PutAdditionalProperties(ref s, parameters);
            // TestAnyRegistered();
            //TestAllowance();
            TestTime();
        }

        static void TestTime()
        {
            var uw = new UnitOfWork<GravitasDbContext>(new GravitasDbContext());
            var tr = new TicketRepository(uw);
            var manager = new QueueManager(new NodeRepository(uw), new MockConnectManager(),
                new OpDataRepository(uw), new RoutesRepository(uw),
                tr, new QueueSettingsRepository(uw), new TrafficRepository(uw), new ExternalDataRepository(uw),
               
                new QueueRegisterRepository(uw)
            );

            manager.OnRouteAssigned(ticketContainer1);
//            var s = manager.GetPredictionEntranceTime(Guid.NewGuid());
        }


        static void TestExternalQueuepattern()
        {
            var uw = new UnitOfWork<GravitasDbContext>(new GravitasDbContext());
            var tr = new TicketRepository(uw);
            var manager = new QueueManager(new NodeRepository(uw), new MockConnectManager(),
                new OpDataRepository(uw), new RoutesRepository(uw),
                tr, new QueueSettingsRepository(uw), new TrafficRepository(uw), new ExternalDataRepository(uw),
              
                new QueueRegisterRepository(uw)
            );

            manager.OnRouteAssigned(ticketContainer1);

            //  manager.OnNodeArrival(ticketContainer1, 301);
            manager.OnNodeArrival(ticketContainer2, 301);
            manager.OnNodeArrival(ticketContainer2, 502);

            

        }

        static void TestRouteChange()
        {
            var uw = new UnitOfWork<GravitasDbContext>(new GravitasDbContext());
            var tr = new TicketRepository(uw);
            var manager = new QueueManager(new NodeRepository(uw), new MockConnectManager(),
                new OpDataRepository(uw), new RoutesRepository(uw),
                tr, new QueueSettingsRepository(uw), new TrafficRepository(uw), new ExternalDataRepository(uw),
                 new QueueRegisterRepository(uw),
                new MockProductWeightBridgeRepository());

            manager.OnRouteAssigned(ticketContainer1);

            //  manager.OnNodeArrival(ticketContainer1, 301);
            manager.OnNodeArrival(ticketContainer1, 503);
            manager.OnNodeArrival(ticketContainer1, 703);
            manager.OnNodeArrival(ticketContainer1, 8);
            manager.OnNodeArrival(ticketContainer1, 801);
            manager.OnNodeArrival(ticketContainer1, 701);

            var ticket = tr.GetFirstOrDefault<Ticket, long>(t => t.ContainerId == ticketContainer1);
            ticket.RouteTemplateId = 2;
            tr.Update<Ticket, long>(ticket);

            manager.OnRouteAssigned(ticketContainer1);
        }

        static void TestAnyRegistered()
        {

            var uw = new UnitOfWork<GravitasDbContext>(new GravitasDbContext());
            var manager = new QueueManager(new NodeRepository(uw), new MockConnectManager(),
                new OpDataRepository(uw), new RoutesRepository(uw),
                new TicketRepository(uw), new QueueSettingsRepository(uw), new TrafficRepository(uw),
                new ExternalDataRepository(uw),
                new QueueRegisterRepository(uw),
                new MockProductWeightBridgeRepository());

            manager.OnRouteAssigned(ticketContainer1);


            manager.OnNodeArrival(ticketContainer1, 301);
            manager.OnNodeArrival(ticketContainer1, 503);
            manager.OnNodeArrival(ticketContainer1, 703);
            manager.OnNodeArrival(ticketContainer1, 8);
            manager.OnNodeArrival(ticketContainer1, 801);
            manager.OnNodeArrival(ticketContainer1, 701);
            manager.OnNodeArrival(ticketContainer1, 302);
        }

        /// <summary>
        /// Пробує заїхати без черги
        /// </summary>
        static void TestAllowance()
        {
            
            var manager = CreatManager();

            manager.OnRouteAssigned(ticketContainer1);
            manager.OnRouteAssigned(ticketContainer2);

            Console.WriteLine($"Першому вїзд дозволено {manager.IsAllowedEnterTerritory(ticket1)}");
            Console.WriteLine($"Другому вїзд дозволено {manager.IsAllowedEnterTerritory(ticket2)}");

            manager.OnNodeArrival(ticket1, 703);
            Console.WriteLine($"Другому вїзд дозволено {manager.IsAllowedEnterTerritory(ticket2)}");
            manager.OnNodeArrival(ticket1, 8);
            Console.WriteLine($"Другому вїзд дозволено {manager.IsAllowedEnterTerritory(ticket2)}");
        }


        
        static void TestPreregistration()
        {
            var manager = CreatManager();
//            var t = manager.GetPredictionEntranceTime(Guid.NewGuid());
            manager.OnRouteAssigned(ticketContainer1);
        }

        static IQueueManager CreatManager()
        {
            var uw = new UnitOfWork<GravitasDbContext>(new GravitasDbContext());
            return new QueueManager(new NodeRepository(uw), new MockConnectManager(),
                new OpDataRepository(uw), new RoutesRepository(uw),
                new TicketRepository(uw), new QueueSettingsRepository(uw), new TrafficRepository(uw),
                new ExternalDataRepository(uw),
                new QueueRegisterRepository(uw),
                new MockProductWeightBridgeRepository());

            
        }
    }
}