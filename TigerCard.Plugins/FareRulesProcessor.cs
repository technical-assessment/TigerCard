using System;
using System.Collections.Generic;
using TigerCard.Models;
using System.Linq;
namespace TigerCard.Plugins
{
    public class FareRulesProcessor : IFareRules
    {
        private readonly IBusinessConfigurationProvider _businessConfigurationProvider;

        public FareRulesProcessor(IBusinessConfigurationProvider businessConfigurationProvider)
        {
            _businessConfigurationProvider = businessConfigurationProvider;
        }

        #region Fare Cap Rule
        public FareCapResponse ApplyFareCapRule(List<Journey> journeyList, Journey currentJourney, double expectedFare)
        {
            var response = new FareCapResponse();

            var capLimit = _businessConfigurationProvider.GetFareCapLimit(currentJourney.FromZone.Id, currentJourney.ToZone.Id);

            if (!CheckAndApplyWeeklyCap(journeyList, currentJourney, expectedFare, response, capLimit.Weekly))
            {
                if (!CheckAndApplyDailyCap(journeyList, currentJourney, expectedFare, response,capLimit.Daily))
                {
                    currentJourney.Fare = expectedFare;
                }
            }
            return response;
        }

        private bool CheckAndApplyDailyCap(List<Journey> journeyList, Journey currentJourney, double expectedFare, FareCapResponse response,double dailyCapFare)
        {
            

            var dailySumOfFare = journeyList.FindAll(x => x.Date.Date.Day == currentJourney.Date.Day)
                                     .Sum(x => x.Fare);
            if (dailySumOfFare + expectedFare > dailyCapFare)
            {
                response.CapFare = Math.Abs(dailySumOfFare - dailyCapFare);
                response.IsFareCapApplicable = true;
                return true;
            }
            return false;
        }

        private bool CheckAndApplyWeeklyCap(List<Journey> journeyList, Journey currentJourney, double expectedFare,FareCapResponse response,double weeklyCapFare)
        {
            var startOfWeek = currentJourney.Date.FirstDateInWeek(DayOfWeek.Monday);
            var weeklySumOfFare = journeyList.FindAll(x => x.Date >= startOfWeek
                                     && x.Date <= currentJourney.Date)
                                     .Sum(x => x.Fare);

            if (weeklySumOfFare + expectedFare > weeklyCapFare)
            {
                response.CapFare = Math.Abs(weeklySumOfFare - weeklyCapFare);
                response.IsFareCapApplicable = true;
                return true;
            }

            return false;
        }
        #endregion

        #region Peak Fare Rule

        public double GetFare(Journey currentJourney)
        {
            var fareDetails = _businessConfigurationProvider.GetFareDetails();
            var peakHours = _businessConfigurationProvider.GetPeakHours();

            var fare = fareDetails.FirstOrDefault(t => t.SourceZoneId == currentJourney.FromZone.Id && t.ToZoneId == currentJourney.ToZone.Id);
            if (IfJourneyDuringPeakHours(currentJourney.Date, peakHours))
            {
                return fare.PeakFare;
            }
            else
            {
                return fare.BaseFare;
            }
        }

        private bool IfJourneyDuringPeakHours(DateTime journeyTime, List<PeakHours> peakHours)
        {
            foreach (var item in peakHours)
            {
                if (item.Day == journeyTime.DayOfWeek)
                {
                    foreach (var window in item.Windows)
                    {
                        if (Convert.ToInt32(window.StartTime.ToString("hh")) <= Convert.ToInt32(journeyTime.ToString("hh"))
                            && Convert.ToInt32(window.EndTime.ToString("hh")) >= Convert.ToInt32(journeyTime.ToString("hh")))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        #endregion

    }
}
