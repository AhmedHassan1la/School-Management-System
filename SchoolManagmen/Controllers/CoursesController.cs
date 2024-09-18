using Asp.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagmen.Abstractions.Consts;
using SchoolManagmen.Authentication.Filters;
using SchoolManagmen.Contracts.Courses;

namespace SchoolManagmen.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting(RateLimiters.Concurrency)]

    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // Get all courses
        [HttpGet]
        [HasPermission(Permissions.GetCourses)]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var courses = await _courseService.GetAllAsync(cancellationToken);
            return Ok(courses);
        }

        [HttpPost]
        [HasPermission(Permissions.AddCourses)]

        public async Task<IActionResult> Add(CourseRequest request, CancellationToken cancellationToken)
        {


            var course = await _courseService.AddAsync(request, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = course.CourseId }, course);
        }


        // Get a course by id
        [HttpGet("{id}")]
        [HasPermission(Permissions.GetCourses)]

        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _courseService.GetByIdAsync(id, cancellationToken);
                return Ok(course);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Course not found.");
            }
        }

        // Update a course
        [HttpPut("{id}")]
        [HasPermission(Permissions.UpdateCourses)]

        public async Task<IActionResult> Update(int id, [FromBody] CourseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedCourse = await _courseService.UpdateAsync(id, request, cancellationToken);
                return Ok(updatedCourse);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Course not found.");
            }
        }

        // Delete a course
        [HttpDelete("{id}")]
        [HasPermission(Permissions.DeleteCourses)]

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _courseService.DeleteAsync(id, cancellationToken);
            if (!result)
            {
                return NotFound("Course not found.");
            }
            return NoContent(); // Successfully deleted
        }

        [HttpGet("teacher/{teacherId}")]
        [HasPermission(Permissions.GetCourses)]

        public async Task<IActionResult> GetCoursesByTeacherId(int teacherId, CancellationToken cancellationToken)
        {
            var courses = await _courseService.GetCoursesByTeacherIdAsync(teacherId, cancellationToken);
            return Ok(courses);
        }
        // Search for courses by name
        [HttpGet("search")]
        [HasPermission(Permissions.GetCourses)]

        public async Task<IActionResult> SearchCoursesByName([FromQuery] string courseName, CancellationToken cancellationToken)
        {
            var courses = await _courseService.SearchCoursesByNameAsync(courseName, cancellationToken);
            return Ok(courses);
        }
        [HttpGet("credits-range")]
        [HasPermission(Permissions.GetCourses)]

        public async Task<IActionResult> GetCoursesByCreditsRange([FromQuery] int minCredits, [FromQuery] int maxCredits, CancellationToken cancellationToken)
        {
            var courses = await _courseService.GetCoursesByCreditsRangeAsync(minCredits, maxCredits, cancellationToken);
            return Ok(courses);
        }

        [HttpPut("{courseId}/update-teacher")]
        [HasPermission(Permissions.UpdateCourses)]

        public async Task<IActionResult> UpdateCourseTeacher(int courseId, [FromQuery] int teacherId, CancellationToken cancellationToken)
        {
            var updatedCourse = await _courseService.UpdateCourseTeacherAsync(courseId, teacherId, cancellationToken);

            if (updatedCourse == null)
            {
                return NotFound("Course or teacher not found.");
            }

            return Ok(updatedCourse);
        }
    }
}
