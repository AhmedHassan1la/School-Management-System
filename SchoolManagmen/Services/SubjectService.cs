




using SchoolManagmen.Contracts.Subjects;
using SchoolManagmen.Entites;


namespace SchoolManagmen.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext _context;

        public SubjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubjectResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var subjects = await _context.Subjects
                .Include(s => s.Teacher)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return subjects.Adapt<IEnumerable<SubjectResponse>>();
        }

        public async Task<SubjectResponse> AddAsync(SubjectRequest request, CancellationToken cancellationToken)
        {
            var subject = request.Adapt<Subject>();

            await _context.Subjects.AddAsync(subject, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var result = await _context.Subjects
                .Include(s => s.Teacher)
                .SingleOrDefaultAsync(s => s.SubjectId == subject.SubjectId, cancellationToken);

            return result.Adapt<SubjectResponse>();
        }

        public async Task<SubjectResponse> GetByIdAsync(int subjectId, CancellationToken cancellationToken)
        {
            var subject = await _context.Subjects
                .Include(s => s.Teacher)
                .SingleOrDefaultAsync(s => s.SubjectId == subjectId, cancellationToken);

            if (subject == null) return null!;

            return subject.Adapt<SubjectResponse>();
        }

        public async Task<SubjectResponse> UpdateAsync(int subjectId, SubjectRequest request, CancellationToken cancellationToken)
        {
            var subject = await _context.Subjects.SingleOrDefaultAsync(s => s.SubjectId == subjectId, cancellationToken);
            if (subject == null) return null!;

            request.Adapt(subject);

            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync(cancellationToken);

            return subject.Adapt<SubjectResponse>();
        }

        public async Task<bool> DeleteAsync(int subjectId, CancellationToken cancellationToken)
        {
            var subject = await _context.Subjects.SingleOrDefaultAsync(s => s.SubjectId == subjectId, cancellationToken);
            if (subject == null) return false;

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<SubjectResponse> AssignTeacherToSubjectAsync(int subjectId, int teacherId, CancellationToken cancellationToken)
        {
            var subject = await _context.Subjects
                .Include(s => s.Teacher)
                .SingleOrDefaultAsync(s => s.SubjectId == subjectId, cancellationToken);

            if (subject == null)
            {
                return null!;
            }

            var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.TeacherId == teacherId, cancellationToken);
            if (teacher == null)
            {
                return null!;
            }

            subject.TeacherId = teacherId;

            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync(cancellationToken);

            return subject.Adapt<SubjectResponse>();
        }
    }
}
