using System;
using System.Collections.Generic;
using Moq;
using TigerCard.Core;
using TigerCard.Models;
using Xunit;
using System.Linq;

namespace TigerCard.UnitTests
{
    public class FareCalculatorServiceTest
    {
        [Fact]
        public void FareCalculatorService_CalculateFare_NonPeakHours_ShouldReturnSuccess()
        {
            // Arrange
            var expectedFare = 30;
            var journey = new Journey { };
            var journeyList = new List<Journey>();
            journeyList.Add(journey);

            var fareCapresonse = new FareCapResponse { IsFareCapApplicable = false };


            var fareRules = new Mock<IFareRules>();
            fareRules.Setup(x => x.GetFare(It.IsAny<Journey>())).Returns(expectedFare);
            fareRules.Setup(x => x.ApplyFareCapRule(It.IsAny<List<Journey>>(), It.IsAny<Journey>(), expectedFare)).Returns(fareCapresonse);



            //Act
            var journeyComponent = new FareCalculatorService(fareRules.Object);
            journeyComponent.CalculateFare(journeyList);

            //Assert
            var actualJourney = journeyList.FirstOrDefault();
            Assert.NotNull(actualJourney);
            Assert.True(actualJourney.Fare == expectedFare);
        }

        [Fact]
        public void FareCalculatorService_CalculateFare_PeakHours_ShouldReturnSuccess()
        {
            // Arrange
            var expectedFare = 35;
            var journey = new Journey { };
            var journeyList = new List<Journey>();
            journeyList.Add(journey);

            var fareCapresonse = new FareCapResponse { IsFareCapApplicable = false };


            var fareRules = new Mock<IFareRules>();
            fareRules.Setup(x => x.GetFare(It.IsAny<Journey>())).Returns(expectedFare);
            fareRules.Setup(x => x.ApplyFareCapRule(It.IsAny<List<Journey>>(), It.IsAny<Journey>(), expectedFare)).Returns(fareCapresonse);



            //Act
            var journeyComponent = new FareCalculatorService(fareRules.Object);
            journeyComponent.CalculateFare(journeyList);

            //Assert
            var actualJourney = journeyList.FirstOrDefault();
            Assert.NotNull(actualJourney);
            Assert.True(actualJourney.Fare == 35);
        }
    }
}
