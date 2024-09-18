namespace SchoolManagmen.Entites;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public int GradeLevel { get; set; }

    public int TeacherId { get; set; }

    public virtual Teacher Teacher { get; set; } = null!;





}
