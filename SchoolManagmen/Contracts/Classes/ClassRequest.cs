namespace SchoolManagmen.Contracts.Classes
{
    public record ClassRequest(
        string ClassName,
        int GradeLevel,
        int TeacherId
    );
}
