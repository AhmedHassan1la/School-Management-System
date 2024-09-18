using SchoolManagmen.Contracts.Courses;
using SchoolManagmen.Entites;


namespace SchoolManagmen.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all courses
        public async Task<IEnumerable<CourseResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var courses = await _context.Courses
               .Include(c => c.Teacher)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var coursesresponse = courses.Select(c => new CourseResponse(
                c.CourseId,
                c.CourseName,
                c.Description,
                c.Credits,
                c.TeacherId




                ));
            return coursesresponse;

        }

        // Add a new course
        public async Task<CourseResponse> AddAsync(CourseRequest request, CancellationToken cancellationToken)
        {




            var course = request.Adapt<Course>();

            await _context.Courses.AddAsync(course, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            var a = await _context.Courses.Include(t => t.Teacher).SingleOrDefaultAsync(c => c.CourseId == course.CourseId, cancellationToken);


            return new CourseResponse(
                a!.CourseId,
                 a.CourseName,

                a.Description!,
                a.Credits,
                a.TeacherId


                );
        }

        // Get a course by id
        public async Task<CourseResponse> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var course = await _context.Courses
                 .Include(c => c.Teacher) 
                .SingleOrDefaultAsync(c => c.CourseId == id, cancellationToken);

            if (course == null)
            {
                throw new KeyNotFoundException("Course not found.");
            }
            return new CourseResponse(
                          course.CourseId,
                           course.CourseName,

                          course.Description!,
                          course.Credits,
                          course.TeacherId


                          );
        }

        // Update a course by id
        public async Task<CourseResponse> UpdateAsync(int id, CourseRequest request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses
                .Include(t=>t.Teacher)
                .SingleOrDefaultAsync(c => c.CourseId == id, cancellationToken);

            if (course == null)
            {
                throw new KeyNotFoundException("Course not found.");
            }

            // Update the course details
            //course.CourseName = request.CourseName;
            //course.Description = request.Description;
            //course.Credits = request.Credits;
            //course.TeacherId = request.TeacherId;

            request.Adapt(course);

            _context.Courses.Update(course);
            await _context.SaveChangesAsync(cancellationToken);

            return new CourseResponse(
                                     course.CourseId,
                                      course.CourseName,

                                     course.Description!,
                                     course.Credits,
                                     course.TeacherId


                                     );
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var course = await _context.Courses
                .SingleOrDefaultAsync(c => c.CourseId == id, cancellationToken);

            if (course == null)
            {
                return false;
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<CourseResponse>> GetCoursesByTeacherIdAsync(int teacherId, CancellationToken cancellationToken)
        {
            // Query to get courses by teacherId
            var courses = await _context.Courses
                .Where(c => c.TeacherId == teacherId)
                .Include(c => c.Teacher) 
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var coursesresponse = courses.Select(c => new CourseResponse(
                          c.CourseId,
                          c.CourseName,
                          c.Description,
                          c.Credits,
                          c.TeacherId




                          ));
            return coursesresponse;
        }

        public async Task<IEnumerable<CourseResponse>> SearchCoursesByNameAsync(string courseName, CancellationToken cancellationToken)
        {
            courseName = courseName.Trim().ToLower();

            var CourseName = await _context.Courses
                .Include(t => t.Teacher)
                .Where(c => c.CourseName.Trim().ToLower().Contains(courseName)).
                AsNoTracking().
                ToListAsync(cancellationToken);
            var coursesresponse = CourseName.Select(c => new CourseResponse(
                                     c.CourseId,
                                     c.CourseName,
                                     c.Description,
                                     c.Credits,
                                     c.TeacherId




                                     ));
            return coursesresponse;


        }

        public async Task<IEnumerable<CourseResponse>> GetCoursesByCreditsRangeAsync(int minCredits, int maxCredits, CancellationToken cancellationToken)
        {
            if (minCredits > maxCredits)
            {
                throw new ArgumentException("minCredits cannot be earlier than maxCredits");

            }

            var courses = await _context.Courses.Include(t => t.Teacher)
                .Where(c => c.Credits > minCredits && c.Credits < maxCredits)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            var coursesresponse = courses.Select(c => new CourseResponse(
                           c.CourseId,
                           c.CourseName,
                           c.Description,
                           c.Credits,
                           c.TeacherId


                           ));
            return coursesresponse;

        }
        // Method to update the teacher assigned to a course
        public async Task<CourseResponse> UpdateCourseTeacherAsync(int courseId, int teacherId, CancellationToken cancellationToken)
        {
            var course = await _context.Courses
                .SingleOrDefaultAsync(c => c.CourseId == courseId, cancellationToken);

            if (course == null)
            {
                return null!;
            }

            var teacher = await _context.Teachers
                .SingleOrDefaultAsync(t => t.TeacherId == teacherId, cancellationToken);

            if (teacher == null)
            {
                return null!;
            }

            course.TeacherId = teacherId;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync(cancellationToken);

            return new CourseResponse(
                                               course.CourseId,
                                                course.CourseName,

                                               course.Description!,
                                               course.Credits,
                                               course.TeacherId


                                               );
        }


    }
}
