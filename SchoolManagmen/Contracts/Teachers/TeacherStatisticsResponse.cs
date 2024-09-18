namespace SchoolManagmen.Contracts.Teachers
{
    public record TeacherStatisticsResponse(
        int TotalTeachers,
        int ActiveTeachers,
        int InactiveTeachers
    );
}
