using Domain;
using Domain.Services;
using Moq;

namespace Core.Domain.Services.Tests
{
    public class MealBoxRepositoryTests
    {
        [Fact]
        public void GetMealBoxes_Should_ReturnOnlyAvailableMealBoxes()
        {
            // Arrange
            var mealBoxRepositoryMock = new Mock<IMealBoxRepository>();
            var availableMealBoxes = new List<MealBox?>
            {
                new MealBox { Id = 1, Name = "Box1", StudentId = null },
                new MealBox { Id = 2, Name = "Box2", StudentId = null }
            };

            mealBoxRepositoryMock.Setup(repo => repo.GetMealBoxes()).Returns(availableMealBoxes);

            // Act
            var result = mealBoxRepositoryMock.Object.GetMealBoxes();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, box => Assert.Null(box?.StudentId));
        }

        [Fact]
        public void GetReservedMealBoxes_Should_ReturnOnlyReservedMealBoxesForStudent()
        {
            // Arrange
            var mealBoxRepositoryMock = new Mock<IMealBoxRepository>();
            var studentEmail = "student@avans.nl";
            var reservedMealBoxes = new List<MealBox?>
            {
                new MealBox { Id = 1, Name = "Box1", ReservedBy = new Student { Email = studentEmail } },
                new MealBox { Id = 3, Name = "Box2", ReservedBy = new Student { Email = studentEmail } }
            };

            mealBoxRepositoryMock.Setup(repo => repo.GetMealBox(studentEmail)).Returns(reservedMealBoxes);

            // Act
            var result = mealBoxRepositoryMock.Object.GetMealBox(studentEmail);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, box => Assert.Equal(studentEmail, box?.ReservedBy?.Email));
        }

        [Fact]
        public void GetMealBoxesFromCanteen_Should_ReturnMealBoxesOrderedByDate()
        {
            // Arrange
            var mealBoxRepositoryMock = new Mock<IMealBoxRepository>();
            var canteenId = 1;
            var mealBoxes = new List<MealBox?>
            {
                new MealBox { Id = 1, CanteenId = canteenId, PickUpTimeMin = DateTime.Today.AddDays(1) },
                new MealBox { Id = 2, CanteenId = canteenId, PickUpTimeMin = DateTime.Today }
            };

            mealBoxRepositoryMock.Setup(repo => repo.GetMealBoxesFromCanteen(canteenId))
                .Returns(mealBoxes.OrderBy(mb => mb?.PickUpTimeMin).ToList());


            // Act
            var result = mealBoxRepositoryMock.Object.GetMealBoxesFromCanteen(canteenId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.True(result.First()?.PickUpTimeMin < result.Last()?.PickUpTimeMin);
        }


        [Fact]
        public void CreateMealBox_Should_AddNewMealBox()
        {
            // Arrange
            var mealBoxRepositoryMock = new Mock<IMealBoxRepository>();
            var newMealBox = new MealBox { Id = 10, Name = "NewBox" };

            mealBoxRepositoryMock.Setup(repo => repo.CreateMealBox(It.IsAny<MealBox>())).Returns(newMealBox.Id);

            // Act
            var result = mealBoxRepositoryMock.Object.CreateMealBox(newMealBox);

            // Assert
            Assert.Equal(newMealBox.Id, result);
        }

        [Fact]
        public void UpdateMealBox_Should_ModifyMealBoxIfNotReserved()
        {
            // Arrange
            var mealBoxRepositoryMock = new Mock<IMealBoxRepository>();
            var updatedMealBox = new MealBox { Id = 2, Name = "UpdatedBox", StudentId = null };

            mealBoxRepositoryMock.Setup(repo => repo.UpdateMealBox(It.IsAny<MealBox>())).Returns(updatedMealBox.Id);

            // Act
            var result = mealBoxRepositoryMock.Object.UpdateMealBox(updatedMealBox);

            // Assert
            Assert.Equal(updatedMealBox.Id, result);
        }

        [Fact]
        public void DeleteMealBox_Should_RemoveMealBoxIfNotReserved()
        {
            // Arrange
            var mealBoxRepositoryMock = new Mock<IMealBoxRepository>();
            var mealBoxToDelete = new MealBox { Id = 3, Name = "DeletableBox", StudentId = null };

            mealBoxRepositoryMock.Setup(repo => repo.DeleteMealBox(mealBoxToDelete));

            // Act
            mealBoxRepositoryMock.Object.DeleteMealBox(mealBoxToDelete);

            // Assert
            mealBoxRepositoryMock.Verify(repo => repo.DeleteMealBox(mealBoxToDelete), Times.Once);
        }

        [Fact]
        public void ReserveMealBox_Should_NotAllowReservationForUnderageStudentIf18Plus()
        {
            // Arrange
            var mealBoxRepositoryMock = new Mock<IMealBoxRepository>();
            var underageStudent = new Student { Id = 1, BirthDate = DateTime.Today.AddYears(-17) };
            var adultMealBox = new MealBox { Id = 4, Name = "AdultBox", Adult = true };

            mealBoxRepositoryMock.Setup(repo => repo.ReserveMealBox(adultMealBox, underageStudent)).Returns(false);

            // Act
            var result = mealBoxRepositoryMock.Object.ReserveMealBox(adultMealBox, underageStudent);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ReserveMealBox_Should_AllowSingleReservationPerDay()
        {
            // Arrange
            var mealBoxRepositoryMock = new Mock<IMealBoxRepository>();
            var student = new Student { Id = 2, Email = "student@avans.nl" };
            var mealBox = new MealBox { Id = 5, Name = "LunchBox", PickUpTimeMin = DateTime.Today };
            var mealBoxesReservedToday = new List<MealBox?> { mealBox };

            mealBoxRepositoryMock.Setup(repo => repo.GetMealBox(student.Email)).Returns(mealBoxesReservedToday);
            mealBoxRepositoryMock.Setup(repo => repo.ReserveMealBox(mealBox, student)).Returns(false);

            // Act
            var result = mealBoxRepositoryMock.Object.ReserveMealBox(mealBox, student);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetMealBoxes_Should_FilterByLocationAndMealType()
        {
            // Arrange
            var mealBoxRepositoryMock = new Mock<IMealBoxRepository>();
            var location = "Breda";
            var mealType = "Dinner";

            var filteredMealBoxes = new List<MealBox?>
            {
                new MealBox { Id = 6, Name = "BoxA", City = location, Type = mealType },
                new MealBox { Id = 7, Name = "BoxB", City = location, Type = mealType }
            };

            mealBoxRepositoryMock.Setup(repo => repo.GetMealBoxes())
                .Returns(filteredMealBoxes.Where(mb => mb?.City == location && mb.Type == mealType).ToList());

            // Act
            var result = mealBoxRepositoryMock.Object.GetMealBoxes();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, box => Assert.Equal(location, box?.City));
            Assert.All(result, box => Assert.Equal(mealType, box?.Type));
        }

        [Fact]
        public void GetMealBoxesNotReservedByCanteenId_Should_ReturnOnlyUnreservedMealBoxesForCanteen()
        {
            // Arrange
            var mealBoxRepositoryMock = new Mock<IMealBoxRepository>();
            var canteenId = 1;
            var mealBoxes = new List<MealBox?>
            {
                new MealBox { Id = 1, Name = "Box1", CanteenId = canteenId, StudentId = null },
                new MealBox { Id = 2, Name = "Box2", CanteenId = canteenId, StudentId = null },
                new MealBox { Id = 3, Name = "Box3", CanteenId = canteenId, StudentId = 5 }
            };

            mealBoxRepositoryMock.Setup(repo => repo.GetMealBoxesNotReservedByCanteenId(canteenId))
                .Returns(mealBoxes.Where(mb => mb?.StudentId == null).ToList());

            // Act
            var result = mealBoxRepositoryMock.Object.GetMealBoxesNotReservedByCanteenId(canteenId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, box => Assert.Equal(canteenId, box?.CanteenId));
            Assert.All(result, box => Assert.Null(box?.StudentId));
        }

        [Fact]
        public void GetMealBoxesNotReservedByTypeAndCanteenId_Should_ReturnOnlyUnreservedMealBoxesOfTypeForCanteen()
        {
            // Arrange
            var mealBoxRepositoryMock = new Mock<IMealBoxRepository>();
            var canteenId = 1;
            var mealType = "Dinner";
            var mealBoxes = new List<MealBox?>
            {
                new MealBox { Id = 1, Name = "Box1", CanteenId = canteenId, Type = mealType, StudentId = null },
                new MealBox { Id = 2, Name = "Box2", CanteenId = canteenId, Type = mealType, StudentId = null },
                new MealBox { Id = 3, Name = "Box3", CanteenId = canteenId, Type = "Lunch", StudentId = null }
            };

            mealBoxRepositoryMock.Setup(repo => repo.GetMealBoxesNotReservedByTypeAndCanteenId(mealType, canteenId))
                .Returns(mealBoxes.Where(mb => mb?.StudentId == null && mb?.Type == mealType).ToList());

            // Act
            var result = mealBoxRepositoryMock.Object.GetMealBoxesNotReservedByTypeAndCanteenId(mealType, canteenId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, box => Assert.Equal(mealType, box?.Type));
            Assert.All(result, box => Assert.Null(box?.StudentId));
        }

        [Fact]
        public void GetMealBoxesDifferentCanteen_Should_ReturnMealBoxesFromOtherCanteens()
        {
            // Arrange
            var mealBoxRepositoryMock = new Mock<IMealBoxRepository>();
            var canteenId = 1;
            var mealBoxes = new List<MealBox?>
            {
                new MealBox { Id = 1, Name = "Box1", CanteenId = canteenId },
                new MealBox { Id = 2, Name = "Box2", CanteenId = 2 },
                new MealBox { Id = 3, Name = "Box3", CanteenId = 3 }
            };

            mealBoxRepositoryMock.Setup(repo => repo.GetMealBoxesDifferentCanteen(canteenId))
                .Returns(mealBoxes.Where(mb => mb?.CanteenId != canteenId).ToList());

            // Act
            var result = mealBoxRepositoryMock.Object.GetMealBoxesDifferentCanteen(canteenId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, box => Assert.NotEqual(canteenId, box?.CanteenId));
        }

        [Fact]
        public void GetAllMealBoxesOrderedByPickUpTime_Should_ReturnMealBoxesOrderedByPickUpTime()
        {
            // Arrange
            var mealBoxRepositoryMock = new Mock<IMealBoxRepository>();
            var mealBoxes = new List<MealBox?>
            {
                new MealBox { Id = 1, Name = "Box1", PickUpTimeMin = DateTime.Today.AddDays(2) },
                new MealBox { Id = 2, Name = "Box2", PickUpTimeMin = DateTime.Today },
                new MealBox { Id = 3, Name = "Box3", PickUpTimeMin = DateTime.Today.AddDays(1) }
            };

            mealBoxRepositoryMock.Setup(repo => repo.GetAllMealBoxesOrderedByPickUpTime())
                .Returns(mealBoxes.OrderBy(mb => mb?.PickUpTimeMin).ToList());

            // Act
            var result = mealBoxRepositoryMock.Object.GetAllMealBoxesOrderedByPickUpTime().ToList(); // Convert to List

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.True(result[0]?.PickUpTimeMin <= result[1]?.PickUpTimeMin);
            Assert.True(result[1]?.PickUpTimeMin <= result[2]?.PickUpTimeMin);
        }


        [Fact]
        public void ReserveMealBox_ShouldNotAllowDoubleReservationOnSameMealBox()
        {
            // Arrange
            var mealBoxRepositoryMock = new Mock<IMealBoxRepository>();
            var mealBox = new MealBox { Id = 1, Name = "LunchBox", StudentId = null };
            var student1 = new Student { Id = 1, Email = "student1@example.com" };
            var student2 = new Student { Id = 2, Email = "student2@example.com" };

            mealBoxRepositoryMock.Setup(repo => repo.ReserveMealBox(mealBox, student1)).Returns(true);
            mealBoxRepositoryMock.Setup(repo => repo.ReserveMealBox(mealBox, student2)).Returns(false);

            // Act
            var result1 = mealBoxRepositoryMock.Object.ReserveMealBox(mealBox, student1);
            var result2 = mealBoxRepositoryMock.Object.ReserveMealBox(mealBox, student2);

            // Assert
            Assert.True(result1); // First reservation should succeed
            Assert.False(result2); // Second reservation should fail
        }
    }
}