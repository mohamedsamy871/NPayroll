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

        [EnumDataType(typeof(Seniority))]
        public Seniority SeniorityLevel { get; set; }

        public enum Seniority
        {
            FiveYears,
            SevenYears
        }
    }
}
