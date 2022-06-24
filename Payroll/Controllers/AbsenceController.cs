using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Controllers
{
    public class AbsenceController : Controller
    {
        private readonly IUnitOfWork<AbsenceConditions> _absence;

        public AbsenceController(IUnitOfWork<AbsenceConditions> absence)
        {
            _absence = absence;
        }
        // GET: AbsenceController
        public ActionResult Index()
        {
            return View(_absence.Entity.GetAll());
        }

        // GET: AbsenceController/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: AbsenceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AbsenceConditions absenceConditions)
        {
            try
            {
                _absence.Entity.Add(absenceConditions);
                _absence.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: AbsenceController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.AbsenceId = id;
            return View(_absence.Entity.GetById(id));
        }
        // POST: AbsenceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AbsenceConditions absenceConditions)
        {
            try
            {
                _absence.Entity.Update(absenceConditions);
                _absence.Save();
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
                _absence.Entity.Delete(id);
                _absence.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
