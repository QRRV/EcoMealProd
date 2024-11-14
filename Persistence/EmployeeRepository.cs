using Domain;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly SqlServerDb _context;

    public EmployeeRepository(SqlServerDb context)
    {
        _context = context;
    }

    public ICollection<Employee?> GetEmployees()
    {
        return _context.Employees.Include(e => e.Canteen).ToList()!;
    }

    public Employee? GetEmployee(int id)
    {
        return _context.Employees.Include(e => e.Canteen).FirstOrDefault(e => e != null && e.Id == id);
    }

    public Employee? GetEmployee(string email)
    {
        return _context.Employees.Include(e => e.Canteen).FirstOrDefault(e => e != null && e.Email == email);
    }

    public void CreateEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public void UpdateEmployee(Employee employee)
    {
        _context.Employees.Update(employee);
        _context.SaveChanges();
    }

    public void DeleteEmployee(Employee employee)
    {
        _context.Employees.Remove(employee);
        _context.SaveChanges();
    }
}