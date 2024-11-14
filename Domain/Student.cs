using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    [Required] public DateTime BirthDate { get; set; }

    [Required] public int StudentNumber { get; set; }

    public string Email { get; set; } = null!;
    public string StudyCity { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}