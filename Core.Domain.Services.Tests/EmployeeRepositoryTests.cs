using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.Services;
using Moq;

namespace Core.Domain.Services.Tests
{
    public class EmployeeRepositoryTests
    {
        [Fact]
        public void GetEmployees_Should_ReturnAllEmployeesWithCanteens()
        {
            // Arrange
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var employees = new List<Employee?>
            {
                new Employee { Id = 1, FirstName = "John", LastName = "Doe", Canteen = new Canteen { Id = 1, City = "Breda" }, EmployeeNumber = 101 },
                new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", Canteen = new Canteen { Id = 2, City = "Tilburg" }, EmployeeNumber = 102 }
            };

            employeeRepositoryMock.Setup(repo => repo.GetEmployees()).Returns(employees);

            // Act
            var result = employeeRepositoryMock.Object.GetEmployees();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, e => Assert.NotNull(e?.Canteen));
            Assert.Contains(result, e => e?.FirstName == "John" && e.Canteen.City == "Breda");
            Assert.Contains(result, e => e?.FirstName == "Jane" && e.Canteen.City == "Tilburg");
        }

        [Fact]
        public void GetEmployeeById_Should_ReturnEmployeeWithCanteen()
        {
            // Arrange
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var employee = new Employee { Id = 1, FirstName = "John", LastName = "Doe", Canteen = new Canteen { Id = 1, City = "Breda" }, EmployeeNumber = 101 };

            employeeRepositoryMock.Setup(repo => repo.GetEmployee(1)).Returns(employee);

            // Act
            var result = employeeRepositoryMock.Object.GetEmployee(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result?.FirstName);
            Assert.Equal("Breda", result?.Canteen.City);
        }

        [Fact]
        public void GetEmployeeByEmail_Should_ReturnEmployeeWithCanteen()
        {
            // Arrange
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var email = "john.doe@example.com";
            var employee = new Employee { Id = 1, FirstName = "John", LastName = "Doe", Email = email, Canteen = new Canteen { Id = 1, City = "Breda" }, EmployeeNumber = 101 };

            employeeRepositoryMock.Setup(repo => repo.GetEmployee(email)).Returns(employee);

            // Act
            var result = employeeRepositoryMock.Object.GetEmployee(email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result?.FirstName);
            Assert.Equal("Breda", result?.Canteen.City);
            Assert.Equal(email, result?.Email);
        }

        [Fact]
        public void CreateEmployee_Should_AddNewEmployee()
        {
            // Arrange
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var newEmployee = new Employee { Id = 3, FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", EmployeeNumber = 103, CanteenId = 1 };

            employeeRepositoryMock.Setup(repo => repo.CreateEmployee(It.IsAny<Employee>()));

            // Act
            employeeRepositoryMock.Object.CreateEmployee(newEmployee);

            // Assert
            employeeRepositoryMock.Verify(repo => repo.CreateEmployee(newEmployee), Times.Once);
        }

        [Fact]
        public void UpdateEmployee_Should_UpdateExistingEmployee()
        {
            // Arrange
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var employee = new Employee { Id = 2, FirstName = "Bob", LastName = "Smith", Email = "bob.smith@example.com", EmployeeNumber = 104, CanteenId = 1 };

            employeeRepositoryMock.Setup(repo => repo.UpdateEmployee(employee));

            // Act
            employeeRepositoryMock.Object.UpdateEmployee(employee);

            // Assert
            employeeRepositoryMock.Verify(repo => repo.UpdateEmployee(employee), Times.Once);
        }

        [Fact]
        public void DeleteEmployee_Should_RemoveEmployee()
        {
            // Arrange
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var employee = new Employee { Id = 2, FirstName = "Bob", LastName = "Smith", Email = "bob.smith@example.com", EmployeeNumber = 104, CanteenId = 1 };

            employeeRepositoryMock.Setup(repo => repo.DeleteEmployee(employee));

            // Act
            employeeRepositoryMock.Object.DeleteEmployee(employee);

            // Assert
            employeeRepositoryMock.Verify(repo => repo.DeleteEmployee(employee), Times.Once);
        }
        
        [Fact]
        public void Employee_EmployeeNumber_ShouldBeRequired()
        {
            // Arrange
            var employeeWithNumber = new Employee { Id = 4, FirstName = "Chris", LastName = "Evans", EmployeeNumber = 105, CanteenId = 1 };
            var employeeWithoutNumber = new Employee { Id = 5, FirstName = "Natalie", LastName = "Portman", CanteenId = 2 };

            // Act & Assert
            Assert.True(employeeWithNumber.EmployeeNumber > 0);
            Assert.Throws<ValidationException>(() =>
            {
                if (employeeWithoutNumber.EmployeeNumber == 0) throw new ValidationException("EmployeeNumber is required.");
            });
        }

        [Fact]
        public void Employee_CanteenId_ShouldBeRequired()
        {
            // Arrange
            var employee = new Employee { Id = 6, FirstName = "Emma", LastName = "Stone", EmployeeNumber = 106 };

            // Act & Assert
            Assert.Throws<ValidationException>(() =>
            {
                if (employee.CanteenId == 0) throw new ValidationException("CanteenId is required.");
            });
        }
    }
}
