using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string JoinDate { get; set; }

        [ForeignKey("Rank")]
        public int RankId { get; set; }
        public Rank Rank { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }



        [ForeignKey("Incentive")]
        public int IncentiveId { get; set; }
        public Incentive Incentive { get; set; }

        public ICollection<Attendance> Attendance { get; set; }

        public Employee()
        {
            Attendance = new Collection<Attendance>();
        }

    }
}
