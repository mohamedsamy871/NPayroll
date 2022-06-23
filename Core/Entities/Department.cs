using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name="Department Incentive")]
        public double Incentive { get; set; }
        public ICollection<Employee> Employees { get; set; }

        public Department()
        {
            Employees = new Collection<Employee>();
        }
    }
}
