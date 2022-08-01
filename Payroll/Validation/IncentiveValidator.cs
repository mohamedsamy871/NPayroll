using Core.Entities;
using FluentValidation;

namespace Payroll.Validation
{
    public class IncentiveValidator:AbstractValidator<Incentive>
    {
        public IncentiveValidator()
        {
            RuleFor(x => x.SeniorityLevel).NotNull().NotEmpty().Length(4, 50); 
            RuleFor(x => x.EmpIncentive).NotNull().NotEmpty().LessThanOrEqualTo(2).WithMessage("Its a percentage , enter a number that less than 2"); ;
        }
    }
}
