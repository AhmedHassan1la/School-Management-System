using Asp.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagmen.Abstractions.Consts;
using SchoolManagmen.Authentication.Filters;
using SchoolManagmen.Contracts.Common;
using SchoolManagmen.Contracts.Students;


namespace SchoolManagmen.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]


    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting(RateLimiters.Concurrency)]
    public class StudentsController(IStudentService studentService) : ControllerBase
    {
        private readonly IStudentService _studentService = studentService;
        [MapToApiVersion(1)]
        [HttpGet("all")]
        [HasPermission(Permissions.GetStudents)]
        public async Task<IActionResult> GetAllV1([FromQuery] RequestFilters filters, CancellationToken cancellationToken)
        {
            var students = await _studentService.GetAllAsyncV1(filters, cancellationToken);
            return students.IsSuccess ? Ok(students.Value) :
                         students.ToProblem();

        }
        [MapToApiVersion(2)]
        [HttpGet("all")]
        [HasPermission(Permissions.GetStudents)]
        public async Task<IActionResult> GetAllV2([FromQuery] RequestFilters filters, CancellationToken cancellationToken)
        {
            var students = await _studentService.GetAllAsyncV2(filters, cancellationToken);
            return students.IsSuccess ? Ok(students.Value) :
                         students.ToProblem();

        }
        [HttpPost]
        [HasPermission(Permissions.AddStudents)]

        public async Task<IActionResult> Add(StudentRequest request, CancellationToken cancellationToken)
        {
            var student = await _studentService.AddAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = student.StudentId }, student);
        }

        [HttpGet("{id}")]
        [HasPermission(Permissions.GetStudents)]

        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetByIdAsync(id, cancellationToken);
            return student.IsSuccess ? Ok(student.Value) :
                BadRequest(student.Error);


        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.UpdateStudents)]

        public async Task<IActionResult> Update(int id, StudentRequest request, CancellationToken cancellationToken)
        {
            var student = await _studentService.UpdateAsync(id, request, cancellationToken);
            return student.IsSuccess ? Ok(student.Value) : BadRequest(student.Error);

        }
        [HttpDelete("{id}")]
        [HasPermission(Permissions.DeleteStudents)]

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _studentService.DeleteAsync(id, cancellationToken);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("search")]
        [HasPermission(Permissions.GetStudents)]

        public async Task<IActionResult> Search([FromQuery] string keyword, CancellationToken cancellationToken)
        {
            var students = await _studentService.SearchAsync(keyword, cancellationToken);
            return Ok(students);
        }





    }
}

