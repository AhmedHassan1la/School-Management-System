using SchoolManagmen.Contracts.GradeReports;
using SchoolManagmen.Entites;

namespace SchoolManagmen.Services
{

    public class GradeReportService : IGradeReportService
    {
        private readonly ApplicationDbContext _context;

        public GradeReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GradeReportResponse> AddAsync(GradeReportRequest request, CancellationToken cancellationToken)
        {
            var gradeReport = request.Adapt<GradeReport>();

            await _context.GradeReports.AddAsync(gradeReport, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var result = await _context.GradeReports
                .Include(gr => gr.Student)
                .Include(gr => gr.Course)
                .SingleOrDefaultAsync(gr => gr.GradeReportId == gradeReport.GradeReportId, cancellationToken);

            return result.Adapt<GradeReportResponse>();
        }

        public async Task<GradeReportResponse> GetByIdAsync(int reportId, CancellationToken cancellationToken)
        {
            var gradeReport = await _context.GradeReports
                .Include(gr => gr.Student)
                .Include(gr => gr.Course)
                .SingleOrDefaultAsync(gr => gr.GradeReportId == reportId, cancellationToken);

            if (gradeReport == null)
            {
                return null!;
            }

            return gradeReport.Adapt<GradeReportResponse>();
        }

        public async Task<IEnumerable<GradeReportResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var gradeReports = await _context.GradeReports
                .Include(gr => gr.Student)
                .Include(gr => gr.Course)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return gradeReports.Adapt<IEnumerable<GradeReportResponse>>();
        }

        public async Task<bool> DeleteAsync(int reportId, CancellationToken cancellationToken)
        {
            var gradeReport = await _context.GradeReports.SingleOrDefaultAsync(g => g.GradeReportId == reportId, cancellationToken);

            if (gradeReport == null)
            {
                return false;
            }

            _context.GradeReports.Remove(gradeReport);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<GradeReportResponse> UpdateAsync(int reportId, GradeReportRequest request, CancellationToken cancellationToken)
        {
            var gradeReport = await _context.GradeReports
                .SingleOrDefaultAsync(gr => gr.GradeReportId == reportId, cancellationToken);

            if (gradeReport == null)
            {
                return null!;
            }

            request.Adapt(gradeReport);

            _context.GradeReports.Update(gradeReport);
            await _context.SaveChangesAsync(cancellationToken);

            return gradeReport.Adapt<GradeReportResponse>();
        }

        public async Task<IEnumerable<GradeReportResponse>> GetGradeReportsByStudentIdAsync(int studentId, CancellationToken cancellationToken)
        {
            var gradeReports = await _context.GradeReports
                .Where(gr => gr.StudentId == studentId)
                .Include(gr => gr.Student)
                .Include(gr => gr.Course)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return gradeReports.Adapt<IEnumerable<GradeReportResponse>>();
        }

        public async Task<IEnumerable<GradeReportResponse>> GetGradeReportsByCourseIdAsync(int courseId, CancellationToken cancellationToken)
        {
            var gradeReports = await _context.GradeReports
                .Include(gr => gr.Student)
                .Include(gr => gr.Course)
                .Where(gr => gr.CourseId == courseId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return gradeReports.Adapt<IEnumerable<GradeReportResponse>>();
        }

        public async Task<decimal> GetAverageGradeByCourseIdAsync(int courseId, CancellationToken cancellationToken)
        {
            var averageGrade = await _context.GradeReports
                .Where(gr => gr.CourseId == courseId)
                .AverageAsync(gr => gr.Grade, cancellationToken);

            return averageGrade;
        }

        public async Task<IEnumerable<GradeReportResponse>> GetTopPerformingStudentsInCourseAsync(int courseId, int topN, CancellationToken cancellationToken)
        {
            var topStudents = await _context.GradeReports
                .Include(gr => gr.Student)
                .Include(gr => gr.Course)
                .Where(gr => gr.CourseId == courseId)
                .OrderByDescending(gr => gr.Grade)
                .Take(topN)
                .ToListAsync(cancellationToken);

            return topStudents.Adapt<IEnumerable<GradeReportResponse>>();
        }

        public async Task<decimal> GetOverallAverageGradeAsync(CancellationToken cancellationToken)
        {
            var averageGrade = await _context.GradeReports
                .AsNoTracking()
                .AverageAsync(gr => gr.Grade, cancellationToken);

            return averageGrade;
        }
    }
}
