namespace SchoolManagmen.Contracts.Enrollments
{
    public record EnrollmentResponse(
        int EnrollmentId,
         int StudentId,
     int CourseId,
     string StudentName,
     string CourseName,
     DateOnly EnrollmentDate,
     decimal Grade



        );

}
