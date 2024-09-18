

namespace SchoolManagmen.Entites;

public partial class Student : AuditableEntity
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public int Grade { get; set; }

    public string Address { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; }


    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<GradeReport> GradeReports { get; set; } = new List<GradeReport>();


}
