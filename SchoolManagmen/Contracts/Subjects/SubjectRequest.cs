namespace SchoolManagmen.Contracts.Subjects
{
    public record SubjectRequest(
        string SubjectName,
        string Description,
        int TeacherId
    );
}
