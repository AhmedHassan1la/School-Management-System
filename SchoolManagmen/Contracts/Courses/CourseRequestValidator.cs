using SchoolManagmen.Contracts.Courses;

public class CourseRequestValidator : AbstractValidator<CourseRequest>
{
    public CourseRequestValidator()
    {
        RuleFor(x => x.CourseName)
            .NotEmpty().WithMessage("Course name is required.")
            .Length(3, 100).WithMessage("Course name must be between 3 and 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description can't be longer than 500 characters.");

        RuleFor(x => x.Credits)
            .GreaterThan(0).WithMessage("Credits must be greater than 0.")
            .LessThanOrEqualTo(6).WithMessage("Credits cannot exceed 6.");

        RuleFor(x => x.TeacherId)
            .GreaterThan(0).WithMessage("TeacherId must be a positive number.");
    }
}
