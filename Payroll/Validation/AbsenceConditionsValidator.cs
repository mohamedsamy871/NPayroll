

using Core.Entities;
using FluentValidation;

namespace Payroll.Validation
{
    public class AbsenceConditionsValidator:AbstractValidator<AbsenceConditions>
    {
        public AbsenceConditionsValidator()
        {
            RuleFor(x => x.AbsenceCondition).NotEmpty().NotNull().Length(4, 50);
            RuleFor(x => x.DeductionAmount).LessThanOrEqualTo(2).WithMessage("Its a percentage , enter a number that less than 2");
            RuleFor(x => x.IncentiveAmount).LessThanOrEqualTo(2).WithMessage("Its a percentage , enter a number that less than 2");
        }
    }
}
