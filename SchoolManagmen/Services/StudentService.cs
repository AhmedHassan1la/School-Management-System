//using SchoolManagmen.Contracts.Common;
//using SchoolManagmen.Contracts.Students;
//using SchoolManagmen.Entites;

//namespace SchoolManagmen.Services
//{
//    public class StudentService(ApplicationDbContext context) : IStudentService
//    {
//        private readonly ApplicationDbContext _context = context;




//        public async Task<Result<PaginatedList<StudentResponse>>> GetAllAsyncV1(RequestFilters filters, CancellationToken cancellationToken)
//        {
//            // Fetching the students from the context
//            var studentsQuery = _context.Students.AsNoTracking();

//            // Applying search filter if SearchValue is not null or empty
//            if (!string.IsNullOrEmpty(filters.SearchValue))
//            {
//                studentsQuery = studentsQuery.Where(s =>
//                    s.FirstName.Contains(filters.SearchValue) ||
//                    s.LastName.Contains(filters.SearchValue) ||
//                    s.Email.Contains(filters.SearchValue));
//            }

//            // Projecting to StudentResponse
//            var projectedQuery = studentsQuery.ProjectToType<StudentResponse>();

//            // Creating paginated list
//            var paginatedList = await PaginatedList<StudentResponse>.CreateAsync(
//                projectedQuery,
//                filters.PageNumber,
//                filters.PageSize,
//                cancellationToken
//            );

//            return Result.Success(paginatedList);
//        }

//        public async Task<Result<PaginatedList<StudentResponseV2>>> GetAllAsyncV2(RequestFilters filters, CancellationToken cancellationToken)
//        {
//            // Fetching the students from the context
//            var studentsQuery = _context.Students.AsNoTracking();

//            // Applying search filter if SearchValue is not null or empty
//            if (!string.IsNullOrEmpty(filters.SearchValue))
//            {
//                studentsQuery = studentsQuery.Where(s =>
//                    s.FirstName.Contains(filters.SearchValue) ||
//                    s.LastName.Contains(filters.SearchValue) ||
//                    s.Email.Contains(filters.SearchValue));
//            }

//            // Projecting to StudentResponse
//            var projectedQuery = studentsQuery.ProjectToType<StudentResponseV2>();

//            // Creating paginated list
//            var paginatedList = await PaginatedList<StudentResponseV2>.CreateAsync(
//                projectedQuery,
//                filters.PageNumber,
//                filters.PageSize,
//                cancellationToken
//            );

//            return Result.Success(paginatedList);
//        }

//        public async Task<StudentResponse> AddAsync(StudentRequest request, CancellationToken cancellationToken)
//        {

//            var student = request.Adapt<Student>();

//            await _context.Students.AddAsync(student);
//            await _context.SaveChangesAsync();

//            return student.Adapt<StudentResponse>();
//        }

//        public async Task<Result<StudentResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
//        {
//            var student = await _context.Students.SingleOrDefaultAsync(s => s.StudentId == id);


//            var result = student.Adapt<StudentResponse>();
//            return result is not null ?
//                Result.Success(result) :
//                Result.Failure<StudentResponse>(StudentErrors.StudentNotFound);
//        }


//        public async Task<Result<StudentResponse>> UpdateAsync(int id, StudentRequest request, CancellationToken cancellationToken)
//        {
//            var student = await _context.Students.SingleOrDefaultAsync(s => s.StudentId == id, cancellationToken);
//            if (student == null)
//            {
//                return Result.Failure<StudentResponse>(StudentErrors.StudentNotFound);
//            }

//            request.Adapt(student);

//            _context.Students.Update(student);
//            await _context.SaveChangesAsync(cancellationToken);

//            var result = student.Adapt<StudentResponse>();
//            return Result.Success(result);
//        }


//        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
//        {
//            var student = await _context.Students.SingleOrDefaultAsync(s => s.StudentId == id, cancellationToken);
//            if (student == null)
//            {
//                return false;
//            }

//            _context.Students.Remove(student);
//            await _context.SaveChangesAsync(cancellationToken);
//            return true;
//        }

//        public async Task<IEnumerable<StudentResponse>> SearchAsync(string keyword, CancellationToken cancellationToken)
//        {
//            keyword = keyword?.Trim().ToLower()!;

