using SchoolManagmen.Contracts.Courses;

namespace SchoolManagmen.Services
{
    public interface ICourseService
    {
        // Get all courses
        Task<IEnumerable<CourseResponse>> GetAllAsync(CancellationToken cancellationToken);

        // Add a new course
        Task<CourseResponse> AddAsync(CourseRequest request, CancellationToken cancellationToken);

        // Get a course by id
        Task<CourseResponse> GetByIdAsync(int id, CancellationToken cancellationToken);

        // Update a course by id
        Task<CourseResponse> UpdateAsync(int id, CourseRequest request, CancellationToken cancellationToken);

        // Delete a course by id
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<CourseResponse>> GetCoursesByTeacherIdAsync(int teacherId, CancellationToken cancellationToken);
        Task<IEnumerable<CourseResponse>> SearchCoursesByNameAsync(string courseName, CancellationToken cancellationToken);
        Task<IEnumerable<CourseResponse>> GetCoursesByCreditsRangeAsync(int minCredits, int maxCredits, CancellationToken cancellationToken);
        // Task<bool> EnrollStudentInCourseAsync(int courseId, int studentId, CancellationToken cancellationToken);
        // Task<IEnumerable<StudentResponse>> GetStudentsInCourseAsync(int courseId, CancellationToken cancellationToken);
        //Task<CourseStatisticsResponse> GetCourseStatisticsAsync(int courseId, CancellationToken cancellationToken);

        Task<CourseResponse> UpdateCourseTeacherAsync(int courseId, int teacherId, CancellationToken cancellationToken);

    }
}
