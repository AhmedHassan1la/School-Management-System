namespace SchoolManagmen.Contracts.GradeReports
{
    public record GradeReportResponse(
        int GradeReportId,
        int StudentId,
        string StudentName,
        int CourseId,
        string CourseName,
        decimal Grade,
        string Comments
    );
}
