namespace SchoolManagmen.Contracts.Courses
{
    public record CourseResponse(
        int CourseId,
        string CourseName,
        string Description,
        int Credits,
        int TeacherId

    );
}
