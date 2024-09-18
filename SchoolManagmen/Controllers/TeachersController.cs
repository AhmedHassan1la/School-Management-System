using Asp.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagmen.Abstractions.Consts;
using SchoolManagmen.Authentication.Filters;
using SchoolManagmen.Contracts.Teachers;


namespace SchoolManagmen.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting(RateLimiters.Concurrency)]

    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        [HasPermission(Permissions.GetTeachers)]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var teachers = await _teacherService.GetAllAsync(cancellationToken);
            return Ok(teachers);
        }

        [HttpPost]
        [HasPermission(Permissions.AddTeachers)]

        public async Task<IActionResult> Add(TeacherRequest request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherService.AddAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = teacher.TeacherId }, teacher);
        }

        [HttpGet("{id}")]
        [HasPermission(Permissions.GetTeachers)]

        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var teacher = await _teacherService.GetByIdAsync(id, cancellationToken);
            return teacher != null ? Ok(teacher) : NotFound();
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.UpdateTeachers)]

        public async Task<IActionResult> Update(int id, TeacherRequest request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherService.UpdateAsync(id, request, cancellationToken);
            return teacher != null ? Ok(teacher) : NotFound();
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.DeleteTeachers)]

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var success = await _teacherService.DeleteAsync(id, cancellationToken);
            return success ? NoContent() : NotFound();
        }
        [HttpPut("{id}/toggle-status")]
        [HasPermission(Permissions.UpdateTeachers)]

        public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken)
        {
            var teacher = await _teacherService.ToggleStatusAsync(id, cancellationToken);
            return teacher != null ? Ok(teacher) : NotFound();
        }
        [HttpGet("search")]
        [HasPermission(Permissions.GetTeachers)]

        public async Task<IActionResult> SearchByName([FromQuery] string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name must be provided for search.");
            }

            var teachers = await _teacherService.SearchByNameAsync(name, cancellationToken);
            if (teachers == null || !teachers.Any())
            {
                return NotFound($"No teachers found with the name containing '{name}'.");
            }

            return Ok(teachers);
        }
        [HttpGet("active")]
        [HasPermission(Permissions.GetTeachers)]

        public async Task<IActionResult> GetActiveTeachers(CancellationToken cancellationToken)
        {
            var teachers = await _teacherService.GetActiveTeachersAsync(cancellationToken);
            return Ok(teachers);
        }
        [HttpGet("recently-hired")]
        [HasPermission(Permissions.GetTeachers)]

        public async Task<IActionResult> GetRecentlyHiredTeachers([FromQuery] DateOnly fromDate, CancellationToken cancellationToken)
        {
            var teachers = await _teacherService.GetRecentlyHiredTeachersAsync(fromDate, cancellationToken);
            return Ok(teachers);
        }
        [HttpGet("hire-range")]
        [HasPermission(Permissions.GetTeachers)]

        public async Task<IActionResult> GetTeachersByHireDateRange([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
        {
            var teachers = await _teacherService.GetTeachersByHireDateRangeAsync(startDate, endDate, cancellationToken);
            return Ok(teachers);
        }
        [HttpGet("statistics")]
        [HasPermission(Permissions.GetTeachers)]

        public async Task<IActionResult> GetTeacherStatistics(CancellationToken cancellationToken)
        {
            var statistics = await _teacherService.GetTeacherStatisticsAsync(cancellationToken);
            return Ok(statistics);
        }
        [HttpPut("{id}/update-phone")]
        [HasPermission(Permissions.UpdateTeachers)]

        public async Task<IActionResult> UpdatePhoneNumber(int id, [FromBody] string phoneNumber, CancellationToken cancellationToken)
        {
            try
            {
                var updatedTeacher = await _teacherService.UpdatePhoneNumberAsync(id, phoneNumber, cancellationToken);
                return Ok(updatedTeacher);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Teacher not found.");
            }
        }
    }
}
