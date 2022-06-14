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
        // GET: TerytController
        /*  public IActionResult Index()
          {
              IEnumerable<Terc> objTercList = _unitOfWork.Terc.GetAll();

              //var cos =  client.PobierzListeMiejscowosciWGminieAsync( "śląskie", "gliwicki","gierałtowice",DateTime.Now).Result;
              //LINQ To get correct data


              return View(objTercList);



          }*/

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

        [HttpPost]
        public JsonResult AutoComplete(string input)
       {

            //_unitOfWork.User.GetFirstOrDefault(u => u.ID == id);
          IEnumerable<Terc> objTercList = _unitOfWork.Terc.GetAll();

            var search =  (from Terc in objTercList
                           where
                            Terc.NAZWA.StartsWith(input)
                           select new
                           {
                               label = Terc.NAZWA,
                               val = Terc.NAZWA
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
        //Trzeba zrobić listę ze wszystkimi miastami :/
       

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
