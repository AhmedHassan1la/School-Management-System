namespace SchoolManagmen.Contracts.Teachers
{
    public record TeacherRequest(
        string FirstName,
        string LastName,
        DateOnly DateOfBirth,
        DateOnly HireDate,
        string Gender,
        bool IsActive,
        string Subject,
        string PhoneNumber,
        string Email,
        string Address
    );
}
