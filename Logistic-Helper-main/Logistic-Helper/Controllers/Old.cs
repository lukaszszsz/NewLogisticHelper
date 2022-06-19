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
    public class TerytController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public TerytController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           

        }
      


          

        public IActionResult Index()
        {

            IEnumerable<Terc> Terc = _unitOfWork.Terc.GetAll();
            return View(Terc);
        }

        // GET: TerytController/Details/5
        public ActionResult Details(int id)
        {
           
            return View();
        }

        // GET: TerytController/Search
        public IActionResult Search()
        {
          
            return View();
        }


        //AUTOCOMPLETE FUNCTION FOR SEARCH PURPOSES
        //Function allows us to search data from table Terc, thanks to this,
        //we can find Gmina, Powiat and Wojewodztwo, just by  typing in textbox
        [HttpPost]
        public JsonResult AutoComplete(string input)
       {
            //create a list of all Terc elements
          IEnumerable<Terc> objTercList = _unitOfWork.Terc.GetAll();
            //scan them
            var search =  (from Terc in objTercList
                           where
                            Terc.NAZWA.StartsWith(input)
                           select new
                           {
                               label = Terc.NAZWA,
                               val = Terc.NAZWA,
                               
                           }).Take(5).ToList();
            return Json(search);
        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            ViewBag.Message = "Selected GMI Name: " + search;
            return View();
        }

        // POST: TerytController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
       

        // GET: TerytController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TerytController/Edit/5
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

        // GET: TerytController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TerytController/Delete/5
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
