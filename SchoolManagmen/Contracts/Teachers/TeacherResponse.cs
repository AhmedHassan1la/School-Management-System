namespace SchoolManagmen.Contracts.Teachers
{
    public record TeacherResponse(
        int TeacherId,
        string FirstName,
            
                string ClassName,

        List<string> CourseNames

    );
}
