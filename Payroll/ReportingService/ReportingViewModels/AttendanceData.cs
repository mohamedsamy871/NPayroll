using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.ReportingService.ReportingViewModels
{
    public class AttendanceData
    {
        public string EmployeeName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string AbsenceCondition { get; set; }
        public double? DeductionAmount { get; set; }
        public double? IncentiveAmount { get; set; }

    }
}
