using Core.Entities;
using Payroll.ReportingService.ReportingViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.ReportingService
{
    public interface IReporting
    {
        public IEnumerable<EmployeesData> GetEmployeesData();
        public IEnumerable<SalaryData> GetSalaryData();
        public IEnumerable<AttendanceData> GetAttendanceData();
        public IEnumerable<IncentiveAndDeductionData> GetIncentiveAndDeductionData();
    }
}
