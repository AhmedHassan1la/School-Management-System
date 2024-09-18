namespace SchoolManagmen.Contracts.Subjects
{
    public record SubjectResponse(
       int SubjectId,
       string SubjectName,
       string Description,
       string TeacherName
   );
}
