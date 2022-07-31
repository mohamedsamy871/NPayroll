using Core.Entities;
using FluentValidation;
using Payroll.ReportingService.ReportingViewModels;
using System;
using System.Text.RegularExpressions;

namespace Payroll.Validation
{
    public class EmployeeValidator:AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().NotEmpty().Length(4,50);
            RuleFor(x=>x.Phone).NotEmpty().NotNull().WithMessage("Phone Number is required.")
               .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
               .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
               .Matches(new Regex(@"^01[0125][0-9]{8}$")).WithMessage("Phone Number not valid");
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.JoinDate).NotNull().NotEmpty().Matches(new Regex(@"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$")).WithMessage("Date is not valid, ex. 2021-12-30");
            RuleFor(x => x.BirthDate).NotNull().NotEmpty().Matches(new Regex(@"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$")).WithMessage("Date is not valid, ex. 2021-12-30");
        }
    }
}
