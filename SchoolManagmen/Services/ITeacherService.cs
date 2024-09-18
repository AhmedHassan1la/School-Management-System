using SchoolManagmen.Contracts.Teachers;

namespace SchoolManagmen.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<TeacherResponse> AddAsync(TeacherRequest request, CancellationToken cancellationToken);
        Task<TeacherResponse> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<TeacherResponse> UpdateAsync(int id, TeacherRequest request, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<TeacherResponse> ToggleStatusAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<TeacherResponse>> SearchByNameAsync(string name, CancellationToken cancellationToken);
        Task<IEnumerable<TeacherResponse>> GetActiveTeachersAsync(CancellationToken cancellationToken);
        Task<IEnumerable<TeacherResponse>> GetRecentlyHiredTeachersAsync(DateOnly fromDate, CancellationToken cancellationToken);
        Task<IEnumerable<TeacherResponse>> GetTeachersByHireDateRangeAsync(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken);

        Task<TeacherStatisticsResponse> GetTeacherStatisticsAsync(CancellationToken cancellationToken);

        Task<TeacherResponse> UpdatePhoneNumberAsync(int teacherId, string phoneNumber, CancellationToken cancellationToken);


    }
}
