using Asp.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagmen.Abstractions.Consts;
using SchoolManagmen.Authentication.Filters;
using SchoolManagmen.Contracts.Enrollments;

namespace SchoolManagmen.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting(RateLimiters.Concurrency)]

    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost]
        [HasPermission(Permissions.AddEnrollments)]

        public async Task<IActionResult> Add(EnrollmentRequest request, CancellationToken cancellationToken)
        {
            var result = await _enrollmentService.AddAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { enrollmentId = result.EnrollmentId }, result);
        }

        [HttpGet("{enrollmentId:int}")]
        [HasPermission(Permissions.GetEnrollments)]

        public async Task<IActionResult> GetById(int enrollmentId, CancellationToken cancellationToken)
        {
            var result = await _enrollmentService.GetByIdAsync(enrollmentId, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        [HttpGet("")]
        [HasPermission(Permissions.GetEnrollments)]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _enrollmentService.GetAllAsync(cancellationToken);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("{enrollmentId}")]
        [HasPermission(Permissions.UpdateEnrollments)]

        public async Task<IActionResult> Update(int enrollmentId, EnrollmentRequest request, CancellationToken cancellationToken)
        {
            var enrollments = await _enrollmentService.Updatesync(enrollmentId, request, cancellationToken);
            return Ok(enrollments);
        }
        [HttpGet("student/{studentId:int}")]
        [HasPermission(Permissions.GetEnrollments)]

        public async Task<IActionResult> GetEnrollmentsByStudentId(int studentId, CancellationToken cancellationToken)
        {
            var result = await _enrollmentService.GetEnrollmentsByStudentIdAsync(studentId, cancellationToken);

            if (result == null || !result.Any())
            {
                return NotFound($"No enrollments found for student with ID {studentId}");
            }

            return Ok(result);
        }
        [HttpGet("course/{courseId:int}")]
        [HasPermission(Permissions.GetEnrollments)]

        public async Task<IActionResult> GetEnrollmentsByCoursetId(int courseId, CancellationToken cancellationToken)
        {
            var result = await _enrollmentService.GetEnrollmentsByStudentIdAsync(courseId, cancellationToken);

            if (result == null || !result.Any())
            {
                return NotFound($"No enrollments found for course with ID {courseId}");
            }

            return Ok(result);
        }
        [HttpGet("course/{courseId:int}/top/{topN:int}")]
        [HasPermission(Permissions.GetEnrollments)]

        public async Task<IActionResult> GetTopPerformingStudentsByCourseId(int courseId, int topN, CancellationToken cancellationToken)
        {
            var result = await _enrollmentService.GetTopPerformingStudentsByCourseIdAsync(courseId, topN, cancellationToken);

            if (result == null || !result.Any())
            {
                return NotFound($"No enrollments found or insufficient students for course with ID {courseId}");
            }

            return Ok(result);
        }
        [HttpGet("student/{studentId:int}/courses")]
        [HasPermission(Permissions.GetEnrollments)]

        public async Task<IActionResult> GetCoursesForStudentAsync(int studentId, CancellationToken cancellationToken)
        {
            var result = await _enrollmentService.GetCoursesForStudentAsync(studentId, cancellationToken);

            if (result == null || !result.Any())
            {
                return NotFound($"No courses found for student with ID {studentId}");
            }

            return Ok(result);
        }
        [HttpPost("enroll-multiple")]
        [HasPermission(Permissions.AddEnrollments)]

        public async Task<IActionResult> EnrollMultipleStudentsAsync(IEnumerable<EnrollmentRequest> requests, CancellationToken cancellationToken)
        {
            var result = await _enrollmentService.EnrollMultipleStudentsAsync(requests, cancellationToken);

            if (!result.Any())
            {
                return BadRequest("No students were enrolled.");
            }

            return Ok(result);
        }


    }
}
