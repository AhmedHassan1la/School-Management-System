
using SchoolManagmen.Contracts.Teachers;
using SchoolManagmen.Entites;


namespace SchoolManagmen.Services
{
    public class TeacherService(ApplicationDbContext context) : ITeacherService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<TeacherResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var teachers = await _context.Teachers
                .Include(t => t.Courses)
                .Include(t => t.Classes)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return teachers.Adapt<IEnumerable<TeacherResponse>>();
        }

        public async Task<TeacherResponse> AddAsync(TeacherRequest request, CancellationToken cancellationToken)
        {
            var teacher = request.Adapt<Teacher>();
            await _context.Teachers.AddAsync(teacher, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return teacher.Adapt<TeacherResponse>();
        }

        public async Task<TeacherResponse> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers
                .Include(t => t.Courses)
                .Include(t => t.Classes)
                .SingleOrDefaultAsync(t => t.TeacherId == id, cancellationToken);

            if (teacher == null)
            {
                throw new KeyNotFoundException($"Teacher with ID {id} was not found.");
            }

            return teacher.Adapt<TeacherResponse>();
        }

        public async Task<TeacherResponse> UpdateAsync(int id, TeacherRequest request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.TeacherId == id, cancellationToken);

            if (teacher == null)
            {
                throw new KeyNotFoundException($"Teacher with ID {id} was not found.");
            }

            request.Adapt(teacher);
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync(cancellationToken);
            return teacher.Adapt<TeacherResponse>();
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.TeacherId == id, cancellationToken);
            if (teacher == null) return false;

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<TeacherResponse> ToggleStatusAsync(int id, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.TeacherId == id, cancellationToken);
            if (teacher == null) return null!;

            teacher.IsActive = !teacher.IsActive;
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync(cancellationToken);
            return teacher.Adapt<TeacherResponse>();
        }

        public async Task<IEnumerable<TeacherResponse>> SearchByNameAsync(string name, CancellationToken cancellationToken)
        {
            name = name?.Trim().ToLower()!;
            var teachers = await _context.Teachers
                .Include(c => c.Classes).Include(c => c.Courses)
                .Where(t => t.FirstName.Trim().ToLower().Contains(name) || t.LastName.Trim().ToLower().Contains(name))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return teachers.Adapt<IEnumerable<TeacherResponse>>();
        }

        public async Task<IEnumerable<TeacherResponse>> GetActiveTeachersAsync(CancellationToken cancellationToken)
        {
            var activeTeachers = await _context.Teachers
                .Include(c => c.Classes).Include(c => c.Courses)
                .Where(t => t.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return activeTeachers.Adapt<IEnumerable<TeacherResponse>>();
        }

        public async Task<IEnumerable<TeacherResponse>> GetRecentlyHiredTeachersAsync(DateOnly fromDate, CancellationToken cancellationToken)
        {
            var recentlyHiredTeachers = await _context.Teachers
                .Include(c => c.Classes).Include(c => c.Courses)
                .Where(t => t.HireDate > fromDate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return recentlyHiredTeachers.Adapt<IEnumerable<TeacherResponse>>();
        }

        public async Task<IEnumerable<TeacherResponse>> GetTeachersByHireDateRangeAsync(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("End date cannot be earlier than start date.");
            }

            var teachersInRange = await _context.Teachers
                .Include(c => c.Classes).Include(c => c.Courses)
                .Where(t => t.HireDate >= startDate && t.HireDate <= endDate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return teachersInRange.Adapt<IEnumerable<TeacherResponse>>();
        }

        public async Task<TeacherStatisticsResponse> GetTeacherStatisticsAsync(CancellationToken cancellationToken)
        {
            var totalTeachers = await _context.Teachers.CountAsync(cancellationToken);

            var activeTeachers = await _context.Teachers.CountAsync(t => t.IsActive, cancellationToken);

            var inactiveTeachers = totalTeachers - activeTeachers;


            return new TeacherStatisticsResponse(
                totalTeachers,
                activeTeachers,
                 inactiveTeachers
            );
        }
        public async Task<TeacherResponse> UpdatePhoneNumberAsync(int teacherId, string phoneNumber, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers
                                .Include(c => c.Classes).Include(c => c.Courses)

                .SingleOrDefaultAsync(t => t.TeacherId == teacherId, cancellationToken);

            if (teacher == null)
            {
                throw new KeyNotFoundException("Teacher not found.");
            }
            teacher.PhoneNumber = phoneNumber;

            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync(cancellationToken);


            return teacher.Adapt<TeacherResponse>();
        }
    }
}