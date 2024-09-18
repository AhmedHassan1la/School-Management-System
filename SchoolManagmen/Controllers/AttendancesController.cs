using Asp.Versioning;
using SchoolManagmen.Abstractions.Consts;
using SchoolManagmen.Authentication.Filters;
using SchoolManagmen.Contracts.Attendances;


namespace SchoolManagmen.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendancesController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        [HasPermission(Permissions.GetAttendances)]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var attendanceRecords = await _attendanceService.GetAllAsync(cancellationToken);
            return Ok(attendanceRecords);
        }

        [HttpGet("{id}")]
        [HasPermission(Permissions.GetAttendances)]

        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var attendance = await _attendanceService.GetByIdAsync(id, cancellationToken);
            if (attendance == null)
                return NotFound("Attendance record not found.");
            return Ok(attendance);
        }

        [HttpPost]
        [HasPermission(Permissions.AddAttendances)]

        public async Task<IActionResult> Add([FromBody] AttendanceRequest request, CancellationToken cancellationToken)
        {
            var attendance = await _attendanceService.AddAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = attendance.AttendanceId }, attendance);
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.UpdateAttendances)]

        public async Task<IActionResult> Update(int id, [FromBody] AttendanceRequest request, CancellationToken cancellationToken)
        {
            var attendance = await _attendanceService.UpdateAsync(id, request, cancellationToken);
            if (attendance == null)
                return NotFound("Attendance record not found.");
            return Ok(attendance);
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.DeleteAttendances)]

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _attendanceService.DeleteAsync(id, cancellationToken);
            if (!result)
                return NotFound("Attendance record not found.");
            return NoContent();
        }

        [HttpGet("student/{studentId}")]
        [HasPermission(Permissions.GetAttendances)]

        public async Task<IActionResult> GetAttendanceByStudentId(int studentId, CancellationToken cancellationToken)
        {
            var attendanceRecords = await _attendanceService.GetAttendanceByStudentIdAsync(studentId, cancellationToken);
            return Ok(attendanceRecords);
        }

        // POST: api/attendances/bulk
        [HttpPost("bulk")]
        [HasPermission(Permissions.GetAttendances)]

        public async Task<IActionResult> MarkAttendanceBulk([FromBody] IEnumerable<AttendanceRequest> requests, CancellationToken cancellationToken)
        {
            // Call the service method to mark attendance in bulk
            await _attendanceService.MarkAttendanceBulkAsync(requests, cancellationToken);

            // Return a success response (no need to return any data)
            return Ok("Attendance records have been successfully added.");
        }
        [HttpGet("absentees")]
        [HasPermission(Permissions.GetAttendances)]

        public async Task<IActionResult> GetAbsenteesForDate([FromQuery] DateOnly date, CancellationToken cancellationToken)
        {
            var absentees = await _attendanceService.GetAbsenteesForDateAsync(date, cancellationToken);
            if (absentees == null || !absentees.Any())
                return NotFound("No absentees found for the specified date.");

            return Ok(absentees);
        }
    }
}
