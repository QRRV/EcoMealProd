using Domain;
using Domain.Services;

namespace Persistence;

public class StudentRepository : IStudentRepository
{
    private readonly SqlServerDb _context;

    public StudentRepository(SqlServerDb context)
    {
        _context = context;
    }

    public ICollection<Student?> GetStudent()
    {
        return _context.Students.ToList()!;
    }

    public Student? GetStudent(int id)
    {
        return _context.Students.FirstOrDefault(s => s.Id == id);
    }

    public Student? GetStudent(string email)
    {
        return _context.Students.FirstOrDefault(s => s.Email == email);
    }


    public void CreateStudent(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
    }

    public void UpdateStudent(Student student)
    {
        _context.Students.Update(student);
        _context.SaveChanges();
    }

    public void DeleteStudent(Student student)
    {
        _context.Students.Remove(student);
        _context.SaveChanges();
    }
}