using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SalaryManagement
    {
        public int Id { get; set; }
        public string JobRank { get; set; }
        public double Salary { get; set; }
    }
}
