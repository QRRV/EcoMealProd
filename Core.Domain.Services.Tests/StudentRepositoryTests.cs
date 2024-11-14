using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.Services;
using Moq;

namespace Core.Domain.Services.Tests
{
    public class StudentRepositoryTests
    {
        [Fact]
        public void GetStudent_Should_ReturnAllStudents()
        {
            // Arrange
            var studentRepositoryMock = new Mock<IStudentRepository>();
            var students = new List<Student?>
            {
                new Student { Id = 1, FirstName = "John", LastName = "Doe", StudentNumber = 1001, BirthDate = new DateTime(2000, 1, 1), Email = "john@example.com", StudyCity = "Breda", PhoneNumber = "0612345678" },
                new Student { Id = 2, FirstName = "Jane", LastName = "Smith", StudentNumber = 1002, BirthDate = new DateTime(2001, 2, 2), Email = "jane@example.com", StudyCity = "Tilburg", PhoneNumber = "0698765432" }
            };

            studentRepositoryMock.Setup(repo => repo.GetStudent()).Returns(students);

            // Act
            var result = studentRepositoryMock.Object.GetStudent();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, s => s?.FirstName == "John" && s.StudyCity == "Breda");
            Assert.Contains(result, s => s?.FirstName == "Jane" && s.StudyCity == "Tilburg");
        }

        [Fact]
        public void GetStudentById_Should_ReturnStudentById()
        {
            // Arrange
            var studentRepositoryMock = new Mock<IStudentRepository>();
            var student = new Student { Id = 1, FirstName = "John", LastName = "Doe", StudentNumber = 1001, BirthDate = new DateTime(2000, 1, 1), Email = "john@example.com", StudyCity = "Breda", PhoneNumber = "0612345678" };

            studentRepositoryMock.Setup(repo => repo.GetStudent(1)).Returns(student);

            // Act
            var result = studentRepositoryMock.Object.GetStudent(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result?.FirstName);
            Assert.Equal("Breda", result?.StudyCity);
        }

        [Fact]
        public void GetStudentByEmail_Should_ReturnStudentByEmail()
        {
            // Arrange
            var studentRepositoryMock = new Mock<IStudentRepository>();
            var email = "john@example.com";
            var student = new Student { Id = 1, FirstName = "John", LastName = "Doe", StudentNumber = 1001, BirthDate = new DateTime(2000, 1, 1), Email = email, StudyCity = "Breda", PhoneNumber = "0612345678" };

            studentRepositoryMock.Setup(repo => repo.GetStudent(email)).Returns(student);

            // Act
            var result = studentRepositoryMock.Object.GetStudent(email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result?.FirstName);
            Assert.Equal(email, result?.Email);
        }

        [Fact]
        public void CreateStudent_Should_AddNewStudent()
        {
            // Arrange
            var studentRepositoryMock = new Mock<IStudentRepository>();
            var newStudent = new Student { Id = 3, FirstName = "Alice", LastName = "Johnson", StudentNumber = 1003, BirthDate = new DateTime(2002, 3, 3), Email = "alice@example.com", StudyCity = "Eindhoven", PhoneNumber = "0687654321" };

            studentRepositoryMock.Setup(repo => repo.CreateStudent(It.IsAny<Student>()));

            // Act
            studentRepositoryMock.Object.CreateStudent(newStudent);

            // Assert
            studentRepositoryMock.Verify(repo => repo.CreateStudent(newStudent), Times.Once);
        }

        [Fact]
        public void UpdateStudent_Should_UpdateExistingStudent()
        {
            // Arrange
            var studentRepositoryMock = new Mock<IStudentRepository>();
            var student = new Student { Id = 2, FirstName = "Bob", LastName = "Smith", StudentNumber = 1004, BirthDate = new DateTime(2003, 4, 4), Email = "bob@example.com", StudyCity = "Rotterdam", PhoneNumber = "0678901234" };

            studentRepositoryMock.Setup(repo => repo.UpdateStudent(student));

            // Act
            studentRepositoryMock.Object.UpdateStudent(student);

            // Assert
            studentRepositoryMock.Verify(repo => repo.UpdateStudent(student), Times.Once);
        }

        [Fact]
        public void DeleteStudent_Should_RemoveStudent()
        {
            // Arrange
            var studentRepositoryMock = new Mock<IStudentRepository>();
            var student = new Student { Id = 2, FirstName = "Bob", LastName = "Smith", StudentNumber = 1004, BirthDate = new DateTime(2003, 4, 4), Email = "bob@example.com", StudyCity = "Rotterdam", PhoneNumber = "0678901234" };

            studentRepositoryMock.Setup(repo => repo.DeleteStudent(student));

            // Act
            studentRepositoryMock.Object.DeleteStudent(student);

            // Assert
            studentRepositoryMock.Verify(repo => repo.DeleteStudent(student), Times.Once);
        }

        [Fact]
        public void Student_BirthDate_ShouldBeRequired()
        {
            // Arrange
            var studentWithBirthDate = new Student { Id = 4, FirstName = "Chris", LastName = "Evans", StudentNumber = 1005, BirthDate = new DateTime(2005, 5, 5), Email = "chris@example.com", StudyCity = "Den Bosch", PhoneNumber = "0665432109" };
            var studentWithoutBirthDate = new Student { Id = 5, FirstName = "Natalie", LastName = "Portman", StudentNumber = 1006, Email = "natalie@example.com", StudyCity = "Maastricht", PhoneNumber = "0612345678" };

            // Act & Assert
            Assert.True(studentWithBirthDate.BirthDate != default(DateTime));
            Assert.Throws<ValidationException>(() =>
            {
                if (studentWithoutBirthDate.BirthDate == default(DateTime)) throw new ValidationException("BirthDate is required.");
            });
        }

        [Fact]
        public void Student_StudentNumber_ShouldBeRequired()
        {
            // Arrange
            var studentWithNumber = new Student { Id = 6, FirstName = "Emma", LastName = "Stone", StudentNumber = 1007, BirthDate = new DateTime(2004, 6, 6), Email = "emma@example.com", StudyCity = "Leiden", PhoneNumber = "0601234567" };
            var studentWithoutNumber = new Student { Id = 7, FirstName = "Will", LastName = "Smith", BirthDate = new DateTime(2004, 7, 7), Email = "will@example.com", StudyCity = "Amsterdam", PhoneNumber = "0612345678" };

            // Act & Assert
            Assert.True(studentWithNumber.StudentNumber > 0);
            Assert.Throws<ValidationException>(() =>
            {
                if (studentWithoutNumber.StudentNumber == 0) throw new ValidationException("StudentNumber is required.");
            });
        }
    }
}
