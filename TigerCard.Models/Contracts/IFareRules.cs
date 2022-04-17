using System;
using System.Collections.Generic;

namespace TigerCard.Models
{
    public interface IFareRules
    {
        FareCapResponse ApplyFareCapRule(List<Journey> journeyList, Journey currentJourney, double expectedFare);

        double GetFare(Journey currentJourney);
    }
}
