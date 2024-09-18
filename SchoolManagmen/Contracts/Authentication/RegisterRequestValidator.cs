using SchoolManagmen.Abstractions.Consts;

namespace SchoolManagmen.Contracts.Authentication
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()

                .MinimumLength(8)
              .Matches(RegexPatterns.Password)
              .WithMessage("the password must be at least 8 characters");


            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(3, 100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(3, 100);

        }
    }
}
