using System.ComponentModel.DataAnnotations;

namespace Domain;

public class MealBox
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Mealbox_Product>? MealboxProducts { get; set; }
    public string City { get; set; } = null!;
    public Canteen Canteen { get; set; } = null!;

    [Required] public int CanteenId { get; set; }

    [Required] public DateTime PickUpTimeMin { get; set; }

    public DateTime PickUpTimeMax { get; set; }

    [Required] public bool Adult { get; set; }

    [Required] public decimal Price { get; set; }

    public string Type { get; set; } = null!;
    public Student? ReservedBy { get; set; }
    public int? StudentId { get; set; }
}