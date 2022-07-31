
using Core.Entities;
using FluentValidation;

namespace Payroll.Validation
{
    public class DepartmentValidator:AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(4, 50);
            RuleFor(x => x.Incentive).NotNull().NotEmpty().LessThanOrEqualTo(10).WithMessage("Its a percentage , enter a number that less than 10");
        }
    }
}
