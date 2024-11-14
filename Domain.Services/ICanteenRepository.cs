namespace Domain.Services;

public interface ICanteenRepository
{
    public ICollection<Canteen?> GetCanteens();
    public Canteen? GetCanteen(int id);
    public Canteen? GetStudentCanteen(string studentCity);
    public void CreateCanteen(Canteen? canteen);
    public void UpdateCanteen(Canteen canteen);
    public void DeleteCanteen(Canteen canteen);
}