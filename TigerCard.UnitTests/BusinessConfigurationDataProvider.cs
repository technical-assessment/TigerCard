using System;
using System.Collections.Generic;
using TigerCard.Models;
using System.Linq;

namespace TigerCard.UnitTests
{
    public class BusinessConfigurationDataProvider
    {
        private static List<FareCapLimit> capLimits;
        static BusinessConfigurationDataProvider()
        {
            capLimits = new List<FareCapLimit>
                {
                    new FareCapLimit
                    {
                        FromZoneId = "1",
                        ToZoneId = "1",
                        Daily = 100,
                        Weekly = 500
                    },
                    new FareCapLimit
                    {
                        FromZoneId = "1",
                        ToZoneId = "2",
                        Daily = 120,
                        Weekly = 600
                    },
                    new FareCapLimit
                    {
                        FromZoneId = "2",
                        ToZoneId = "1",
                        Daily = 120,
                        Weekly = 600
                    },
                    new FareCapLimit
                    {
                        FromZoneId = "2",
                        ToZoneId = "1",
                        Daily = 80,
                        Weekly = 400
                    }
                };
        }
        internal static List<FareDetail> GetFareDetails()
        {
            return new List<FareDetail>
            {
                new FareDetail
                {
                    SourceZoneId = "1",
                    ToZoneId = "1",
                    BaseFare = 25,
                    PeakFare = 30
                },
                new FareDetail
                {
                    SourceZoneId = "1",
                    ToZoneId = "2",
                    BaseFare = 30,
                    PeakFare = 35
                },
                new FareDetail
                {
                    SourceZoneId = "2",
                    ToZoneId = "1",
                    BaseFare = 30,
                    PeakFare = 25
                },
                new FareDetail
                {
                    SourceZoneId = "2",
                    ToZoneId = "1",
                    BaseFare = 20,
                    PeakFare = 25
                }
            };
        }

        internal static List<PeakHours> GetPeakHours()
        {
            return new List<PeakHours>
            {
                SetPeakHours(DayOfWeek.Monday),
                SetPeakHours(DayOfWeek.Tuesday),
                SetPeakHours(DayOfWeek.Wednesday),
                SetPeakHours(DayOfWeek.Thursday),
                SetPeakHours(DayOfWeek.Friday),
                SetPeakHours(DayOfWeek.Saturday, new string[]{"09:00", "11:00" },  new string[]{"18:00", "22:00" }),
                SetPeakHours(DayOfWeek.Sunday, new string[]{"09:00", "11:00" },  new string[]{"18:00", "22:00" }),
            };
        }

        private static PeakHours SetPeakHours(DayOfWeek dayOfWeek)
        {
            return new PeakHours
            {
                Day = dayOfWeek,
                Windows = new List<Window>
                    {
                        new Window
                        {
                            StartTime = Convert.ToDateTime("01-01-1900 07:00"),
                            EndTime = Convert.ToDateTime("01-01-1900 10:30")
                        },
                        new Window
                        {
                            StartTime = Convert.ToDateTime("01-01-1900 17:00"),
                            EndTime = Convert.ToDateTime("01-01-1900 20:00")
                        }
                    }
            };
        }

        private static PeakHours SetPeakHours(DayOfWeek dayOfWeek, string[] morningWindow, string[] eveningWindow)
        {
            return new PeakHours
            {
                Day = dayOfWeek,
                Windows = new List<Window>
                    {
                        new Window
                        {
                            StartTime = Convert.ToDateTime("01-01-1900 " + morningWindow[0]),
                            EndTime = Convert.ToDateTime("01-01-1900 " + morningWindow[1]),
                        },
                        new Window
                        {
                            StartTime = Convert.ToDateTime("01-01-1900 " + eveningWindow[0]),
                            EndTime = Convert.ToDateTime("01-01-1900 " + eveningWindow[1]),
                        }
                    }
            };
        }

        internal static FareCapLimit GetCapLimits(string fromZoneId, string toZoneId)
        {
            return capLimits.FirstOrDefault(t => t.FromZoneId == fromZoneId && t.ToZoneId == toZoneId);
        }
    }
}
