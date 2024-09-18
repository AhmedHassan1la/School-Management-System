namespace SchoolManagmen.Contracts.GradeReports
{
    public record GradeReportRequest(
        int StudentId,
        int CourseId,
        decimal Grade,
        string Comments
    );
}
