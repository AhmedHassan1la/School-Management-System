using SchoolManagmen.Contracts.Common;
using SchoolManagmen.Contracts.Students;

namespace SchoolManagmen.Services
{
    public interface IStudentService
    {
        Task<Result<PaginatedList<StudentResponse>>> GetAllAsyncV1(RequestFilters filters, CancellationToken cancellationToken);
        Task<Result<PaginatedList<StudentResponseV2>>> GetAllAsyncV2(RequestFilters filters, CancellationToken cancellationToken);
        Task<StudentResponse> AddAsync(StudentRequest request, CancellationToken cancellationToken);
        Task<Result<StudentResponse>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<StudentResponse>> UpdateAsync(int id, StudentRequest request, CancellationToken cancellationToken);
        Task<IEnumerable<StudentResponse>> SearchAsync(string keyword, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);


    }
}
