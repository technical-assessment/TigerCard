using System;
using System.Collections.Generic;

using System.Linq;
using TigerCard.Models;

namespace TigerCard.Core
{
    public class FareCalculatorService
    {
        private readonly IFareRules _fareRules;
        public FareCalculatorService(IFareRules fareRules)
        {
            _fareRules = fareRules;
        }
        public void CalculateFare(List<Journey> journeys)
        {
            foreach (var journey in journeys.OrderBy(t => t.Date))
            {
                var expectedFare = _fareRules.GetFare(journey);
                var fareCapResoponse = _fareRules.ApplyFareCapRule(journeys, journey, expectedFare);
                if (fareCapResoponse.IsFareCapApplicable)
                {
                    journey.Fare = fareCapResoponse.CapFare;
                }
                else
                    journey.Fare = expectedFare;
            }
        }
    }
}
