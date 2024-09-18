namespace SchoolManagmen.Contracts.Students
{
    public record StudentRequest(

         string FirstName,
         string LastName,
         DateTime DateOfBirth,
         string Gender,
         int Grade,
         string Address,
         string PhoneNumber,
         string Email,
         DateTime EnrollmentDate
        );


}
