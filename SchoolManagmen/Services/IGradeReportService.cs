using SchoolManagmen.Contracts.GradeReports;

namespace SchoolManagmen.Services
{
    public interface IGradeReportService
    {
        Task<IEnumerable<GradeReportResponse>> GetAllAsync(CancellationToken cancellationToken);

        Task<GradeReportResponse> GetByIdAsync(int reportId, CancellationToken cancellationToken);

        Task<GradeReportResponse> AddAsync(GradeReportRequest request, CancellationToken cancellationToken);

        Task<GradeReportResponse> UpdateAsync(int reportId, GradeReportRequest request, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int reportId, CancellationToken cancellationToken);
        Task<IEnumerable<GradeReportResponse>> GetGradeReportsByStudentIdAsync(int studentId, CancellationToken cancellationToken);
        Task<IEnumerable<GradeReportResponse>> GetGradeReportsByCourseIdAsync(int courseId, CancellationToken cancellationToken);
        Task<decimal> GetAverageGradeByCourseIdAsync(int courseId, CancellationToken cancellationToken);
        Task<IEnumerable<GradeReportResponse>> GetTopPerformingStudentsInCourseAsync(int courseId, int topN, CancellationToken cancellationToken);
        Task<decimal> GetOverallAverageGradeAsync(CancellationToken cancellationToken);


    }
}
