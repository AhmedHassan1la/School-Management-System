namespace SchoolManagmen.Entites;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public bool IsActive { get; set; } = true;
    public string Gender { get; set; } = null!;

    public DateOnly HireDate { get; set; }

    public string? Subject { get; set; }

    public string? PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
