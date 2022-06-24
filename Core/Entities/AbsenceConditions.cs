using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AbsenceConditions
    {
        public int Id { get; set; }
        public string AbsenceCondition { get; set; }
        public double? DeductionAmount { get; set; }
        public double? IncentiveAmount { get; set; }
    }
}
