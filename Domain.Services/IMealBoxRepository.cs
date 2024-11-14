namespace Domain.Services;

public interface IMealBoxRepository
{
    public ICollection<MealBox?> GetMealBoxes();
    public ICollection<MealBox?> GetMealBoxesNoNest();
    public ICollection<MealBox?> GetMealBoxesFromCanteen(int canteenId);
    public ICollection<MealBox?> GetMealBoxesDifferentCanteen(int canteenId);
    public ICollection<MealBox?> GetAllMealBoxesOrderedByPickUpTime();
    public ICollection<MealBox?> GetMealBoxesNotReserved();
    public ICollection<MealBox?> GetMealBoxesNotReservedByCanteenId(int canteenId);
    public ICollection<MealBox?> GetMealBoxesNotReservedByType(string type);
    public ICollection<MealBox?> GetMealBoxesNotReservedByTypeAndCanteenId(string type, int canteenId);
    public MealBox? GetMealBox(int id);
    public ICollection<MealBox?> GetMealBox(string email);
    public int CreateMealBox(MealBox mealBox);
    public int UpdateMealBox(MealBox mealBox);
    public bool ReserveMealBox(MealBox mealBox, Student student);
    public bool ReserveMealBox(MealBox mealBox, int studentId);
    public void DeleteMealBox(MealBox mealBox);
}