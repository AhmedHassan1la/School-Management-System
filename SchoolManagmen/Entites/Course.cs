namespace SchoolManagmen.Entites;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string Description { get; set; }=null!;

    public int Credits { get; set; }

    public int TeacherId { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<GradeReport> GradeReports { get; set; } = new List<GradeReport>();

    public virtual Teacher Teacher { get; set; } = null!;
}
