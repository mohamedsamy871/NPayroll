using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.ReportingService;
using Payroll.ViewModels;
using System;
using System.Linq;

namespace Payroll.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork<Employee> _emp;
        private readonly DataContext _db;
        private readonly IReporting _iReporting;
        public EmployeesController( IUnitOfWork<Employee> employee, DataContext db, IReporting iReporting)
        {
            _emp = employee;
            _db = db;
            _iReporting = iReporting;
        }
        // GET: EmployeesController
        public ActionResult Index()
        {
            var employees = _db.Employees.Include(m => m.Department).Include(m => m.Incentive).Include(m => m.SalaryManagement).ToList();
            return View(employees);
        }
        public IActionResult EditEmployee(int id)
        {
            //to fix some form error I send Id by ViewBag
            ViewBag.empId = id;
            var employeeInDb = _emp.Entity.GetById(id);
            EmployeesData _empDate = new EmployeesData()
            {
                Departments = _db.Departments.ToList(),
                Incentive = _db.Incentive.ToList(),
                SalaryManagement = _db.SalaryManagement.ToList(),
                Employee = employeeInDb
            };
            return View(_empDate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEmployee(Employee employee)
        {
            int employeeJoinYear = int.Parse(employee.JoinDate.Substring(0, 4));
            int yearsOfExperience = DateTime.Now.Year - employeeJoinYear;
            if (yearsOfExperience > 0)
            {
                var incentiveIdFromDb = _db.Incentive.Where(x => x.ExperienceInYears <= yearsOfExperience).Select(x => x.Id).FirstOrDefault();

                if (incentiveIdFromDb > 0)
                {
                    employee.IncentiveId = incentiveIdFromDb;
                }
            }
            _emp.Entity.Update(employee);
            _emp.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult AddEmployee()
        {
            EmployeesData _empDate = new EmployeesData()
            {
                Departments = _db.Departments.ToList(),
                Incentive = _db.Incentive.ToList(),
                SalaryManagement = _db.SalaryManagement.ToList(),
                Employee = new Employee()
            };
            return View(_empDate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee(Employee employee)
        {
            int employeeJoinYear = int.Parse(employee.JoinDate.Substring(0,4));
            int yearsOfExperience = DateTime.Now.Year - employeeJoinYear;
            if (yearsOfExperience > 0)
            {
                var incentiveFromDb = _db.Incentive.Where(x => x.ExperienceInYears <= yearsOfExperience).Select(x=>x.ExperienceInYears).ToList();
                int maxIncentive = incentiveFromDb.Max();
                int incentiveId = _db.Incentive.Where(x => x.ExperienceInYears == maxIncentive).Select(x => x.Id).FirstOrDefault();

                if (maxIncentive > 0)
                {
                    employee.IncentiveId = incentiveId;
                }
            }

            _emp.Entity.Add(employee);
            _emp.Save();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Delete(int id)
        {
            try
            {
                _emp.Entity.Delete(id);
                _emp.Save();
                return RedirectToAction(nameof(Index));
            } 
            catch
            {
                return NotFound();
            }
        }
        public IActionResult DownloadReport(IFormCollection obj)
        {
            string reportname = $"Employees_{Guid.NewGuid():N}.xlsx";
            var _emps = _iReporting.GetEmployeesData();
            if (_emps.Count() > 0)
            {
                var exportbytes = ReportToExcel.ExporttoExcel<Payroll.ReportingService.ReportingViewModels.EmployeesData>(_emps, reportname);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
            }
            else
            {
                TempData["Message"] = "No Data to Export";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
