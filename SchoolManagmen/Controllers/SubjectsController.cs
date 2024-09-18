using Asp.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagmen.Abstractions.Consts;
using SchoolManagmen.Authentication.Filters;
using SchoolManagmen.Contracts.Subjects;

namespace SchoolManagmen.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting(RateLimiters.Concurrency)]

    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        [HasPermission(Permissions.GetSubjects)]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _subjectService.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("{subjectId:int}")]
        [HasPermission(Permissions.GetSubjects)]

        public async Task<IActionResult> GetById(int subjectId, CancellationToken cancellationToken)
        {
            var result = await _subjectService.GetByIdAsync(subjectId, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [HasPermission(Permissions.AddSubjects)]

        public async Task<IActionResult> Add(SubjectRequest request, CancellationToken cancellationToken)
        {
            var result = await _subjectService.AddAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { subjectId = result.SubjectId }, result);
        }

        [HttpPut("{subjectId:int}")]
        [HasPermission(Permissions.UpdateSubjects)]

        public async Task<IActionResult> Update(int subjectId, SubjectRequest request, CancellationToken cancellationToken)
        {
            var result = await _subjectService.UpdateAsync(subjectId, request, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{subjectId:int}")]
        [HasPermission(Permissions.DeleteSubjects)]

        public async Task<IActionResult> Delete(int subjectId, CancellationToken cancellationToken)
        {
            var result = await _subjectService.DeleteAsync(subjectId, cancellationToken);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPut("{subjectId:int}/assign-teacher/{teacherId:int}")]
        [HasPermission(Permissions.UpdateSubjects)]

        public async Task<IActionResult> AssignTeacherToSubject(int subjectId, int teacherId, CancellationToken cancellationToken)
        {
            var result = await _subjectService.AssignTeacherToSubjectAsync(subjectId, teacherId, cancellationToken);

            if (result == null)
            {
                return NotFound("Subject or Teacher not found");
            }

            return Ok(result);
        }

    }
}
