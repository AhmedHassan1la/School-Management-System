using Asp.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagmen.Abstractions.Consts;
using SchoolManagmen.Authentication.Filters;
using SchoolManagmen.Contracts.GradeReports;

namespace SchoolManagmen.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting(RateLimiters.Concurrency)]

    public class GradeReportsController : ControllerBase
    {
        private readonly IGradeReportService _gradeReportService;

        public GradeReportsController(IGradeReportService gradeReportService)
        {
            _gradeReportService = gradeReportService;
        }

        [HttpGet]
        [HasPermission(Permissions.GetGradeReports)]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var gradeReports = await _gradeReportService.GetAllAsync(cancellationToken);
            return Ok(gradeReports);
        }

        [HttpGet("{id}")]
        [HasPermission(Permissions.GetGradeReports)]

        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var gradeReport = await _gradeReportService.GetByIdAsync(id, cancellationToken);

            if (gradeReport == null)
            {
                return NotFound("Grade report not found.");
            }

            return Ok(gradeReport);
        }

        [HttpPost]
        [HasPermission(Permissions.AddGradeReports)]

        public async Task<IActionResult> Add([FromBody] GradeReportRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gradeReport = await _gradeReportService.AddAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = gradeReport.GradeReportId }, gradeReport);
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.DeleteGradeReports)]

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _gradeReportService.DeleteAsync(id, cancellationToken);

            if (!result)
            {
                return NotFound("Grade report not found.");
            }

            return NoContent(); 
        }

        [HttpPut("{reportId}")]
        [HasPermission(Permissions.UpdateGradeReports)]

        public async Task<IActionResult> UpdateGradeReport(int reportId, [FromBody] GradeReportRequest request, CancellationToken cancellationToken)
        {
            var updatedReport = await _gradeReportService.UpdateAsync(reportId, request, cancellationToken);

            if (updatedReport == null)
            {
                return NotFound("Grade report not found.");
            }

            return Ok(updatedReport);
        }
        [HttpGet("student/{studentId}")]
        [HasPermission(Permissions.GetGradeReports)]

        public async Task<IActionResult> GetGradeReportsByStudentId(int studentId, CancellationToken cancellationToken)
        {
            var gradeReports = await _gradeReportService.GetGradeReportsByStudentIdAsync(studentId, cancellationToken);
            if (gradeReports == null)
            {
                return NotFound("No grade reports found for the specified student.");
            }
            return Ok(gradeReports);
        }
        [HttpGet("course/{courseId}")]
        [HasPermission(Permissions.GetGradeReports)]

        public async Task<IActionResult> GetGradeReportsByCourseId(int courseId, CancellationToken cancellationToken)
        {
            var gradeReports = await _gradeReportService.GetGradeReportsByCourseIdAsync(courseId, cancellationToken);
            if (gradeReports == null || !gradeReports.Any())
            {
                return NotFound("No grade reports found for the specified course.");
            }
            return Ok(gradeReports);
        }
        [HttpGet("course/{courseId}/average-grade")]
        [HasPermission(Permissions.GetGradeReports)]

        public async Task<IActionResult> GetAverageGradeByCourseId(int courseId, CancellationToken cancellationToken)
        {
            var averageGrade = await _gradeReportService.GetAverageGradeByCourseIdAsync(courseId, cancellationToken);
            return Ok(averageGrade);
        }
        [HttpGet("course/{courseId}/top-performers/{topN}")]
        public async Task<IActionResult> GetTopPerformingStudentsInCourse(int courseId, int topN, CancellationToken cancellationToken)
        {
            var topStudents = await _gradeReportService.GetTopPerformingStudentsInCourseAsync(courseId, topN, cancellationToken);
            return Ok(topStudents);
        }

        [HttpGet("average-grade")]
        [HasPermission(Permissions.GetGradeReports)]

        public async Task<IActionResult> GetOverallAverageGrade(CancellationToken cancellationToken)
        {
            var averageGrade = await _gradeReportService.GetOverallAverageGradeAsync(cancellationToken);

            if (averageGrade == 0)
            {
                return NotFound("No grade reports found.");
            }

            return Ok(averageGrade);
        }

    }
}
