//using SchoolManagmen.Contracts.Enrollments;
//using SchoolManagmen.Entites;

//namespace SchoolManagmen.Services
//{
//    public class EnrollmentService(ApplicationDbContext context) : IEnrollmentService
//    {
//        private readonly ApplicationDbContext _context = context;

//        public async Task<EnrollmentResponse> AddAsync(EnrollmentRequest request, CancellationToken cancellationToken)
//        {
//            var enrollment = request.Adapt<Enrollment>();

//            await _context.Enrollments.AddAsync(enrollment, cancellationToken);
//            await _context.SaveChangesAsync(cancellationToken);

//            var result = await _context.Enrollments
//                .Include(e => e.Student)
//                .Include(e => e.Course)
//                .SingleOrDefaultAsync(e => e.EnrollmentId == enrollment.EnrollmentId, cancellationToken);

//            return new EnrollmentResponse(
//                result!.EnrollmentId,
//                result.StudentId,
//                result.CourseId,
//                $"{result.Student.FirstName} {result.Student.LastName}",
//                result.Course.CourseName,
//                result.EnrollmentDate,
//                result.Grade
//            );
//        }



//        public async Task<EnrollmentResponse> GetByIdAsync(int enrollmentId, CancellationToken cancellationToken)
//        {
//            var enrollment = await _context.Enrollments
//                .Include(s => s.Student)
//                .Include(c => c.Course)
//                .Where(e => e.EnrollmentId == enrollmentId)
//                .SingleOrDefaultAsync(cancellationToken);

//            if (enrollment == null)
//            {
//                return null!;
//            }
//            return new EnrollmentResponse(
//                enrollment.EnrollmentId,
//                enrollment.StudentId,
//                enrollment.CourseId,
//            $"{enrollment.Student.FirstName} {enrollment.Student.LastName}",
//            enrollment.Course.CourseName,
//            enrollment.EnrollmentDate,
//            enrollment.Grade




//                );
//        }

//        public async Task<IEnumerable<EnrollmentResponse>> GetAllAsync(CancellationToken cancellationToken)
//        {
//            var enrollments = await _context.Enrollments.
//                Include(s => s.Student).
//                Include(c => c.Course)
//                .AsNoTracking().ToListAsync(cancellationToken);
//            var enrollmentsresponse = enrollments.Select(e => new EnrollmentResponse(
//                e.EnrollmentId,
//                e.StudentId,
//                e.CourseId,
//                $"{e.Student.FirstName} {e.Student.LastName}",
//                e.Course.CourseName,
//                e.EnrollmentDate,
//                e.Grade

//                )).ToList();
//            return enrollmentsresponse!;

//        }

//        public async Task<EnrollmentResponse> Updatesync(int enrollmentId, EnrollmentRequest request, CancellationToken cancellationToken)
//        {
//            var enrollupdate = await _context.Enrollments.SingleOrDefaultAsync(e => e.EnrollmentId == enrollmentId);
//            if (enrollupdate is null)
//            {
//                return null!;
//            }
//            enrollupdate.StudentId = request.StudentId;
//            enrollupdate.CourseId = request.CourseId;
//            enrollupdate.EnrollmentDate = request.EnrollmentDate;
//            enrollupdate.Grade = request.Grade;
//            _context.Update(enrollupdate);
//            await _context.SaveChangesAsync(cancellationToken);

//            var result = await _context.Enrollments.Include(s => s.Student).Include(c => c.Course)
//                .Where(e => e.EnrollmentId == enrollmentId).SingleOrDefaultAsync(cancellationToken);

//            return new EnrollmentResponse(
//                  result!.EnrollmentId,
//                  result.StudentId,
//                  result.CourseId,
//              $"{result.Student.FirstName} {result.Student.LastName}",
//              result.Course.CourseName,
//              result.EnrollmentDate,
//              result.Grade



//                  );

//        }


//        public async Task<IEnumerable<EnrollmentResponse>> GetEnrollmentsByStudentIdAsync(int studentId, CancellationToken cancellationToken)
//        {
//            // جلب جميع التسجيلات المرتبطة بالطالب المحدد
//            var enrollments = await _context.Enrollments
//                .Include(e => e.Student)
//                .Include(e => e.Course)
//                .Where(e => e.StudentId == studentId)
//                .AsNoTracking()
//                .ToListAsync(cancellationToken);

//            var enrollmentResponses = enrollments.Select(e => new EnrollmentResponse(
//                e.EnrollmentId,
//                e.StudentId,
//                e.CourseId,
//                $"{e.Student.FirstName} {e.Student.LastName}",
//                e.Course.CourseName,
//                e.EnrollmentDate,
//                e.Grade
//            )).ToList();

