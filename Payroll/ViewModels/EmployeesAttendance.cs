using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.ViewModels
{
    public class EmployeesAttendance
    {
        public Attendance Attendance { get; set; }
        public IEnumerable<Employee> Employees{ get; set; }
    }
}
