using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.PreRegistration.Api.Models;

namespace Gravitas.PreRegistration.Api.Helpers
{
    public class ProductHelper
    {
        private static readonly byte DaysForRegistration = 3;
        private static readonly int MinutesInDay = 1440;

        public static List<ProductItemDto> ConvertToDto(List<ProductItem> data)
        {
            var result = new List<ProductItemDto>();

            foreach (var item in data)
            {
                var newItem = new ProductItemDto()
                {
                    RouteId = item.RouteId,
                    Title = item.Title,
                    FreeDateTimeList = GetFreeDateTimeList(item.BusyDateTimeList, GetFirstAvailableHour(item.RouteTimeInMinutes),
                        DateTime.Now.AddDays(DaysForRegistration), new TimeSpan(0, item.RouteTimeInMinutes, 0)),
                    TrucksInQueue = item.TrucksInQueue
                };
                result.Add(newItem);
            }

            return result;
        }

        public static List<BarChartInfo> GetBarChartInfos(List<ProductItem> data)
        {
            var result = new List<BarChartInfo>();
            foreach (var item in data)
            {
                var newItem = new BarChartInfo()
                {
                    ProductTitle = item.Title,
                    Labels = GetBarChartLabels(),
                    DataCount = GetBarChartDataCount(item),
                    Data = GetBarChartData(item)
                };
                result.Add(newItem);
            }

            return result;
        }

        private static string[] GetBarChartLabels()
        {
            var result = new List<string>();
            var date = DateTime.Now;

            for (var i = 0; i <= DaysForRegistration; ++i)
            {
                result.Add(date.AddDays(i).ToString("dd/MM/yyyy"));
            }

            return result.ToArray();
        }

        private static int[,] GetBarChartData(ProductItem product)
        {
            var result = new int[GetBarChartDataCount(product), DaysForRegistration + 1];

            var fromFirstAvailableHour = GetFirstAvailableHour(product.RouteTimeInMinutes);
            var toDateTime = fromFirstAvailableHour.AddDays(DaysForRegistration);
            var eventDuration = new TimeSpan(0, product.RouteTimeInMinutes, 0);
            var currentDateTime = fromFirstAvailableHour;
            var dayCounter = 0;
            var hourCounter = 0;
            var dataCount = GetBarChartDataCount(product) - 1;

            for (var i = fromFirstAvailableHour; i < toDateTime; i += eventDuration)
            {
                if (currentDateTime.Day < i.Day)
                {
                    ++dayCounter;
                    hourCounter = 0;
                    currentDateTime = i;
                }

                if (product.BusyDateTimeList.Any(e => e.ToString() == i.ToString()))
                {
                    if (dayCounter != 0)
                    {
                        result[hourCounter, dayCounter] = 1;

                    }
                    else
                    {
                        result[dataCount - hourCounter, dayCounter] = 1;
                    }
                }
                else
                {
                    if (dayCounter != 0)
                    {
                        result[hourCounter, dayCounter] = 0;

                    }
                    else
                    {
                        result[dataCount - hourCounter, dayCounter] = 0;
                    }
                }
                ++hourCounter;
            }

            return result;
        }

        private static int GetBarChartDataCount(ProductItem product)
        {
            return MinutesInDay / product.RouteTimeInMinutes;
        }

        private static List<DateTime> GetFreeDateTimeList(List<DateTime> busyDateTimes, DateTime fromDateTime, DateTime toDateTime, TimeSpan eventDuration)
        {
            var result = new List<DateTime>();
            for (var i = fromDateTime; i < toDateTime; i += eventDuration)
            {
                if (busyDateTimes.All(e => e.ToString() != i.ToString()))
                {
                    result.Add(i);
                }
            }

            return result;
        }

        private static DateTime GetFirstAvailableHour(int routeTimeInMinutes)
        {
            var dateTimeNow = DateTime.Now;
            var dateNow = dateTimeNow.Subtract(new TimeSpan(dateTimeNow.Hour, dateTimeNow.Minute, dateTimeNow.Second));
            var eventDuration = new TimeSpan(0, routeTimeInMinutes, 0);

            var result = dateNow;
            while (result <= dateTimeNow)
            {
                result += eventDuration;
            }

            return result;
        }
    }
}