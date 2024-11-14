using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Canteen
{
    public int Id { get; set; }
    public string City { get; set; } = null!;
    public string Location { get; set; } = null!;
    [Required] public bool HotMeals { get; set; }
}   