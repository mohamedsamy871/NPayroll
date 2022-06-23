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
        public IActionResult GetDepartmentById(int id)
        {
            var _emps = _db.Employees.Where(m => m.DepartmentId == id).ToList();

            return View(_emps);

        }
        public IActionResult EditDepartment(int id)
        {
            ViewBag.DepartmentId = id;
            return View(_department.Entity.GetById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDepartment(Department department)
        {
            try
            {
                _department.Entity.Update(department);
                _department.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDepartment(Department department)
        {
            _department.Entity.Add(department);
            _department.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _department.Entity.Delete(id);
            _department.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
