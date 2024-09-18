
using SchoolManagmen.Contracts.Attendances;
using SchoolManagmen.Entites;

namespace SchoolManagmen.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext _context;

        public AttendanceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AttendanceResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var attendanceRecords = await _context.Attendances
                .Include(a => a.Student)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return attendanceRecords.Select(a => new AttendanceResponse(
                a.AttendanceId,
                a.StudentId,
                $"{a.Student.FirstName} {a.Student.LastName}",
                a.Date,
                a.Status
            )).ToList();
        }

        public async Task<AttendanceResponse> GetByIdAsync(int attendanceId, CancellationToken cancellationToken)
        {
            var attendance = await _context.Attendances
                .Include(a => a.Student)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AttendanceId == attendanceId, cancellationToken);

            if (attendance == null) return null!;

            return new AttendanceResponse(
                attendance.AttendanceId,
                attendance.StudentId,
                $"{attendance.Student.FirstName} {attendance.Student.LastName}",
                attendance.Date,
                attendance.Status
            );
        }

        public async Task<AttendanceResponse> AddAsync(AttendanceRequest request, CancellationToken cancellationToken)
        {
            var attendance = new Attendance
            {
                StudentId = request.StudentId,
                Date = request.Date,
                Status = request.Status
            };

            await _context.Attendances.AddAsync(attendance, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new AttendanceResponse(
                attendance.AttendanceId,
                attendance.StudentId,
                await GetStudentNameById(attendance.StudentId, cancellationToken),
                attendance.Date,
                attendance.Status
            );
        }

        public async Task<AttendanceResponse> UpdateAsync(int attendanceId, AttendanceRequest request, CancellationToken cancellationToken)
        {
            var attendance = await _context.Attendances.FindAsync(attendanceId);
            if (attendance == null) return null!;

            attendance.StudentId = request.StudentId;
            attendance.Date = request.Date;
            attendance.Status = request.Status;

            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync(cancellationToken);

            return new AttendanceResponse(
                attendance.AttendanceId,
                attendance.StudentId,
                await GetStudentNameById(attendance.StudentId, cancellationToken),
                attendance.Date,
                attendance.Status
            );
        }

        public async Task<bool> DeleteAsync(int attendanceId, CancellationToken cancellationToken)
        {
            var attendance = await _context.Attendances.FindAsync(attendanceId);
            if (attendance == null) return false;

            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<AttendanceResponse>> GetAttendanceByStudentIdAsync(int studentId, CancellationToken cancellationToken)
        {
            var attendanceRecords = await _context.Attendances
                .Include(a => a.Student)
                .Where(a => a.StudentId == studentId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return attendanceRecords.Select(a => new AttendanceResponse(
                a.AttendanceId,
                a.StudentId,
                $"{a.Student.FirstName} {a.Student.LastName}",
                a.Date,
                a.Status
            )).ToList();
        }

        private async Task<string> GetStudentNameById(int studentId, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FindAsync(studentId);
            return $"{student!.FirstName} {student.LastName}";
        }

        public async Task MarkAttendanceBulkAsync(IEnumerable<AttendanceRequest> requests, CancellationToken cancellationToken)
        {
            foreach (var request in requests)
            {
                // Check if the record already exists (to prevent duplicates)
                var existingAttendance = await _context.Attendances
                    .FirstOrDefaultAsync(a => a.StudentId == request.StudentId && a.Date == request.Date, cancellationToken);

                if (existingAttendance != null)
                {
                    // If attendance record exists, skip adding it again
                    continue;
                }

                var attendance = request.Adapt<Attendance>(); // Map request to Attendance entity
                await _context.Attendances.AddAsync(attendance, cancellationToken);
            }

            // Save changes to the database
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<IEnumerable<AttendanceResponse>> GetAbsenteesForDateAsync(DateOnly date, CancellationToken cancellationToken)
        {
            var absentees = await _context.Attendances
                .Where(a => a.Date == date && a.Status == "Absent")
                .Include(a => a.Student) // Include the Student entity
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            // Map the absentees to the response, including the studentName
            return absentees.Select(a => new AttendanceResponse(
                a.AttendanceId,
                a.StudentId,
                a.Student != null ? $"{a.Student.FirstName} {a.Student.LastName}" : "Unknown", // Full name of the student or "Unknown"
                a.Date,
                a.Status
            )).ToList();
        }




    }
}
