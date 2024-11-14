using Domain;
using Domain.Services;

namespace MealBoxAPI.Models;

public class Query
{
    private readonly IMealBoxRepository _mealBoxRepository;

    public Query(IMealBoxRepository mealBoxRepository)
    {
        _mealBoxRepository = mealBoxRepository;
    }

    public IQueryable<MealBox> MealBoxes => _mealBoxRepository.GetMealBoxes().AsQueryable()!;
}