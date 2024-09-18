namespace SchoolManagmen.Entites;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public string? Description { get; set; }

    public int TeacherId { get; set; }

    public virtual Teacher Teacher { get; set; } = null!;
}
