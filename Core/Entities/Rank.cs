using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Rank
    {
        public int Id { get; set; }

        [EnumDataType(typeof(JobRank))]
        public JobRank JobRank { get; set; }

        public double Salary { get; set; }
    }
    public enum JobRank
    {
        [Display(Name = "Class A")]
        ClassA,

        [Display(Name = "Class B")]
        ClassB,

        [Display(Name = "Class C")]
        ClassC
    }
}
