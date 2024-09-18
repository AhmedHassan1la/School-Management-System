using SchoolManagmen.Contracts.Attendances;


namespace SchoolManagmen.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<AttendanceResponse> GetByIdAsync(int attendanceId, CancellationToken cancellationToken);
        Task<AttendanceResponse> AddAsync(AttendanceRequest request, CancellationToken cancellationToken);
        Task<AttendanceResponse> UpdateAsync(int attendanceId, AttendanceRequest request, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int attendanceId, CancellationToken cancellationToken);
        Task<IEnumerable<AttendanceResponse>> GetAttendanceByStudentIdAsync(int studentId, CancellationToken cancellationToken);
        Task MarkAttendanceBulkAsync(IEnumerable<AttendanceRequest> requests, CancellationToken cancellationToken);
        // Task<IEnumerable<AttendanceResponse>> GetAttendanceForClassByDateAsync(int classId, DateOnly date, CancellationToken cancellationToken);
        Task<IEnumerable<AttendanceResponse>> GetAbsenteesForDateAsync(DateOnly date, CancellationToken cancellationToken);

    }
}
