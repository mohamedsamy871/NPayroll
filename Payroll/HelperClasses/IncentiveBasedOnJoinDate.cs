
using Core.Entities;
using Infrastructure;
using System;
using System.Linq;

namespace Payroll.HelperClasses
{
    public class IncentiveBasedOnJoinDate
    {
        private readonly DataContext _db;

        public IncentiveBasedOnJoinDate(DataContext db)
        {
            _db = db;
        }
        public int GetIncentiveId(Employee employee) 
        {
            int employeeJoinYear = int.Parse(employee.JoinDate.Substring(0, 4));
            int yearsOfExperience = DateTime.Now.Year - employeeJoinYear;
            if (yearsOfExperience > 0)
            {
                var incentiveFromDb = _db.Incentive.Where(x => x.ExperienceInYears <= yearsOfExperience).Select(x => x.ExperienceInYears).ToList();
                int maxIncentive = incentiveFromDb.Max();
                int incentiveId = _db.Incentive.Where(x => x.ExperienceInYears == maxIncentive).Select(x => x.Id).FirstOrDefault();

                if (maxIncentive > 0)
                {
                    return incentiveId;
                }
            }
            return 0;
        }
    }
}
