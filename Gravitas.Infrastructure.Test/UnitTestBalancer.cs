using System;
using System.Collections.Generic;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Model;
using NUnit.Framework;

namespace Gravitas.Infrastructure.Test
{
    [TestFixture]
    public class UnitTestBalancer
    {
        [Test]
        public void TestMethodBalancer()
        {
            var list = new List<Node>(){new Node(){Id = 1}, new Node() { Id = 2 }, new Node() { Id = 3 }};
            var q = new QueueLoadBalancer(list);
            var r = new RouteInfo(){TicketContainerId = 1, PathNodes = {new List<long>(){1}, new List<long>() { 2 } } };
            q.AddRouteLoad(r);

            Assert.IsTrue(q.NodesLoad[1].RoutesLoad[0].TicketContainerId == 1);
            Assert.IsTrue(q.NodesLoad[1].RoutesLoad[0].ArrivalProbability == 1);
            Assert.IsTrue(q.NodesLoad[1].RoutesLoad[0].NodesBeforeArrival == 0);
            
        }
    }
}
