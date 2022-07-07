using FluentValidation;

namespace TranslateAPI.Contacts.Validators
{
    public class AccountValidator:AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(a => a.UserName).NotNull().NotEmpty();
            RuleFor(a => a.PassWork).NotNull().NotEmpty();
        }
    }
}
