﻿using System.Data.Entity.Migrations;
using System.Linq;
using Gravitas.Model;

namespace Gravitas.DAL.TestData
{
    public static partial class TestData
    {
        public static void DeviceState(GravitasDbContext context)
        {
            foreach (var id in context.Set<DeviceParam>().Select(e => e.Id).ToList())
                context.Set<DeviceState>().AddOrUpdate(new DeviceState
                {
                    Id = id,
                    InData = null,
                    OutData = null,
                    ErrorCode = Dom.Device.Status.ErrorCode.NA
                });
            context.SaveChanges();
        }
    }
}