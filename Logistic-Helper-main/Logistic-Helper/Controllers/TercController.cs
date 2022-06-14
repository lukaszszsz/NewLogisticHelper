using LogisticHelper.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LogisticHelper.DataAccess;
using LogisticHelper.Models;

namespace LogisticHelper.Controllers
{
    public class TercController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TercController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        // GET: BookCoverController

        public IActionResult Index()
        {

            IEnumerable<Terc> objUserList = _unitOfWork.Terc.GetAll();
            return View(objUserList);
        }

        // GET: TercsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TercsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TercsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TercsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TercsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TercsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TercsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
