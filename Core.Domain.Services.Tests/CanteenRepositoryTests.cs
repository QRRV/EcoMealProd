using Domain;
using Domain.Services;
using Moq;

namespace Core.Domain.Services.Tests
{
    public class CanteenRepositoryTests
    {
        [Fact]
        public void GetCanteens_Should_ReturnAllCanteens()
        {
            // Arrange
            var canteenRepositoryMock = new Mock<ICanteenRepository>();
            var canteens = new List<Canteen?>
            {
                new Canteen { Id = 1, City = "Breda", Location = "LA", HotMeals = true },
                new Canteen { Id = 2, City = "Tilburg", Location = "LD", HotMeals = false }
            };

            canteenRepositoryMock.Setup(repo => repo.GetCanteens()).Returns(canteens);

            // Act
            var result = canteenRepositoryMock.Object.GetCanteens();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c?.City == "Breda" && c.Location == "LA" && c.HotMeals);
            Assert.Contains(result, c => c?.City == "Tilburg" && c.Location == "LD" && !c.HotMeals);
        }

        [Fact]
        public void GetCanteen_Should_ReturnCanteenById()
        {
            // Arrange
            var canteenRepositoryMock = new Mock<ICanteenRepository>();
            var canteen = new Canteen { Id = 1, City = "Den Bosch", Location = "LE", HotMeals = true };

            canteenRepositoryMock.Setup(repo => repo.GetCanteen(1)).Returns(canteen);

            // Act
            var result = canteenRepositoryMock.Object.GetCanteen(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Den Bosch", result?.City);
            Assert.Equal("LE", result?.Location);
            Assert.True(result?.HotMeals);
        }

        [Fact]
        public void GetStudentCanteen_Should_ReturnCanteenByStudentCity()
        {
            // Arrange
            var canteenRepositoryMock = new Mock<ICanteenRepository>();
            var studentCity = "Breda";
            var canteen = new Canteen { Id = 3, City = studentCity, Location = "LA", HotMeals = true };

            canteenRepositoryMock.Setup(repo => repo.GetStudentCanteen(studentCity)).Returns(canteen);

            // Act
            var result = canteenRepositoryMock.Object.GetStudentCanteen(studentCity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(studentCity, result?.City);
            Assert.Equal("LA", result?.Location);
            Assert.True(result?.HotMeals);
        }

        [Fact]
        public void CreateCanteen_Should_AddNewCanteen()
        {
            // Arrange
            var canteenRepositoryMock = new Mock<ICanteenRepository>();
            var newCanteen = new Canteen { Id = 4, City = "Eindhoven", Location = "LC", HotMeals = false };

            canteenRepositoryMock.Setup(repo => repo.CreateCanteen(It.IsAny<Canteen>()));

            // Act
            canteenRepositoryMock.Object.CreateCanteen(newCanteen);

            // Assert
            canteenRepositoryMock.Verify(repo => repo.CreateCanteen(newCanteen), Times.Once);
        }

        [Fact]
        public void UpdateCanteen_Should_UpdateExistingCanteen()
        {
            // Arrange
            var canteenRepositoryMock = new Mock<ICanteenRepository>();
            var canteen = new Canteen { Id = 2, City = "Utrecht", Location = "LG", HotMeals = true };

            canteenRepositoryMock.Setup(repo => repo.UpdateCanteen(canteen));

            // Act
            canteenRepositoryMock.Object.UpdateCanteen(canteen);

            // Assert
            canteenRepositoryMock.Verify(repo => repo.UpdateCanteen(canteen), Times.Once);
        }

        [Fact]
        public void DeleteCanteen_Should_RemoveCanteen()
        {
            // Arrange
            var canteenRepositoryMock = new Mock<ICanteenRepository>();
            var canteen = new Canteen { Id = 5, City = "Rotterdam", Location = "LF", HotMeals = false };

            canteenRepositoryMock.Setup(repo => repo.DeleteCanteen(canteen));

            // Act
            canteenRepositoryMock.Object.DeleteCanteen(canteen);

            // Assert
            canteenRepositoryMock.Verify(repo => repo.DeleteCanteen(canteen), Times.Once);
        }
        
    }
}
