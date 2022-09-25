using LogisticHelper.Models;
using LogisticHelper.Repository.IRepository;
using LogisticHelper.Repository;
using Microsoft.AspNetCore.Mvc;
using ServiceReference1;
using System.IO.Compression;
using System.Xml;
using Quartz;
using Quartz.Impl;
namespace LogisticHelper.Controllers
{
    public class SimcController : Controller
    {
     
        private readonly IUnitOfWork _unitOfWork;

    
        
        public SimcController(IUnitOfWork unitOfWork)
        {
           
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Simc> objSimcList = _unitOfWork.Simc.GetAll();
            return View(objSimcList);
        }

        public IActionResult Search()
        {

            IEnumerable<Simc> objSimcList = _unitOfWork.Simc.GetAll();

            return View(objSimcList);
        }
        [HttpPost]
        public JsonResult AutoComplete(string input)
        {
            //create a list of all Terc elements
            IEnumerable<Simc> objSimcList = _unitOfWork.Simc.GetAll();
            IEnumerable<Terc> objTercList = _unitOfWork.Terc.GetAll();
            //scan them
            var search = (from Simc in objTercList
                          where
                           Simc.NAZWA.StartsWith(input) 
                          select new
                          {

                              label = Simc.NAZWA,                            
                              val = Simc.STAN_NA,

                          }).Take(5).ToList();
            return Json(search);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return View();
        }
    }
}