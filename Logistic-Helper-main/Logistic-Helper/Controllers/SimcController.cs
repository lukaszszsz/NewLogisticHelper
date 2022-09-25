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