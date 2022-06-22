using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Employee> _emp;
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _db;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork<Employee> employee, DataContext db)
        {
            _logger = logger;
            _emp = employee;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetEmployees()
        {
            var employees = _db.Employees.Include(m => m.Department).Include(m => m.Incentive).Include(m => m.Rank).ToList();
            return View(employees);
        }

        public IActionResult EditEmployee(int id) {
            return View();
        }
        [HttpPost]
        public IActionResult EditEmployee(Employee employee)
        {
            _emp.Entity.Update(employee);
            _emp.Save();
            return RedirectToAction("GetEmployees");
        }
        public IActionResult AddEmployee()
        {
            ViewBag.departments = _db.Departments.ToList();
            ViewBag.rank = _db.Rank.ToList();
            ViewBag.incentive = _db.Incentive.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            _emp.Entity.Add(employee);
            _emp.Save();
            return RedirectToAction("GetEmployees");
        }



    }
}
