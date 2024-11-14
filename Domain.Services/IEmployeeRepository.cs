namespace Domain.Services;

public interface IEmployeeRepository
{
    public ICollection<Employee?> GetEmployees();
    public Employee? GetEmployee(int id);
    public Employee? GetEmployee(string email);
    public void CreateEmployee(Employee employee);
    public void UpdateEmployee(Employee employee);
    public void DeleteEmployee(Employee employee);
}