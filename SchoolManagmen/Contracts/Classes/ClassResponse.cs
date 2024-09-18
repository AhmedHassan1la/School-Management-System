namespace SchoolManagmen.Contracts.Classes
{
    public record ClassResponse(
        int ClassId,
        string ClassName,
        int GradeLevel,
        int TeacherId,
        string TeacherName
    );
}
