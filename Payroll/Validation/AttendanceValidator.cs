using Core.Entities;
using FluentValidation;
using System;

namespace Payroll.Validation
{
    public class AttendanceValidator:AbstractValidator<Attendance>
    {
        public AttendanceValidator()
        {
            RuleFor(x => x.Month).NotNull().InclusiveBetween(1, 12)
                .WithMessage("Moth must be between 1 and 12");
            RuleFor(x => x.Year).NotNull()
                .InclusiveBetween(1990, DateTime.Now.Year)
                .WithMessage($"Year must be between 1990 and {DateTime.Now.Year}");
            RuleFor(x => x.EmployeeId).NotNull().NotEmpty();
            RuleFor(x => x.AbsenceConditionsId).NotNull().NotEmpty();
        }
    }
}
