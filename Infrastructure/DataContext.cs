using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Incentive> Incentive { get; set; }
        public DbSet<SalaryManagement> SalaryManagement { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<AbsenceConditions> AbsenceConditions { get; set; }

    }
}
