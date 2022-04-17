using System;
using System.Collections.Generic;
using TigerCard.Models;

namespace TigerCard.UnitTests
{
    public class TestJourneyData
    {
        internal static List<Journey> GetJourneyDetailsForApplicableCap()
        {
            return new List<Journey>
            {
                GetJourney(new DateTime(2022, 4, 17, 10, 0, 0),"1","1",30),
                GetJourney(new DateTime(2022, 4, 17, 10, 30, 0),"1","1",30),
                GetJourney(new DateTime(2022, 4, 17, 18, 30, 0),"1","1",30),
                GetJourney(new DateTime(2022, 4, 17, 19, 30, 0),"1","1"),

            };

        }

        internal static List<Journey> GetJourneyDetailsForWeeklyCap()
        {
            return new List<Journey>
            {
                GetJourney(new DateTime(2022, 4, 18, 10, 30, 0),"1","1",100),
                GetJourney(new DateTime(2022, 4, 19, 18, 30, 0),"1","1",100),
                GetJourney(new DateTime(2022, 4, 20, 19, 30, 0),"1","1",100),
                GetJourney(new DateTime(2022, 4, 21, 18, 30, 0),"1","1",100),
                GetJourney(new DateTime(2022, 4, 22, 19, 30, 0),"1","1",30),
                GetJourney(new DateTime(2022, 4, 22, 20, 30, 0),"1","1",30),
                GetJourney(new DateTime(2022, 4, 22, 21, 30, 0),"1","1",30),
                GetJourney(new DateTime(2022, 4, 22, 21, 45, 0),"1","1")

            };

        }

        private static Journey GetJourney(DateTime journeyDateTime, string fromZoneId, string toZoneId, double fare = 0)
        {
            return new Journey
            {
                Date = journeyDateTime,
                FromZone = new Zone
                {
                    Id = fromZoneId
                },
                ToZone = new Zone
                {
                    Id = toZoneId
                },
                Fare = fare
            };
        }


    }
}
