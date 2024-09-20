

using SchoolManagmen.Entites;
using SchoolManagmen;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Student> GetAll()
    {
        return _context.Students.AsNoTracking();
    }

    public async Task<Student> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Students.SingleOrDefaultAsync(s => s.StudentId == id, cancellationToken);
    }

    public async Task AddAsync(Student student, CancellationToken cancellationToken)
    {
        await _context.Students.AddAsync(student, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Student student, CancellationToken cancellationToken)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Student student, CancellationToken cancellationToken)
    {
        _context.Students.Remove(student);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
 

