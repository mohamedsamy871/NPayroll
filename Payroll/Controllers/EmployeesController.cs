using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork<Employee> _emp;
        private readonly DataContext _db;
        public EmployeesController( IUnitOfWork<Employee> employee, DataContext db)
        {
            _emp = employee;
            _db = db;
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
            try
            {
                _emp.Entity.Update(employee);
                _emp.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
            try
            {
                _emp.Entity.Add(employee);
                _emp.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
    }
}
