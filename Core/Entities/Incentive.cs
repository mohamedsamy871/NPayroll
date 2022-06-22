using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Incentive
    {
        public int Id { get; set; }
        public double EmpIncentive { get; set; }
        public string SeniorityLevel { get; set; }
    }
}
