using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AbsenceConditions
    {
        public int Id { get; set; }
        public string AbsenceCondition { get; set; }

        [Display(Name ="Deduction Percentage")]
        public double? DeductionAmount { get; set; }
        [Display(Name = "Incentive Percentage")]
        public double? IncentiveAmount { get; set; }
    }
}
