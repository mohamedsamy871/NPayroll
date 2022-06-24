using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey("AbsenceConditions")]
        public int AbsenceConditionsId { get; set; }
        public AbsenceConditions AbsenceConditions { get; set; }
    }
}
