using Domain;
using Domain.Services;

namespace Persistence;

public class MealBoxProductRepository : IMealboxProductRepository
{
    private readonly SqlServerDb _context;

    public MealBoxProductRepository(SqlServerDb context)
    {
        _context = context;
    }

    public void CreateMealBox_Product(Mealbox_Product mealboxProduct)
    {
        _context.Add(mealboxProduct);
        _context.SaveChanges();
    }

    public void UpdateMealBox_Product(Mealbox_Product mealboxProduct)
    {
        _context.MealboxProducts.Update(mealboxProduct);
        _context.SaveChanges();
    }

    public void DeleteMealBox_Product(Mealbox_Product mealboxProduct)
    {
        _context.MealboxProducts.Remove(mealboxProduct);
        _context.SaveChanges();
    }
}