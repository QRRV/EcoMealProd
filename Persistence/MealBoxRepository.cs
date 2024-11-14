using Domain;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class MealBoxRepository : IMealBoxRepository
{
    private readonly SqlServerDb _context;

    public MealBoxRepository(SqlServerDb context)
    {
        _context = context;
    }

    public ICollection<MealBox?> GetMealBoxes()
    {
        return _context.MealBoxes.Include(m => m.Canteen).Include(m => m.ReservedBy).Include(m => m.MealboxProducts)!
            .ThenInclude(op => op.Product).ToList()!;
    }
    
    public ICollection<MealBox?> GetMealBoxesNoNest()
    {
        return _context.MealBoxes.ToList()!;
    }
    
    public ICollection<MealBox?> GetMealBoxesFromCanteen(int canteenId)
    {
        return  GetMealBoxes()
            .Where(m => m != null && m.CanteenId == canteenId).OrderBy(m =>
            {
                if (m != null) return m.PickUpTimeMin;
                return default;
            }).ToList();
    }
    
    public ICollection<MealBox?> GetMealBoxesDifferentCanteen(int canteenId)
    {
        return GetMealBoxes().Where(m =>
            m != null && m.CanteenId != canteenId).OrderBy(m =>
        {
            if (m != null) return m.PickUpTimeMin;
            return default;
        }).ToList();
    }

    public ICollection<MealBox?> GetAllMealBoxesOrderedByPickUpTime()
    {
        return GetMealBoxes().OrderBy(m => m!.PickUpTimeMin).ToList();
    }
    
    public ICollection<MealBox?> GetMealBoxesNotReserved()
    {
        return GetMealBoxes().Where(m => m?.StudentId == null).ToList();
    }
    
    public ICollection<MealBox?> GetMealBoxesNotReservedByCanteenId(int canteenId)
    {
        return GetMealBoxesNotReserved().Where(m => m != null && m.CanteenId == canteenId).ToList();
    }
    public ICollection<MealBox?> GetMealBoxesNotReservedByType(string type)
    {
        return GetMealBoxesNotReserved().Where(m => m != null && m.Type.Equals(type)).ToList();
    }
    
    public ICollection<MealBox?> GetMealBoxesNotReservedByTypeAndCanteenId(string type, int canteenId)
    {
        return GetMealBoxesNotReservedByCanteenId(canteenId).Where(m => m != null && m.Type.Equals(type)).ToList();
    }
    
    public MealBox? GetMealBox(int id)
    {
        return _context.MealBoxes.Include(m => m.Canteen).Include(m => m.ReservedBy).Include(m => m.MealboxProducts)!
            .ThenInclude(op => op.Product).FirstOrDefault(m => m != null && m.Id == id);
    }

    public ICollection<MealBox?> GetMealBox(string email)
    {
        return _context.MealBoxes.Include(m => m.Canteen).Include(m => m.ReservedBy).Include(m => m.MealboxProducts)!
            .ThenInclude(op => op.Product).Where(m => m.ReservedBy!.Email.Equals(email)).ToList()!;
    }


    public int CreateMealBox(MealBox mealBox)
    {
        _context.Add(mealBox);
        _context.SaveChanges();
        return mealBox.Id;
    }

    public int UpdateMealBox(MealBox mealBox)
    {
        _context.MealBoxes.Update(mealBox);
        _context.SaveChanges();
        return mealBox.Id;
    }

    public bool ReserveMealBox(MealBox mealBox, Student student)
    {
        // Check if the meal box is already reserved
        if (mealBox == null || mealBox.ReservedBy != null)
        {
            return false;
        }

        // Get all meal boxes reserved by the student to check for a same-day reservation
        var studentMealBoxes = GetMealBox(student.Email);
        var hasSameDayReservation = studentMealBoxes.Any(m => m != null &&
                                                              m.PickUpTimeMin.Date == mealBox.PickUpTimeMin.Date);

        // Check if the student meets age requirements for adult meal boxes
        var isAdult = student.BirthDate.AddYears(18) <= mealBox.PickUpTimeMin;

        // Conditions to allow reservation
        if (!hasSameDayReservation && ((mealBox.Adult && isAdult) || !mealBox.Adult))
        {
            // Reserve the meal box by assigning the student's ID
            mealBox.StudentId = student.Id;
            UpdateMealBox(mealBox); // Update in the database
            return true;
        }

        return false;
    }
    
    public bool ReserveMealBox(MealBox mealBox, int studentId)
    {
        var student = _context.Students.FirstOrDefault(s => s.Id == studentId);
        return student != null && ReserveMealBox(mealBox, student);
    }

    public void DeleteMealBox(MealBox mealBox)
    {
        _context.MealBoxes.Remove(mealBox);
        _context.SaveChanges();
    }
}