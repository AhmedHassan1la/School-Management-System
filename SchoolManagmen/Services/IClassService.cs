using SchoolManagmen.Contracts.Classes;

namespace SchoolManagmen.Services
{
    public interface IClassService
    {
        Task<IEnumerable<ClassResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<ClassResponse> GetByIdAsync(int classId, CancellationToken cancellationToken);
        Task<ClassResponse> AddAsync(ClassRequest request, CancellationToken cancellationToken);
        Task<ClassResponse> UpdateAsync(int classId, ClassRequest request, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int classId, CancellationToken cancellationToken);
        Task<IEnumerable<ClassResponse>> GetByGradeLevelAsync(int gradeLevel, CancellationToken cancellationToken);
        Task<IEnumerable<ClassResponse>> SearchByNameAsync(string name, CancellationToken cancellationToken);
        Task<bool> IsClassExistsAsync(string className, CancellationToken cancellationToken);
        Task<IEnumerable<ClassResponse>> GetClassesByTeacherIdAsync(int teacherId, CancellationToken cancellationToken);



    }
}
