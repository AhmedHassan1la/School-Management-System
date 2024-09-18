using Asp.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagmen.Abstractions.Consts;
using SchoolManagmen.Authentication.Filters;
using SchoolManagmen.Contracts.Classes;

namespace SchoolManagmen.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting(RateLimiters.Concurrency)]

    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        [HasPermission(Permissions.GetClasses)]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var classes = await _classService.GetAllAsync(cancellationToken);
            return Ok(classes);
        }

        [HttpGet("{classId:int}")]
        [HasPermission(Permissions.GetClasses)]

        public async Task<IActionResult> GetById(int classId, CancellationToken cancellationToken)
        {
            var classResponse = await _classService.GetByIdAsync(classId, cancellationToken);
            if (classResponse == null)
            {
                return NotFound();
            }
            return Ok(classResponse);
        }

        [HttpPost]
        [HasPermission(Permissions.AddClasses)]

        public async Task<IActionResult> Add(ClassRequest request, CancellationToken cancellationToken)
        {
            var classResponse = await _classService.AddAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { classId = classResponse.ClassId }, classResponse);
        }

        [HttpPut("{classId:int}")]
        [HasPermission(Permissions.UpdateClasses)]

        public async Task<IActionResult> Update(int classId, ClassRequest request, CancellationToken cancellationToken)
        {
            var classResponse = await _classService.UpdateAsync(classId, request, cancellationToken);
            if (classResponse == null)
            {
                return NotFound();
            }
            return Ok(classResponse);
        }

        [HttpDelete("{classId:int}")]
        [HasPermission(Permissions.DeleteClasses)]

        public async Task<IActionResult> Delete(int classId, CancellationToken cancellationToken)
        {
            var result = await _classService.DeleteAsync(classId, cancellationToken);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // New endpoint to get classes by grade level
        [HttpGet("grade/{gradeLevel:int}")]
        [HasPermission(Permissions.GetClasses)]

        public async Task<ActionResult<IEnumerable<ClassResponse>>> GetByGradeLevel(int gradeLevel, CancellationToken cancellationToken)
        {
            var classes = await _classService.GetByGradeLevelAsync(gradeLevel, cancellationToken);
            return Ok(classes);
        }
        // New endpoint to get classes by grade level
        [HttpGet("name")]
        [HasPermission(Permissions.GetClasses)]

        public async Task<ActionResult<IEnumerable<ClassResponse>>> SearchByNameAsync([FromQuery] string name, CancellationToken cancellationToken)
        {
            var classes = await _classService.SearchByNameAsync(name, cancellationToken);
            return Ok(classes);
        }
        [HttpGet("IsClassExist")]
        [HasPermission(Permissions.GetClasses)]

        public async Task<IActionResult> IsClassExistsAsync(string className, CancellationToken cancellationToken)
        {
            var result = await _classService.IsClassExistsAsync(className, cancellationToken);
            return Ok(result);
        }

        [HttpGet("teacher/{teacherId:int}")]
        [HasPermission(Permissions.GetClasses)]

        public async Task<IActionResult> GetClassesByTeacherIdAsync(int teacherId, CancellationToken cancellationToken)
        {
            var result = await _classService.GetClassesByTeacherIdAsync(teacherId, cancellationToken);
            return Ok(result);
        }
    }
}
