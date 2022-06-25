using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.ReportingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Controllers
{
    public class IncentiveController : Controller
    {
        private readonly IUnitOfWork<Incentive> _seniorityIncentive;
        private readonly DataContext _db;
        private readonly IReporting _iReporting;

        public IncentiveController(IUnitOfWork<Incentive> seniorityIncentive,DataContext db, IReporting iReporting)
        {
            _seniorityIncentive = seniorityIncentive;
            _db = db;
            _iReporting = iReporting;
        }
        // GET: IncentiveController
        public ActionResult Index()
        {
            return View(_seniorityIncentive.Entity.GetAll());
        }

        public ActionResult GetIncentiveByEmpId(int id)
        {
            var _emps = _db.Employees.Where(m => m.IncentiveId == id).ToList();
            return View(_emps);
        }

        // GET: IncentiveController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncentiveController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Incentive incentive)
        {
            try
            {
                _seniorityIncentive.Entity.Add(incentive);
                _seniorityIncentive.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IncentiveController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.IncentiveId = id;

            return View(_seniorityIncentive.Entity.GetById(id));
        }

        // POST: IncentiveController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Incentive incentive)
        {
            try
            {
                _seniorityIncentive.Entity.Update(incentive);
                _seniorityIncentive.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // POST: IncentiveController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                _seniorityIncentive.Entity.Delete(id);
                _seniorityIncentive.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }
        public IActionResult DownloadReport(IFormCollection obj)
        {
            string reportname = $"Employees_Incentive_{Guid.NewGuid():N}.xlsx";
            var _empIncentive = _iReporting.GetIncentiveAndDeductionData();
            if (_empIncentive.Count() > 0)
            {
                var exportbytes = ReportToExcel.ExporttoExcel<Payroll.ReportingService.ReportingViewModels.IncentiveAndDeductionData>(_empIncentive, reportname);
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
