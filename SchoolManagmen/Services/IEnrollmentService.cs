using SchoolManagmen.Contracts.Enrollments;

namespace SchoolManagmen.Services
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentResponse>> GetAllAsync(CancellationToken cancellationToken);

        Task<EnrollmentResponse> AddAsync(EnrollmentRequest request, CancellationToken cancellationToken);
        Task<EnrollmentResponse> GetByIdAsync(int enrollmentId, CancellationToken cancellationToken);
        Task<EnrollmentResponse> Updatesync(int enrollmentId, EnrollmentRequest request, CancellationToken cancellationToken);
        Task<IEnumerable<EnrollmentResponse>> GetEnrollmentsByStudentIdAsync(int studentId, CancellationToken cancellationToken);
        Task<IEnumerable<EnrollmentResponse>> GetEnrollmentsByCourseIdAsync(int courseId, CancellationToken cancellationToken);
        Task<IEnumerable<EnrollmentResponse>> GetTopPerformingStudentsByCourseIdAsync(int courseId, int topN, CancellationToken cancellationToken);

        Task<IEnumerable<EnrollmentResponse>> GetCoursesForStudentAsync(int studentId, CancellationToken cancellationToken);
        Task<IEnumerable<EnrollmentResponse>> EnrollMultipleStudentsAsync(IEnumerable<EnrollmentRequest> requests, CancellationToken cancellationToken);

    }
}
