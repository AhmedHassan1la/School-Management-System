namespace SchoolManagmen.Contracts.Enrollments
{
    public record EnrollmentRequest(
     int StudentId,
     int CourseId,
     DateOnly EnrollmentDate,
     decimal Grade
        );

}