//            var students = await _context.Students
//                .Where(s =>
//                    (keyword == null || EF.Functions.Like(s.FirstName.ToLower(), $"%{keyword}%")) ||
//                    (keyword == null || EF.Functions.Like(s.LastName.ToLower(), $"%{keyword}%")) ||
//                    (keyword == null || EF.Functions.Like(s.Email.ToLower(), $"%{keyword}%")) ||
//                    (keyword == null || EF.Functions.Like(s.Address.ToLower(), $"%{keyword}%")))
//                .AsNoTracking()
//                .ToListAsync(cancellationToken);

//            return students.Adapt<IEnumerable<StudentResponse>>();
//        }


//    }
//}



using SchoolManagmen.Contracts.Common;
using SchoolManagmen.Contracts.Students;
using SchoolManagmen.Entites;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Result<PaginatedList<StudentResponse>>> GetAllAsyncV1(RequestFilters filters, CancellationToken cancellationToken)
    {
        var studentsQuery = _studentRepository.GetAll();

        if (!string.IsNullOrEmpty(filters.SearchValue))
        {
            studentsQuery = studentsQuery.Where(s =>
                s.FirstName.Contains(filters.SearchValue) ||
                s.LastName.Contains(filters.SearchValue) ||
                s.Email.Contains(filters.SearchValue));
        }

        var projectedQuery = studentsQuery.ProjectToType<StudentResponse>();
        var paginatedList = await PaginatedList<StudentResponse>.CreateAsync(
            projectedQuery, filters.PageNumber, filters.PageSize, cancellationToken);

        return Result.Success(paginatedList);
    }

    public async Task<Result<PaginatedList<StudentResponseV2>>> GetAllAsyncV2(RequestFilters filters, CancellationToken cancellationToken)
    {
        var studentsQuery = _studentRepository.GetAll();

        if (!string.IsNullOrEmpty(filters.SearchValue))
        {
            studentsQuery = studentsQuery.Where(s =>
                s.FirstName.Contains(filters.SearchValue) ||
                s.LastName.Contains(filters.SearchValue) ||
                s.Email.Contains(filters.SearchValue));
        }

        var projectedQuery = studentsQuery.ProjectToType<StudentResponseV2>();
        var paginatedList = await PaginatedList<StudentResponseV2>.CreateAsync(
            projectedQuery, filters.PageNumber, filters.PageSize, cancellationToken);

        return Result.Success(paginatedList);
    }

    public async Task<StudentResponse> AddAsync(StudentRequest request, CancellationToken cancellationToken)
    {
        var student = request.Adapt<Student>();
        await _studentRepository.AddAsync(student, cancellationToken);
        return student.Adapt<StudentResponse>();
    }

    public async Task<Result<StudentResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(id, cancellationToken);
        return student != null
            ? Result.Success(student.Adapt<StudentResponse>())
            : Result.Failure<StudentResponse>(StudentErrors.StudentNotFound);
    }

    public async Task<Result<StudentResponse>> UpdateAsync(int id, StudentRequest request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(id, cancellationToken);
        if (student == null)
        {
            return Result.Failure<StudentResponse>(StudentErrors.StudentNotFound);
        }

        request.Adapt(student);
        await _studentRepository.UpdateAsync(student, cancellationToken);
        return Result.Success(student.Adapt<StudentResponse>());
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(id, cancellationToken);
        if (student == null)
        {
            return false;
        }

        await _studentRepository.DeleteAsync(student, cancellationToken);
        return true;
    }

    public async Task<IEnumerable<StudentResponse>> SearchAsync(string keyword, CancellationToken cancellationToken)
    {
        var students = await _studentRepository.GetAll()
            .Where(s =>
                EF.Functions.Like(s.FirstName.ToLower(), $"%{keyword.ToLower()}%") ||
                EF.Functions.Like(s.LastName.ToLower(), $"%{keyword.ToLower()}%") ||
                EF.Functions.Like(s.Email.ToLower(), $"%{keyword.ToLower()}%") ||
                EF.Functions.Like(s.Address.ToLower(), $"%{keyword.ToLower()}%"))
            .ToListAsync(cancellationToken);

        return students.Adapt<IEnumerable<StudentResponse>>();
    }
}