//            return enrollmentResponses;
//        }

//        public async Task<IEnumerable<EnrollmentResponse>> GetEnrollmentsByCourseIdAsync(int courseId, CancellationToken cancellationToken)
//        {
//            var enrollments = await _context.Enrollments
//                .Include(e => e.Student)
//                .Include(e => e.Course)
//                .Where(e => e.CourseId == courseId)
//                .AsNoTracking()
//                .ToListAsync(cancellationToken);

//            return enrollments.Adapt<IEnumerable<EnrollmentResponse>>();
//            //var enrollmentResponses = enrollments.Select(e => new EnrollmentResponse(
//            //    e.EnrollmentId,
//            //    e.StudentId,
//            //    e.CourseId,
//            //    $"{e.Student.FirstName} {e.Student.LastName}",
//            //    e.Course.CourseName,
//            //    e.EnrollmentDate,
//            //    e.Grade
//            //)).ToList();

//            //return enrollmentResponses;
//        }
//        public async Task<IEnumerable<EnrollmentResponse>> GetTopPerformingStudentsByCourseIdAsync(int courseId, int topN, CancellationToken cancellationToken)
//        {
//            var enrollments = await _context.Enrollments
//                .Include(e => e.Student)
//                .Include(e => e.Course)
//                .Where(e => e.CourseId == courseId)
//                .OrderByDescending(e => e.Grade)
//                .Take(topN)
//                .AsNoTracking()
//                .ToListAsync(cancellationToken);

//            var enrollmentResponses = enrollments.Select(e => new EnrollmentResponse(
//                e.EnrollmentId,
//                e.StudentId,
//                e.CourseId,
//                $"{e.Student.FirstName} {e.Student.LastName}",
//                e.Course.CourseName,
//                e.EnrollmentDate,
//                e.Grade
//            )).ToList();


//            return enrollmentResponses;
//        }
//        public async Task<IEnumerable<EnrollmentResponse>> GetCoursesForStudentAsync(int studentId, CancellationToken cancellationToken)
//        {
//            // جلب جميع التسجيلات الخاصة بالطالب
//            var enrollments = await _context.Enrollments
//                .Include(e => e.Course)
//                .Include(e => e.Student)
//                .Where(e => e.StudentId == studentId)
//                .AsNoTracking() //     
//                .ToListAsync(cancellationToken); // تحويل النتائج إلى قائمة

//            // تحويل النتائج إلى استجابات EnrollmentResponse
//            var enrollmentResponses = enrollments.Select(e => new EnrollmentResponse(
//                e.EnrollmentId,
//                e.StudentId,
//                e.CourseId,
//                $"{e.Student.FirstName} {e.Student.LastName}", // دمج الاسم الأول والأخير للطالب
//                e.Course.CourseName, // اسم الدورة
//                e.EnrollmentDate,
//                e.Grade // إظهار الدرجة
//            )).ToList();

//            // إرجاع النتائج
//            return enrollmentResponses;
//        }
//        public async Task<IEnumerable<EnrollmentResponse>> EnrollMultipleStudentsAsync(IEnumerable<EnrollmentRequest> requests, CancellationToken cancellationToken)
//        {
//            var enrollments = new List<Enrollment>();

//            foreach (var request in requests)
//            {
//                // تحقق مما إذا كان الطالب مسجل بالفعل في الدورة لتجنب التكرار
//                var existingEnrollment = await _context.Enrollments
//                    .Include(e => e.Student)
//                    .Include(e => e.Course)
//                    .Where(e => e.StudentId == request.StudentId && e.CourseId == request.CourseId)
//                    .ToListAsync(cancellationToken);

//                if (existingEnrollment is not null)
//                {
//                    // إذا لم يكن الطالب مسجل بالفعل، نقوم بإضافة التسجيل الجديد
//                    var enrollment = request.Adapt<Enrollment>();
//                    enrollments.Add(enrollment);
//                }
//            }

//            // إضافة جميع التسجيلات الجديدة دفعة واحدة إلى قاعدة البيانات
//            if (enrollments.Any())
//            {
//                await _context.Enrollments.AddRangeAsync(enrollments, cancellationToken);
//                await _context.SaveChangesAsync(cancellationToken);
//            }

//            var enrollmentResponses = enrollments.Select(e => new EnrollmentResponse(
//                e.EnrollmentId,
//                e.StudentId,
//                e.CourseId,
//                $"{e.Student.FirstName} {e.Student.LastName}",
//                e.Course.CourseName,
//                e.EnrollmentDate,
//                e.Grade
//            )).ToList();

