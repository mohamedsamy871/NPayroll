using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.ReportingService.ReportingViewModels
{
    public class EmployeesData
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string JoinDate { get; set; }
        public string Department { get; set; }
        public string Rank { get; set; }
    }
}
