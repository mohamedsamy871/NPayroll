using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.ReportingService.ReportingViewModels
{
    public class IncentiveAndDeductionData
    {
        public string EmployeeName { get; set; }
        public double? DeductionDueToAttendance { get; set; }
        public double? IncentiveDueToAttendance { get; set; }
        public double IncentiveDueToSeniorityLevel { get; set; }
        public double IncentiveDueToDepartment { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
