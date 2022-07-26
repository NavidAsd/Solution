using Endpoint.Models;
using FluentValidation;

namespace EndPoint.Models.Validation
{
    public class UserValidation : AbstractValidator<UserLoginViewModel>
    {
        public UserValidation()
        {

            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Please fill in the blank field")
                .NotNull().WithMessage("Please fill in the blank field");
            
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Please fill in the blank field")
                .NotNull().WithMessage("Please fill in the blank field");

        }
    }
}