//            return enrollmentResponses;
//        }



//    }
//}









using SchoolManagmen.Contracts.Enrollments;
using SchoolManagmen.Entites;


namespace SchoolManagmen.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EnrollmentResponse> AddAsync(EnrollmentRequest request, CancellationToken cancellationToken)
        {
            // تحويل EnrollmentRequest إلى كيان Enrollment باستخدام Mapster
            var enrollment = request.Adapt<Enrollment>();

            await _context.Enrollments.AddAsync(enrollment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // الحصول على التسجيل مع المعلم والدورة التعليمية المرتبطة به
            var result = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .SingleOrDefaultAsync(e => e.EnrollmentId == enrollment.EnrollmentId, cancellationToken);

            // تحويل الكيان إلى EnrollmentResponse باستخدام Mapster
            return result.Adapt<EnrollmentResponse>();
        }

        public async Task<EnrollmentResponse> GetByIdAsync(int enrollmentId, CancellationToken cancellationToken)
        {
            var enrollment = await _context.Enrollments
                .Include(s => s.Student)
                .Include(c => c.Course)
                .SingleOrDefaultAsync(e => e.EnrollmentId == enrollmentId, cancellationToken);

            if (enrollment == null)
            {
                return null!;
            }

            // تحويل الكيان إلى استجابة باستخدام Mapster
            return enrollment.Adapt<EnrollmentResponse>();
        }

        public async Task<IEnumerable<EnrollmentResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var enrollments = await _context.Enrollments
                .Include(s => s.Student)
                .Include(c => c.Course)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            // تحويل الكيانات إلى استجابات باستخدام Mapster
            return enrollments.Adapt<IEnumerable<EnrollmentResponse>>();
        }

        public async Task<EnrollmentResponse> Updatesync(int enrollmentId, EnrollmentRequest request, CancellationToken cancellationToken)
        {
            var enrollment = await _context.Enrollments.SingleOrDefaultAsync(e => e.EnrollmentId == enrollmentId);
            if (enrollment == null)
            {
                return null!;
            }

            // تحديث الكيان باستخدام Mapster
            request.Adapt(enrollment);

            _context.Enrollments.Update(enrollment);
            await _context.SaveChangesAsync(cancellationToken);

            var result = await _context.Enrollments
                .Include(s => s.Student)
                .Include(c => c.Course)
                .SingleOrDefaultAsync(e => e.EnrollmentId == enrollmentId, cancellationToken);

            return result.Adapt<EnrollmentResponse>();
        }

        public async Task<IEnumerable<EnrollmentResponse>> GetEnrollmentsByStudentIdAsync(int studentId, CancellationToken cancellationToken)
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return enrollments.Adapt<IEnumerable<EnrollmentResponse>>();
        }

        public async Task<IEnumerable<EnrollmentResponse>> GetEnrollmentsByCourseIdAsync(int courseId, CancellationToken cancellationToken)
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => e.CourseId == courseId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return enrollments.Adapt<IEnumerable<EnrollmentResponse>>();
        }

        public async Task<IEnumerable<EnrollmentResponse>> GetTopPerformingStudentsByCourseIdAsync(int courseId, int topN, CancellationToken cancellationToken)
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => e.CourseId == courseId)
                .OrderByDescending(e => e.Grade)
                .Take(topN)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return enrollments.Adapt<IEnumerable<EnrollmentResponse>>();
        }

        public async Task<IEnumerable<EnrollmentResponse>> GetCoursesForStudentAsync(int studentId, CancellationToken cancellationToken)
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .Where(e => e.StudentId == studentId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return enrollments.Adapt<IEnumerable<EnrollmentResponse>>();
        }

        public async Task<IEnumerable<EnrollmentResponse>> EnrollMultipleStudentsAsync(IEnumerable<EnrollmentRequest> requests, CancellationToken cancellationToken)
        {
            var enrollments = new List<Enrollment>();

            foreach (var request in requests)
            {
                var existingEnrollment = await _context.Enrollments
                    .Where(e => e.StudentId == request.StudentId && e.CourseId == request.CourseId)
                    .ToListAsync(cancellationToken);

                if (existingEnrollment.Count == 0)
                {
                    var enrollment = request.Adapt<Enrollment>();
                    enrollments.Add(enrollment);
                }
            }

            if (enrollments.Any())
            {
                await _context.Enrollments.AddRangeAsync(enrollments, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return enrollments.Adapt<IEnumerable<EnrollmentResponse>>();
        }
    }
}
