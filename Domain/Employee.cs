using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    [Required] public int EmployeeNumber { get; set; }

    public string Email { get; set; } = null!;
    public Canteen Canteen { get; set; } = null!;

    [Required] public int CanteenId { get; set; }
}