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

        [Display(Name ="Incentive Percentage due to Seniority Level")]
        public double EmpIncentive { get; set; }

        [Display(Name = "Years of Experience")]
        public int ExperienceInYears { get; set; }
    }
}
