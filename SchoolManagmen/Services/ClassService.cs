using SchoolManagmen.Contracts.Classes;
using SchoolManagmen.Entites;

namespace SchoolManagmen.Services
{
    public class ClassService : IClassService
    {
        private readonly ApplicationDbContext _context;

        public ClassService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClassResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var classes = await _context.Classes
                .Include(c => c.Teacher)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return classes.Select(c => c.Adapt<ClassResponse>()).ToList();
        }

        public async Task<ClassResponse> GetByIdAsync(int classId, CancellationToken cancellationToken)
        {
            var classEntity = await _context.Classes
                .Include(c => c.Teacher)
                .SingleOrDefaultAsync(c => c.ClassId == classId, cancellationToken);

            if (classEntity == null)
                return null!;

            return classEntity.Adapt<ClassResponse>();
        }

        public async Task<ClassResponse> AddAsync(ClassRequest request, CancellationToken cancellationToken)
        {
            var classEntity = request.Adapt<Class>();

            await _context.Classes.AddAsync(classEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            var a = await _context.Classes.Include(x => x.Teacher).SingleAsync(c => c.ClassId == classEntity.ClassId);

            return new ClassResponse(
                a.ClassId,
                a.ClassName,
                a.GradeLevel,
                a.TeacherId,
                $"{a.Teacher.FirstName} {a.Teacher.LastName}"

                );
        }

        public async Task<ClassResponse> UpdateAsync(int classId, ClassRequest request, CancellationToken cancellationToken)
        {
            var classEntity = await _context.Classes.SingleOrDefaultAsync(c => c.ClassId == classId, cancellationToken);

            if (classEntity == null)
                return null!;

            classEntity.ClassName = request.ClassName;
            classEntity.GradeLevel = request.GradeLevel;
            classEntity.TeacherId = request.TeacherId;

            _context.Classes.Update(classEntity);
            await _context.SaveChangesAsync(cancellationToken);

            return classEntity.Adapt<ClassResponse>();
        }

        public async Task<bool> DeleteAsync(int classId, CancellationToken cancellationToken)
        {
            var classEntity = await _context.Classes.SingleOrDefaultAsync(c => c.ClassId == classId, cancellationToken);

            if (classEntity == null)
                return false;

            _context.Classes.Remove(classEntity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        //public async Task<IEnumerable<ClassResponse>> GetByGradeLevelAsync(int gradeLevel, CancellationToken cancellationToken)
        //{
        //    var grade=await _context.Classes.Where(c=>c.GradeLevel== gradeLevel)
        //          .Include(c => c.Teacher)
        //          .AsNoTracking()
        //        .ToListAsync(cancellationToken);
        //    if (grade == null)

        //        return null!;
        //    var a = grade.Select(g => new ClassResponse(
        //        g.ClassId,
        //        g.ClassName,
        //        g.GradeLevel,
        //        g.TeacherId,
        //        $"{g.Teacher.FirstName} {g.Teacher.LastName}"



        //        ));
        //    return a.ToList();
        //}

        public async Task<IEnumerable<ClassResponse>> GetByGradeLevelAsync(int gradeLevel, CancellationToken cancellationToken)
        {
            var classes = await _context.Classes
                .Include(c => c.Teacher)  // Include Teacher for mapping the TeacherName
                .Where(c => c.GradeLevel == gradeLevel)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return classes.Adapt<IEnumerable<ClassResponse>>();
        }

        public async Task<IEnumerable<ClassResponse>> SearchByNameAsync(string name, CancellationToken cancellationToken)
        {
            name = name.Trim().ToLower();
            var a = await _context.Classes.Include(t => t.Teacher).Where(c => c.ClassName.Trim().ToLower().Contains(name))
                .AsNoTracking().ToListAsync(cancellationToken);
            return a.Adapt<IEnumerable<ClassResponse>>();
        }


        public async Task<bool> IsClassExistsAsync(string className, CancellationToken cancellationToken)
        {
            // Ensure that the className is trimmed and in the right format (case-insensitive comparison)
            className = className.Trim().ToLower();

            // Query the database to check if a class with the given name exists
            var classExists = await _context.Classes
                .Include(t => t.Teacher)
                .AsNoTracking()
                .AnyAsync(c => c.ClassName.ToLower() == className, cancellationToken);

            return classExists;
        }

        public async Task<IEnumerable<ClassResponse>> GetClassesByTeacherIdAsync(int teacherId, CancellationToken cancellationToken)
        {
            var classes = await _context.Classes.Include(t => t.Teacher).Where(c => c.TeacherId == teacherId)
                .ToListAsync(cancellationToken);

            return classes.Adapt<IEnumerable<ClassResponse>>();

        }
    }
}
