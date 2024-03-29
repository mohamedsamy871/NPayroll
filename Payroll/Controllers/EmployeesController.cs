﻿using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.HelperClasses;
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
        private readonly IncentiveBasedOnJoinDate _incentiveBasedOnJoinDate;

        public EmployeesController( IUnitOfWork<Employee> employee, DataContext db, IReporting iReporting, IncentiveBasedOnJoinDate incentiveBasedOnJoinDate)
        {
            _emp = employee;
            _db = db;
            _iReporting = iReporting;
            _incentiveBasedOnJoinDate = incentiveBasedOnJoinDate;
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
            employee.IncentiveId = _incentiveBasedOnJoinDate.GetIncentiveId(employee);
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
            employee.IncentiveId = _incentiveBasedOnJoinDate.GetIncentiveId(employee);
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
