namespace Domain;

public class Mealbox_Product
{
    public int Id { get; set; }
    public int MealBoxId { get; set; }
    public MealBox MealBox { get; set; } = null!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}