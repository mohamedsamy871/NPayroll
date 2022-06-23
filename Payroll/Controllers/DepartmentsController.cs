using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork<Department> _department;
        private readonly DataContext _db;

        public DepartmentsController(IUnitOfWork<Department> department, DataContext db)
        {
            _department = department;
            _db = db;
        }
        public IActionResult Index()
        {
            
            return View(_department.Entity.GetAll());
        }

    }
}
