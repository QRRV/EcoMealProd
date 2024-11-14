using Domain;
using Domain.Services;

namespace Persistence;

public class CanteenRepository : ICanteenRepository
{
    private readonly SqlServerDb _context;

    public CanteenRepository(SqlServerDb context)
    {
        _context = context;
    }

    public ICollection<Canteen?> GetCanteens()
    {
        return _context.Canteens.ToList()!;
    }

    public Canteen? GetCanteen(int id)
    {
        return _context.Canteens.FirstOrDefault(c => c != null && c.Id == id);
    }
    public Canteen? GetStudentCanteen(string studentCity)
    {
        return _context.Canteens.FirstOrDefault(c => c != null && c.City == studentCity);
    }

    public void CreateCanteen(Canteen? canteen)
    {
        if (canteen != null) _context.Canteens.Add(canteen);
        _context.SaveChanges();
    }

    public void UpdateCanteen(Canteen canteen)
    {
        _context.Canteens.Update(canteen);
        _context.SaveChanges();
    }

    public void DeleteCanteen(Canteen canteen)
    {
        _context.Canteens.Remove(canteen);
        _context.SaveChanges();
    }
}