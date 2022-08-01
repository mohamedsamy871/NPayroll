using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace Payroll.ReportingService.ReportingViewModels
{
    public class Reporting : IReporting
    {
        private readonly DataContext _db;
        private readonly IConfiguration _configuration;

        public Reporting(DataContext db,IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public IEnumerable<EmployeesData> GetEmployeesData()
        {
            try
            {
                var _employees = (from e in _db.Employees
                                  join d in _db.Departments
                                  on e.DepartmentId equals d.Id
                                  join r in _db.SalaryManagement
                                  on e.SalaryManagementId equals r.Id
                                  select new EmployeesData()
                                  {
                                      Name = e.Name,
                                      BirthDate = e.BirthDate,
                                      Address = e.Address,
                                      Phone = e.Phone,
                                      Email = e.Email,
                                      JoinDate = e.JoinDate,
                                      Department = d.Name,
                                      Rank = r.JobRank
                                  }).ToList();
                return _employees;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<SalaryData> GetSalaryData()
        {
            try
            {
                var _employeeSalary= (from e in _db.Employees
                                  join r in _db.SalaryManagement
                                  on e.SalaryManagementId equals r.Id
                                  select new SalaryData()
                                  {
                                      EmployeeName = e.Name,
                                      JobRank = r.JobRank,
                                      Salary = r.Salary
                                  }).ToList();
                return _employeeSalary;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<AttendanceData> GetAttendanceData()
        {
            try
            {
                
                   var _employeeAttendance = (from t in _db.Attendance
                                          join e in _db.Employees
                                           on t.EmployeeId equals e.Id
                                           join a in _db.AbsenceConditions
                                           on t.AbsenceConditionsId equals a.Id
                                       select new AttendanceData()
                                       {
                                           EmployeeName = e.Name,
                                           Month = t.Month,
                                           Year = t.Year,
                                           AbsenceCondition = a.AbsenceCondition,
                                           DeductionAmount = a.DeductionAmount,
                                           IncentiveAmount = a.IncentiveAmount
                                       }).ToList();
                return _employeeAttendance;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<IncentiveAndDeductionData> GetIncentiveAndDeductionData()
        {
            try
            {

                var _employeeIncentive = (from t in _db.Attendance
                                          join e in _db.Employees
                                           on t.EmployeeId equals e.Id
                                          join a in _db.AbsenceConditions
                                          on t.AbsenceConditionsId equals a.Id
                                          join n in _db.Incentive
                                          on e.IncentiveId equals n.Id
                                          join d in _db.Departments
                                          on e.DepartmentId equals d.Id
                                          join s in _db.SalaryManagement
                                          on e.SalaryManagementId equals s.Id
                                          select new IncentiveAndDeductionData()
                                          {
                                              EmployeeName = e.Name,
                                              Year = t.Year,
                                              Month = t.Month,
                                              DeductionDueToAttendance = a.DeductionAmount * s.Salary,
                                              IncentiveDueToAttendance = a.IncentiveAmount * s.Salary,
                                              IncentiveDueToSeniorityLevel = n.EmpIncentive * s.Salary,
                                              IncentiveDueToDepartment = d.Incentive * s.Salary,
                                              BasicSalary = s.Salary,
                                             NetSalary = (s.Salary) + ((a.IncentiveAmount * s.Salary)??0) + (n.EmpIncentive * s.Salary) + (d.Incentive * s.Salary) - ((a.DeductionAmount * s.Salary) ?? 0)
                                          }).ToList();
                return _employeeIncentive;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
