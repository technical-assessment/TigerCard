using System;
using System.Collections.Generic;
using Moq;
using TigerCard.Models;
using TigerCard.Plugins;
using Xunit;
using System.Linq;

namespace TigerCard.UnitTests
{
    public class FareRulesProcessorTest
    {
        [Fact]
        public void GetFare_PeakHours_Success()
        {
            //Arrange
            var configurationProvider = new Mock<IBusinessConfigurationProvider>();
            configurationProvider.Setup(x =>
                        x.GetFareDetails())
                .Returns(BusinessConfigurationDataProvider.GetFareDetails());

            configurationProvider.Setup(x =>
                        x.GetPeakHours())
                .Returns(BusinessConfigurationDataProvider.GetPeakHours());

            var jouney = new Journey
            {
                Date = new DateTime(2022, 4, 17, 10, 0, 0),
                FromZone = new Zone
                {
                    Id = "1"
                },
                ToZone = new Zone
                {
                    Id = "1"
                }

            };

            //Act
            var ruleProcessor = new FareRulesProcessor(configurationProvider.Object);
            var fare = ruleProcessor.GetFare(jouney);

            //Assert
            Assert.Equal(30,fare);
        }

        [Fact]
        public void GetFare_NonPeakHours_Success()
        {
            //Arrange
            var configurationProvider = new Mock<IBusinessConfigurationProvider>();
            configurationProvider.Setup(x =>
                        x.GetFareDetails())
                .Returns(BusinessConfigurationDataProvider.GetFareDetails());

            configurationProvider.Setup(x =>
                        x.GetPeakHours())
                .Returns(BusinessConfigurationDataProvider.GetPeakHours());

            var jouney = new Journey
            {
                Date = new DateTime(2022, 4, 17, 17, 0, 0),
                FromZone = new Zone
                {
                    Id = "1"
                },
                ToZone = new Zone
                {
                    Id = "1"
                }

            };

            //Act
            var ruleProcessor = new FareRulesProcessor(configurationProvider.Object);
            var fare = ruleProcessor.GetFare(jouney);

            //Assert
            Assert.Equal(25, fare);
        }

        [Fact]
        public void ApplyCapFareRule_DailyCapShouldApplied()
        {
            //Arrange
            var configurationProvider = new Mock<IBusinessConfigurationProvider>();
            configurationProvider.Setup(x =>
                        x.GetFareCapLimit("1", "1"))
                .Returns(BusinessConfigurationDataProvider.GetCapLimits("1", "1"));

            List<Journey> journeys = TestJourneyData.GetJourneyDetailsForApplicableCap();
            var currentJourney = journeys.LastOrDefault();

            //Act
            var ruleProcessor = new FareRulesProcessor(configurationProvider.Object);
            var fareCapResponse = ruleProcessor.ApplyFareCapRule(journeys, currentJourney,30);

            //Assert
            Assert.NotNull(fareCapResponse);
            Assert.True(fareCapResponse.IsFareCapApplicable);
            Assert.Equal(10, fareCapResponse.CapFare);
        }

        [Fact]
        public void ApplyCapFareRule_WeeklyCapShouldApplied()
        {
            //Arrange
            var configurationProvider = new Mock<IBusinessConfigurationProvider>();
            configurationProvider.Setup(x =>
                        x.GetFareCapLimit("1", "1"))
                .Returns(BusinessConfigurationDataProvider.GetCapLimits("1", "1"));

            List<Journey> journeys = TestJourneyData.GetJourneyDetailsForWeeklyCap();
            var currentJourney = journeys.LastOrDefault();

            //Act
            var ruleProcessor = new FareRulesProcessor(configurationProvider.Object);
            var fareCapResponse = ruleProcessor.ApplyFareCapRule(journeys, currentJourney, 30);

            //Assert
            Assert.NotNull(fareCapResponse);
            Assert.True(fareCapResponse.IsFareCapApplicable);
            Assert.Equal(10, fareCapResponse.CapFare);
        }
    }
}
