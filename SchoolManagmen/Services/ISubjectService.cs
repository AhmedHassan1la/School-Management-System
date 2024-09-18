using SchoolManagmen.Contracts.Subjects;

namespace SchoolManagmen.Services
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<SubjectResponse> AddAsync(SubjectRequest request, CancellationToken cancellationToken);
        Task<SubjectResponse> GetByIdAsync(int subjectId, CancellationToken cancellationToken);
        Task<SubjectResponse> UpdateAsync(int subjectId, SubjectRequest request, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int subjectId, CancellationToken cancellationToken);
        Task<SubjectResponse> AssignTeacherToSubjectAsync(int subjectId, int teacherId, CancellationToken cancellationToken);

    }
}
