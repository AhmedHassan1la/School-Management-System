namespace SchoolManagmen.Contracts.Students
{
    public class StudentRequestValidator : AbstractValidator<StudentRequest>
    {
        public StudentRequestValidator()
        {
            // FirstName Validation
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name can't be longer than 50 characters.");

            // LastName Validation
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name can't be longer than 50 characters.");

            // Email Validation
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            // DateOfBirth Validation (example for reference, not in the image)
            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .Must(BeAValidAge).WithMessage("Date of birth must be in the past and the student must be between 5 and 18 years old.");

            // EnrollmentDate Validation (example for reference, not in the image)
            RuleFor(x => x.EnrollmentDate)
                .NotEmpty().WithMessage("Enrollment date is required.")
                .Must((student, enrollmentDate) => BeAValidEnrollmentDate(student.DateOfBirth, enrollmentDate))
                .WithMessage("Enrollment date cannot be in the future and must be after the date of birth.");
        }

        private bool BeAValidAge(DateTime dateOfBirth)
        {
            var age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;

            return age >= 5 && age <= 18;
        }

        private bool BeAValidEnrollmentDate(DateTime dateOfBirth, DateTime enrollmentDate)
        {
            return enrollmentDate <= DateTime.Today && enrollmentDate > dateOfBirth;
        }
    }
}

