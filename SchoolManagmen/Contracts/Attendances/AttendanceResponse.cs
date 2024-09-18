namespace SchoolManagmen.Contracts.Attendances
{
    public record AttendanceResponse(

          int AttendanceId,
        int StudentId,
        string StudentName,
               DateOnly Date,
               string Status



        );

}
