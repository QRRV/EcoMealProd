namespace Domain.Services;

public interface IMealboxProductRepository
{
    public void CreateMealBox_Product(Mealbox_Product mealboxProduct);
    public void UpdateMealBox_Product(Mealbox_Product mealboxProduct);
    public void DeleteMealBox_Product(Mealbox_Product mealboxProduct);
}