﻿using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.ReportingService;
using Payroll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IUnitOfWork<Attendance> _attendance;
        private readonly DataContext _db;
        private readonly IReporting _iReporting;
        public AttendanceController(IUnitOfWork<Attendance> attendance,
            DataContext db,IReporting iReporting)
        {
            _attendance = attendance;
            _db = db;
            _iReporting = iReporting;
        }
        // GET: AttendanceController
        public ActionResult Index()
        {
            var AttendanceInDb = _db.Attendance.Include(m => m.Employee).Include(m => m.AbsenceConditions).ToList();
            return View(AttendanceInDb);
        }

        // GET: AttendanceController/Details/5
        public ActionResult Details(int id)
        {
            var emp = _db.Employees.Find(id);
            ViewBag.EmpName = emp.Name;
            var AttendanceForEmployee = _db.Attendance.Where(m => m.EmployeeId == id).Include(m=>m.Employee).Include(m=>m.AbsenceConditions).ToList();
            return View(AttendanceForEmployee);
        }

        // GET: AttendanceController/Create
        public ActionResult Create()
        {
            EmployeesAttendance _empAttendance = new EmployeesAttendance()
            {
                Attendance = new Attendance(),
                Employees = _db.Employees.ToList(),
                AbsenceConditions = _db.AbsenceConditions.ToList()
            };
            return View(_empAttendance);
        }

        // POST: AttendanceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Attendance attendance)
        {
            _attendance.Entity.Add(attendance);
            _attendance.Save();
            return RedirectToAction(nameof(Index));
        }
        // GET: AttendanceController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.AttendanceId = id;
            var AttendanceInDb = _attendance.Entity.GetById(id);
            EmployeesAttendance _empAttendance = new EmployeesAttendance()
            {
                Attendance = AttendanceInDb,
                Employees = _db.Employees.ToList(),
                AbsenceConditions = _db.AbsenceConditions.ToList()
            };
            return View(_empAttendance);
        }

        // POST: AttendanceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Attendance attendance)
        {
            try
            {
                _attendance.Entity.Update(attendance);
                _attendance.Save();
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
                _attendance.Entity.Delete(id);
                _attendance.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }
        public IActionResult DownloadReport(IFormCollection obj)
        {
            string reportname = $"Employees_Attendance_{Guid.NewGuid():N}.xlsx";
            var _emps = _iReporting.GetAttendanceData();
            if (_emps.Count() > 0)
            {
                var exportbytes = ReportToExcel.ExporttoExcel<Payroll.ReportingService.ReportingViewModels.AttendanceData>(_emps, reportname);
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
