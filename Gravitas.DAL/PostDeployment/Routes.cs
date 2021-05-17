﻿using System;
using System.Data.Entity.Migrations;
using Gravitas.Model;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.PostDeployment
{
    public static partial class PostDeployment
    {
        public static class Routes
        {
            public static void RouteMapState(GravitasDbContext context)
            {
//                context.Set<RouteMapStatus>().AddOrUpdate(new RouteMapStatus {Id = Dom.Route.Type.InContruction, Name = "В розробці"});
//                context.Set<RouteMapStatus>().AddOrUpdate(new RouteMapStatus {Id = Dom.Route.Type.Canceled, Name = "На виїзд"});
//                context.Set<RouteMapStatus>().AddOrUpdate(new RouteMapStatus {Id = Dom.Route.Type.SingleWindow, Name = "На в'їзд"});
//                context.Set<RouteMapStatus>().AddOrUpdate(new RouteMapStatus {Id = Dom.Route.Type.Temporary, Name = "Тимчасовий"});
//                context.Set<RouteMapStatus>().AddOrUpdate(new RouteMapStatus {Id = Dom.Route.Type.Reload, Name = "Перезавантаження"});
//                context.Set<RouteMapStatus>().AddOrUpdate(new RouteMapStatus {Id = Dom.Route.Type.PartLoad, Name = "Часткове завантаження"});
//                context.Set<RouteMapStatus>().AddOrUpdate(new RouteMapStatus {Id = Dom.Route.Type.PartUnload, Name = "Часткове розвантаження"});
//                context.Set<RouteMapStatus>().AddOrUpdate(new RouteMapStatus {Id = Dom.Route.Type.Move, Name = "Переміщення"});
//                context.Set<RouteMapStatus>().AddOrUpdate(new RouteMapStatus {Id = Dom.Route.Type.MixedFeedLoad, Name = "Завантаження комбікорму"});
//                context.SaveChanges();
            }
            
            public static void RouteTemplates(GravitasDbContext context)
            {
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 1, Name = "Повернення на КПП", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""1"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":""04.01.01"",""name"":""Авто Вагова №1"",""routeGroupId"":7},{""id"":702,""code"":""04.01.02"",""name"":""Авто Вагова №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""2"":{""groupId"":3,""nodeList"":[{""id"":320,""code"":""02.01.01/2"",""name"":""КПП №1 Пост №8 виїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 2, Name = "Часткове завантаження з вагової - шрот", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""2"":{""groupId"":91,""nodeList"":[{""id"":930,""code"":"""",""name"":""Завантаження шроту №1"",""routeGroupId"":93},{""id"":931,""code"":"""",""name"":""Завантаження шроту №2"",""routeGroupId"":93},{""id"":932,""code"":"""",""name"":""Завантаження шроту №3"",""routeGroupId"":93},{""id"":933,""code"":"""",""name"":""Завантаження шроту №4"",""routeGroupId"":93}],""active"":true,""quotaEnabled"":true},""3"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""4"":{""groupId"":3,""nodeList"":[{""id"":320,""code"":"""",""name"":""Охорона.КПП №1. Виїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true},""5"":{""groupId"":9,""nodeList"":[{""id"":902,""code"":"""",""name"":""Призначення точки завантаження шроту, лузги, олії"",""routeGroupId"":9}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 3, Name = "Часткове розвантаження з вагової - шрот", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""2"":{""groupId"":81,""nodeList"":[{""id"":820,""code"":"""",""name"":""Яма розгрузки №30"",""routeGroupId"":81},{""id"":821,""code"":"""",""name"":""Яма розгрузки №40"",""routeGroupId"":81},{""id"":813,""code"":"""",""name"":""Яма схема 5"",""routeGroupId"":81}],""active"":true,""quotaEnabled"":true},""3"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""4"":{""groupId"":3,""nodeList"":[{""id"":320,""code"":"""",""name"":""Охорона.КПП №1. Виїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true},""5"":{""groupId"":9,""nodeList"":[{""id"":902,""code"":"""",""name"":""Призначення точки завантаження шроту, лузги, олії"",""routeGroupId"":9}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 4, Name = "Перезавантаження (центральна лабораторія - шрот)", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""2"":{""groupId"":81,""nodeList"":[{""id"":820,""code"":"""",""name"":""Яма розгрузки №30"",""routeGroupId"":81},{""id"":821,""code"":"""",""name"":""Яма розгрузки №40"",""routeGroupId"":81},{""id"":813,""code"":"""",""name"":""Яма схема 5"",""routeGroupId"":81},{""id"":814,""code"":"""",""name"":""Авторозвантажувач ККЗ"",""routeGroupId"":81}],""active"":true,""quotaEnabled"":true},""3"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""4"":{""groupId"":91,""nodeList"":[{""id"":930,""code"":"""",""name"":""Завантаження шроту №1"",""routeGroupId"":91},{""id"":931,""code"":"""",""name"":""Завантаження шроту №2"",""routeGroupId"":91},{""id"":932,""code"":"""",""name"":""Завантаження шроту №3"",""routeGroupId"":91},{""id"":933,""code"":"""",""name"":""Завантаження шроту №4"",""routeGroupId"":91}],""active"":true,""quotaEnabled"":true},""5"":{""groupId"":5,""nodeList"":[{""id"":201,""code"":"""",""name"":""Відбір проб №1(шрот)"",""routeGroupId"":5}],""active"":true,""quotaEnabled"":true},""6"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""7"":{""groupId"":3,""nodeList"":[{""id"":320,""code"":"""",""name"":""Охорона.КПП №1. Виїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true},""8"":{""groupId"":9,""nodeList"":[{""id"":902,""code"":"""",""name"":""Призначення точки завантаження шроту, лузги, олії"",""routeGroupId"":9}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 5, Name = "Часткове завантаження з вагової - комбікорм", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""2"":{""groupId"":10,""nodeList"":[{""id"":111,""code"":"""",""name"":""Завантаження комбікорму 1"",""routeGroupId"":10},{""id"":112,""code"":"""",""name"":""Завантаження комбікорму 2"",""routeGroupId"":10},{""id"":113,""code"":"""",""name"":""Завантаження комбікорму 3"",""routeGroupId"":10},{""id"":114,""code"":"""",""name"":""Завантаження комбікорму 4"",""routeGroupId"":10}],""active"":true,""quotaEnabled"":true},""3"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""4"":{""groupId"":3,""nodeList"":[{""id"":320,""code"":"""",""name"":""Охорона.КПП №1. Виїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true},""5"":{""groupId"":10,""nodeList"":[{""id"":110,""code"":"""",""name"":""Призначення проїзду для комбікорму"",""routeGroupId"":10}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 6, Name = "Часткове розвантаження з вагової - комбікорм", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""1"":{""groupId"":81,""nodeList"":[{""id"":830,""code"":"""",""name"":""Вивантаження склад"",""routeGroupId"":81}],""active"":true,""quotaEnabled"":true},""2"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""3"":{""groupId"":3,""nodeList"":[{""id"":320,""code"":"""",""name"":""Охорона.КПП №1. Виїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true},""4"":{""groupId"":10,""nodeList"":[{""id"":110,""code"":"""",""name"":""Призначення проїзду для комбікорму"",""routeGroupId"":10}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 7, Name = "Переміщення з Центральної лабораторії", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""1"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""2"":{""groupId"":81,""nodeList"":[{""id"":820,""code"":"""",""name"":""Яма розгрузки №30"",""routeGroupId"":81},{""id"":821,""code"":"""",""name"":""Яма розгрузки №40"",""routeGroupId"":81},{""id"":813,""code"":"""",""name"":""Яма схема 5"",""routeGroupId"":81}],""active"":true,""quotaEnabled"":true},""3"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""4"":{""groupId"":9,""nodeList"":[{""id"":902,""code"":"""",""name"":""Призначення точки завантаження шроту, лузги, олії"",""routeGroupId"":9}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 8, Name = "Завантаження комбікорму СГП 1", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""2"":{""groupId"":3,""nodeList"":[{""id"":310,""code"":"""",""name"":""Охорона.КПП №1. Вїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true},""3"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""4"":{""groupId"":10,""nodeList"":[{""id"":111,""code"":"""",""name"":""Завантаження комбікорму 1"",""routeGroupId"":10}],""active"":true,""quotaEnabled"":true},""5"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""6"":{""groupId"":3,""nodeList"":[{""id"":320,""code"":"""",""name"":""Охорона.КПП №1. Виїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true},""7"":{""groupId"":10,""nodeList"":[{""id"":110,""code"":""Комбікормовий завод"",""name"":""Призначення точки завантаження"",""routeGroupId"":10}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 9, Name = "Завантаження комбікорму СГП 2", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""2"":{""groupId"":3,""nodeList"":[{""id"":310,""code"":"""",""name"":""Охорона.КПП №1. Вїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true},""3"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""4"":{""groupId"":10,""nodeList"":[{""id"":112,""code"":"""",""name"":""Завантаження комбікорму 2"",""routeGroupId"":10}],""active"":true,""quotaEnabled"":true},""5"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""6"":{""groupId"":3,""nodeList"":[{""id"":320,""code"":"""",""name"":""Охорона.КПП №1. Виїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true},""7"":{""groupId"":10,""nodeList"":[{""id"":110,""code"":""Комбікормовий завод"",""name"":""Призначення точки завантаження"",""routeGroupId"":10}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 10, Name = "Завантаження комбікорму СГП 3", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""2"":{""groupId"":3,""nodeList"":[{""id"":310,""code"":"""",""name"":""Охорона.КПП №1. Вїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true},""3"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""4"":{""groupId"":10,""nodeList"":[{""id"":113,""code"":"""",""name"":""Завантаження комбікорму 3"",""routeGroupId"":10}],""active"":true,""quotaEnabled"":true},""5"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""6"":{""groupId"":3,""nodeList"":[{""id"":320,""code"":"""",""name"":""Охорона.КПП №1. Виїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true},""7"":{""groupId"":10,""nodeList"":[{""id"":110,""code"":""Комбікормовий завод"",""name"":""Призначення точки завантаження"",""routeGroupId"":10}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 11, Name = "Завантаження комбікорму СГП 4", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""2"":{""groupId"":3,""nodeList"":[{""id"":310,""code"":"""",""name"":""Охорона.КПП №1. Вїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true},""3"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""4"":{""groupId"":10,""nodeList"":[{""id"":114,""code"":"""",""name"":""Завантаження комбікорму 4"",""routeGroupId"":10}],""active"":true,""quotaEnabled"":true},""5"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"""",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""6"":{""groupId"":3,""nodeList"":[{""id"":320,""code"":"""",""name"":""Охорона.КПП №1. Виїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true},""7"":{""groupId"":10,""nodeList"":[{""id"":110,""code"":""Комбікормовий завод"",""name"":""Призначення точки завантаження"",""routeGroupId"":10}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 12, Name = "Погрузка зернових (без відходів)", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""1"":{""groupId"":9,""nodeList"":[{""id"":902,""code"":"""",""name"":""Призначення завантаження шроту, лузги, олії"",""routeGroupId"":9}],""active"":true,""quotaEnabled"":true},""2"":{""groupId"":3,""nodeList"":[{""id"":310,""code"":""02.01.01/1"",""name"":""КПП №1 Пост №8 в'їзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true},""3"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":""04.01.01"",""name"":""Авто Вагова №1"",""routeGroupId"":7},{""id"":702,""code"":""04.01.02"",""name"":""Авто Вагова №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""4"":{""groupId"":91,""nodeList"":[{""id"":933,""code"":"""",""name"":""Елеватор №1 Завантаження зернових "",""routeGroupId"":91}],""active"":true,""quotaEnabled"":true},""5"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":""04.01.01"",""name"":""Авто Вагова №1"",""routeGroupId"":7},{""id"":702,""code"":""04.01.02"",""name"":""Авто Вагова №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""6"":{""groupId"":5,""nodeList"":[{""id"":503,""code"":""03.01.01"",""name"":""Дільниця №2 Візіровка Проїзд №3"",""routeGroupId"":5},{""id"":504,""code"":""03.01.02"",""name"":""Дільниця №2 Візіровка Проїзд №4"",""routeGroupId"":5}],""active"":true,""quotaEnabled"":true},""7"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":""04.01.01"",""name"":""Авто Вагова №1"",""routeGroupId"":7},{""id"":702,""code"":""04.01.02"",""name"":""Авто Вагова №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""8"":{""groupId"":3,""nodeList"":[{""id"":320,""code"":""02.01.01/2"",""name"":""КПП №1 Пост №8 виїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 13, Name = "Часткове розвантаження зернових (без відходів)", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""1"":{""groupId"":81,""nodeList"":[{""id"":813,""code"":"""",""name"":""Авторозвантажувач  Схема 5"",""routeGroupId"":81}],""active"":true,""quotaEnabled"":true},""2"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":""04.01.01"",""name"":""Авто Вагова №1"",""routeGroupId"":7},{""id"":702,""code"":""04.01.02"",""name"":""Авто Вагова №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""3"":{""groupId"":5,""nodeList"":[{""id"":503,""code"":""03.01.01"",""name"":""Дільниця №2 Візіровка Проїзд №3"",""routeGroupId"":5},{""id"":504,""code"":""03.01.02"",""name"":""Дільниця №2 Візіровка Проїзд №4"",""routeGroupId"":5}],""active"":true,""quotaEnabled"":true},""4"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":""04.01.01"",""name"":""Авто Вагова №1"",""routeGroupId"":7},{""id"":702,""code"":""04.01.02"",""name"":""Авто Вагова №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""5"":{""groupId"":9,""nodeList"":[{""id"":902,""code"":"""",""name"":""Призначення завантаження шроту, лузги, олії"",""routeGroupId"":9}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 14, Name = "Часткове довантаження зернових (без відходів)", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""1"":{""groupId"":91,""nodeList"":[{""id"":933,""code"":"",""name"":""Елеватор №1 Завантаження зернових "",""routeGroupId"":91}],""active"":true,""quotaEnabled"":true},""2"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":""04.01.01"",""name"":""Авто Вагова №1"",""routeGroupId"":7},{""id"":702,""code"":""04.01.02"",""name"":""Авто Вагова №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""3"":{""groupId"":5,""nodeList"":[{""id"":503,""code"":""03.01.01"",""name"":""Дільниця №2 Візіровка Проїзд №3"",""routeGroupId"":5},{""id"":504,""code"":""03.01.02"",""name"":""Дільниця №2 Візіровка Проїзд №4"",""routeGroupId"":5}],""active"":true,""quotaEnabled"":true},""4"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":""04.01.01"",""name"":""Авто Вагова №1"",""routeGroupId"":7},{""id"":702,""code"":""04.01.02"",""name"":""Авто Вагова №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""5"":{""groupId"":9,""nodeList"":[{""id"":902,""code"":"""",""name"":""Призначення завантаження шроту, лузги, олії"",""routeGroupId"":9}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.Set<RouteTemplate>().AddOrUpdate(new RouteTemplate {Id = 15, Name = "Перезавантаження (візірока - зернові) (без відходів)", RouteConfig = @"{""currentGroupId"":null,""disableAppend"":false,""groupDictionary"":{""2"":{""groupId"":81,""nodeList"":[{""id"":813,""code"":"""",""name"":""Яма схема 5"",""routeGroupId"":81}],""active"":true,""quotaEnabled"":true},""3"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""4"":{""groupId"":91,""nodeList"":[{""id"":910,""code"":"""",""name"":""Елеватор №2. Завантаження зернових №1"",""routeGroupId"":91},{""id"":911,""code"":"""",""name"":""Елеватор №2. Завантаження зернових №2"",""routeGroupId"":91},{""id"":912,""code"":"""",""name"":""Елеватор №3 Завантаження зернових №1"",""routeGroupId"":91},{""id"":933,""code"":"""",""name"":""Завантаження шроту №4"",""routeGroupId"":91}],""active"":true,""quotaEnabled"":true},""6"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":"",""name"":""Вагова.Авто №1"",""routeGroupId"":7},{""id"":702,""code"":"""",""name"":""Вагова.Авто №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""7"":{""groupId"":5,""nodeList"":[{""id"":503,""code"":""03.01.01"",""name"":""Дільниця №2 Візіровка Проїзд №3"",""routeGroupId"":5},{""id"":504,""code"":""03.01.02"",""name"":""Дільниця №2 Візіровка Проїзд №4"",""routeGroupId"":5}],""active"":true,""quotaEnabled"":true},""8"":{""groupId"":7,""nodeList"":[{""id"":701,""code"":""04.01.01"",""name"":""Авто Вагова №1"",""routeGroupId"":7},{""id"":702,""code"":""04.01.02"",""name"":""Авто Вагова №2"",""routeGroupId"":7}],""active"":true,""quotaEnabled"":true},""9"":{""groupId"":3,""nodeList"":[{""id"":320,""code"":""02.01.01/2"",""name"":""КПП №1 Пост №8 виїзд"",""routeGroupId"":3}],""active"":true,""quotaEnabled"":true}}}", CreatedOn = DateTime.Now});
                context.SaveChanges();
            }
            
            public static void RouteMaps(GravitasDbContext context)
            {
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 1, NodeId = (long) NodeIdValue.Weighbridge1, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 2, NodeId = (long) NodeIdValue.Weighbridge2, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 3, NodeId = (long) NodeIdValue.Weighbridge3, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 4, NodeId = (long) NodeIdValue.Weighbridge4, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 5, NodeId = (long) NodeIdValue.Laboratory0, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 6, NodeId = (long) NodeIdValue.Laboratory3, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 7, NodeId = (long) NodeIdValue.Laboratory4, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 8, NodeId = (long) NodeIdValue.MixedFeedLoad1, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 9, NodeId = (long) NodeIdValue.MixedFeedLoad2, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 10, NodeId = (long) NodeIdValue.MixedFeedLoad3, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 11, NodeId = (long) NodeIdValue.MixedFeedLoad4, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 12, NodeId = (long) NodeIdValue.LoadPoint30, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 13, NodeId = (long) NodeIdValue.LoadPoint31, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 14, NodeId = (long) NodeIdValue.LoadPoint32, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 15, NodeId = (long) NodeIdValue.LoadPoint33, RouteTemplateId = 1, StatusId = Dom.Route.Type.Canceled});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 16, NodeId = (long) NodeIdValue.CentralLaboratoryProcess1, RouteTemplateId = 4, StatusId = Dom.Route.Type.Reload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 17, NodeId = (long) NodeIdValue.CentralLaboratoryProcess2, RouteTemplateId = 4, StatusId = Dom.Route.Type.Reload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 18, NodeId = (long) NodeIdValue.CentralLaboratoryProcess3, RouteTemplateId = 4, StatusId = Dom.Route.Type.Reload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 19, NodeId = (long) NodeIdValue.LoadPoint30, RouteTemplateId = 4, StatusId = Dom.Route.Type.Reload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 20, NodeId = (long) NodeIdValue.LoadPoint31, RouteTemplateId = 4, StatusId = Dom.Route.Type.Reload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 21, NodeId = (long) NodeIdValue.LoadPoint32, RouteTemplateId = 4, StatusId = Dom.Route.Type.Reload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 22, NodeId = (long) NodeIdValue.LoadPoint33, RouteTemplateId = 4, StatusId = Dom.Route.Type.Reload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 23, NodeId = (long) NodeIdValue.LoadPoint30, RouteTemplateId = 2, StatusId = Dom.Route.Type.PartLoad});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 24, NodeId = (long) NodeIdValue.LoadPoint31, RouteTemplateId = 2, StatusId = Dom.Route.Type.PartLoad});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 25, NodeId = (long) NodeIdValue.LoadPoint32, RouteTemplateId = 2, StatusId = Dom.Route.Type.PartLoad});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 26, NodeId = (long) NodeIdValue.LoadPoint33, RouteTemplateId = 2, StatusId = Dom.Route.Type.PartLoad});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 27, NodeId = (long) NodeIdValue.LoadPoint30, RouteTemplateId = 3, StatusId = Dom.Route.Type.PartUnload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 28, NodeId = (long) NodeIdValue.LoadPoint31, RouteTemplateId = 3, StatusId = Dom.Route.Type.PartUnload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 29, NodeId = (long) NodeIdValue.LoadPoint32, RouteTemplateId = 3, StatusId = Dom.Route.Type.PartUnload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 30, NodeId = (long) NodeIdValue.LoadPoint33, RouteTemplateId = 3, StatusId = Dom.Route.Type.PartUnload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 31, NodeId = (long) NodeIdValue.MixedFeedLoad1, RouteTemplateId = 5, StatusId = Dom.Route.Type.PartLoad});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 32, NodeId = (long) NodeIdValue.MixedFeedLoad2, RouteTemplateId = 5, StatusId = Dom.Route.Type.PartLoad});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 33, NodeId = (long) NodeIdValue.MixedFeedLoad3, RouteTemplateId = 5, StatusId = Dom.Route.Type.PartLoad});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 34, NodeId = (long) NodeIdValue.MixedFeedLoad4, RouteTemplateId = 5, StatusId = Dom.Route.Type.PartLoad});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 35, NodeId = (long) NodeIdValue.MixedFeedLoad1, RouteTemplateId = 6, StatusId = Dom.Route.Type.PartUnload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 36, NodeId = (long) NodeIdValue.MixedFeedLoad2, RouteTemplateId = 6, StatusId = Dom.Route.Type.PartUnload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 37, NodeId = (long) NodeIdValue.MixedFeedLoad3, RouteTemplateId = 6, StatusId = Dom.Route.Type.PartUnload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 38, NodeId = (long) NodeIdValue.MixedFeedLoad4, RouteTemplateId = 6, StatusId = Dom.Route.Type.PartUnload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 39, NodeId = (long) NodeIdValue.CentralLaboratoryProcess1, RouteTemplateId = 7, StatusId = Dom.Route.Type.Move});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 40, NodeId = (long) NodeIdValue.CentralLaboratoryProcess2, RouteTemplateId = 7, StatusId = Dom.Route.Type.Move});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 41, NodeId = (long) NodeIdValue.CentralLaboratoryProcess3, RouteTemplateId = 7, StatusId = Dom.Route.Type.Move});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 42, NodeId = (long) NodeIdValue.MixedFeedLoad1, RouteTemplateId = 8, StatusId = Dom.Route.Type.MixedFeedLoad});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 43, NodeId = (long) NodeIdValue.MixedFeedLoad2, RouteTemplateId = 9, StatusId = Dom.Route.Type.MixedFeedLoad});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 44, NodeId = (long) NodeIdValue.MixedFeedLoad3, RouteTemplateId = 10, StatusId = Dom.Route.Type.MixedFeedLoad});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 45, NodeId = (long) NodeIdValue.MixedFeedLoad4, RouteTemplateId = 11, StatusId = Dom.Route.Type.MixedFeedLoad});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 46, NodeId = (long) NodeIdValue.SingleWindowFirst, RouteTemplateId = 12, StatusId = Dom.Route.Type.SingleWindow});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 47, NodeId = (long) NodeIdValue.LoadPoint33, RouteTemplateId = 13, StatusId = Dom.Route.Type.PartUnload});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 48, NodeId = (long) NodeIdValue.LoadPoint33, RouteTemplateId = 14, StatusId = Dom.Route.Type.PartLoad});
//                context.Set<RouteMap>().AddOrUpdate(new RouteMap {Id = 49, NodeId = (long) NodeIdValue.LoadPoint33, RouteTemplateId = 15, StatusId = Dom.Route.Type.Reload});
//                context.SaveChanges();
            }
        }
    }
}