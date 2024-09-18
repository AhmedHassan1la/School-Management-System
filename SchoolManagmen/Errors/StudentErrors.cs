namespace SchoolManagmen.Errors;

public static class StudentErrors
{
    public static readonly Error StudentNotFound =
        new("Student.NotFound", "No poll was found with the given ID", StatusCodes.Status404NotFound);
}