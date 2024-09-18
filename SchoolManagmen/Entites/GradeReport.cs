namespace SchoolManagmen.Entites;

public partial class GradeReport
{

    public int GradeReportId { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public decimal Grade { get; set; }

    public string? Comments { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
