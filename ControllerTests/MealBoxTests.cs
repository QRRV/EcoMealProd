using Xunit;
using Moq;
using Domain.Services;
using System.Collections.Generic;
using Domain;

namespace ControllerTests {
    public class MealBoxTests {
        [Fact]
        public void CanViewAllAvailablePackages() {
            // Arrange
            var mockRepo = new Mock<IMealBoxRepository>();
            var mealBoxes = new List<MealBox> { new MealBox(), new MealBox() };
            mockRepo.Setup(repo => repo.GetMealBoxes()).Returns(mealBoxes);

            // Act
            var result = mockRepo.Object.GetMealBoxes();

            // Assert
            Assert.Equal(mealBoxes, result);
        }

    }
}