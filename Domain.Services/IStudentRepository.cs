namespace Domain.Services;

public interface IStudentRepository
{
    public ICollection<Student?> GetStudent();
    public Student? GetStudent(int id);
    public Student? GetStudent(string email);

    public void CreateStudent(Student student);
    public void UpdateStudent(Student student);
    public void DeleteStudent(Student student);
}