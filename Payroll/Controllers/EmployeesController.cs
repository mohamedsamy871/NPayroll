using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var employees = _db.Employees.Include(m => m.Department).Include(m => m.Incentive).Include(m => m.Rank).ToList();
            return View(employees);
        }

        public IActionResult EditEmployee(int id)
        {
            return View();
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
            ViewBag.departments = _db.Departments.ToList();
            ViewBag.rank = _db.Rank.ToList();
            ViewBag.incentive = _db.Incentive.ToList();
            return View();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
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
