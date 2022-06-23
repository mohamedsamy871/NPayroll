using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.ViewModels
{
    public class EmployeesData
    {
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Rank> Rank { get; set; }
        public IEnumerable<Incentive> Incentive { get; set; }
    }
}
