namespace SchoolManagmen.Contracts.Courses
{
    public record CourseRequest(
        string CourseName,
        string Description,
        int Credits,
        int TeacherId
    );
}
