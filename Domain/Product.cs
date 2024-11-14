using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    [Required] public bool ContainsAlcohol { get; set; }

    public ICollection<Mealbox_Product> MealboxProducts { get; set; } = null!;

    [StringLength(200)]

    public string ImageLink { get; set; } = null!;
}