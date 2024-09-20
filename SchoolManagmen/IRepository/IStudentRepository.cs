using SchoolManagmen.Entites;

public interface IStudentRepository
{
    IQueryable<Student> GetAll();
    Task<Student> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(Student student, CancellationToken cancellationToken);
    Task UpdateAsync(Student student, CancellationToken cancellationToken);
    Task DeleteAsync(Student student, CancellationToken cancellationToken);
}
