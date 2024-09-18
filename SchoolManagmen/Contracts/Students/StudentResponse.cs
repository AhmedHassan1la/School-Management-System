namespace SchoolManagmen.Contracts.Students
{
    public record StudentResponse(

         int StudentId,
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
    public record StudentResponseV2(

         int StudentId,
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
