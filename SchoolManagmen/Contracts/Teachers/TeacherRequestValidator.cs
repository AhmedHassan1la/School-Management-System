using SchoolManagmen.Contracts.Teachers;

public class TeacherRequestValidator : AbstractValidator<TeacherRequest>
{
    public TeacherRequestValidator()
    {
        // First Name validation
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters.");

        // Last Name validation
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters.");

        // Date of Birth validation
        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .Must(BeAValidAge).WithMessage("Teacher must be at least 24 years old.");


        // Hire Date validation
        RuleFor(x => x.HireDate)
            .NotEmpty().WithMessage("Hire date is required.")
            .GreaterThanOrEqualTo(x => x.DateOfBirth.AddYears(24)).WithMessage("Hire date must be at least 24 years after the date of birth.");

        // Gender validation
        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Gender is required.")
            .Must(BeAValidGender).WithMessage("Gender must be either 'Male' or 'Female'.");

        // IsActive validation
        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("Active status is required.");

        // Subject validation
        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required.")
            .MaximumLength(100).WithMessage("Subject cannot be longer than 100 characters.");

        // Phone Number validation for Egyptian format
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^(?:\+20|0)?1[0-2]\d{8}$").WithMessage("Phone number must be a valid Egyptian number.");

        // Email validation
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.");

        // Address validation
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(255).WithMessage("Address cannot be longer than 255 characters.");
    }

    // Custom validation method for age at least 24 years
    private bool BeAValidAge(DateOnly dateOfBirth)
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        var age = currentDate.Year - dateOfBirth.Year;

        // Check if the birthday has not occurred yet this year
        if (currentDate < dateOfBirth.AddYears(age))
        {
            age--;
        }

        return age >= 24;
    }
    // Custom validation method for gender
    private bool BeAValidGender(string gender)
    {
        return gender == "Male" || gender == "Female";
    }
}
