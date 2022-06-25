using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payroll.ReportingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Controllers
{

    public class SalaryController : Controller
    {
        private readonly IUnitOfWork<SalaryManagement> _salary;
        private readonly DataContext _db;
        private readonly IReporting _iReporting;

        public SalaryController(IUnitOfWork<SalaryManagement> salary, DataContext db, IReporting iReporting)
        {
            _salary = salary;
            _db = db;
            _iReporting = iReporting;
        }
        // GET: SalaryController
        public ActionResult Index()
        {
            return View(_salary.Entity.GetAll());
        }

        // GET: SalaryController/Details/5
        public ActionResult Details(int id)
        {
            var _empWithSalary = _db.Employees.Where(m => m.SalaryManagementId == id).ToList();
            if (_empWithSalary.Count() > 0)
                return View(_empWithSalary);
            else
                return RedirectToAction(nameof(Index));

        }

        // GET: SalaryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalaryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SalaryManagement salaryManagement)
        {
            try
            {
                _salary.Entity.Add(salaryManagement);
                _salary.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalaryController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.SalaryId = id;
            return View(_salary.Entity.GetById(id));
        }

        // POST: SalaryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SalaryManagement salaryManagement)
        {
            try
            {
                _salary.Entity.Update(salaryManagement);
                _salary.Save();
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
                _salary.Entity.Delete(id);
                _salary.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        public IActionResult DownloadReport(IFormCollection obj)
        {
            string reportname = $"Employees_Salary_{Guid.NewGuid():N}.xlsx";
            var _emps = _iReporting.GetSalaryData();
            if (_emps.Count() > 0)
            {
                var exportbytes = ReportToExcel.ExporttoExcel<Payroll.ReportingService.ReportingViewModels.SalaryData>(_emps, reportname);
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
