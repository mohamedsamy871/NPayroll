using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUnitOfWork<Rank> _rank;
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger, IUnitOfWork<Employee> employee, IUnitOfWork<Rank> rank)
        {
            _logger = logger;
            _emp = employee;
            _rank = rank;
        }

        public IActionResult Index()
        {
            var employees = _emp.Entity.GetAll().ToList();
            

            return View(employees);

        }

        public IActionResult AddEmployee()
        {
            return View();
        }

      
    }
}
