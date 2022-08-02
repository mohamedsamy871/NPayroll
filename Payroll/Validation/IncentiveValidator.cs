using Core.Entities;
using FluentValidation;

namespace Payroll.Validation
{
    public class IncentiveValidator:AbstractValidator<Incentive>
    {
        public IncentiveValidator()
        {
            RuleFor(x => x.ExperienceInYears).NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Experience in Years Should be more than 1");
            RuleFor(x => x.EmpIncentive).NotNull().NotEmpty().LessThanOrEqualTo(2).WithMessage("Its a percentage , enter a number that less than 2");
        }
    }
}
