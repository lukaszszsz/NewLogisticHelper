using LogisticHelper.Models;
using LogisticHelper.Repository.IRepository;
using LogisticHelper.Repository;
using Microsoft.AspNetCore.Mvc;
using ServiceReference1;
using System.IO.Compression;
using System.Xml;
using Quartz;
using Quartz.Impl;
using System.Linq;

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
        public TerytWs1Client connection()
        {
            ServiceReference1.TerytWs1Client client = new ServiceReference1.TerytWs1Client();
            /*  serviceteryt.TerytWs1Client client = new serviceteryt.TerytWs1Client();*/


            client.ClientCredentials.UserName.UserName = "Mariusz.Sobota";
            client.ClientCredentials.UserName.Password = "so6QT8ahG";
            client.OpenAsync().Wait();
            return client;
        }
        [HttpPost]
        public JsonResult AutoComplete(string input)
        {
            TerytWs1Client client = connection();

            //create a list of all Terc elements
            IEnumerable<Simc> objSimcList = _unitOfWork.Simc.GetAll();

            //scan them
            var search = (from Simc in objSimcList
                          where
                           Simc.NAZWA.StartsWith(input) 
                          select new
                          {

                              label = client.PobierzListeMiejscowosciWRodzajuGminyAsync(Simc.WOJ, Simc.POW, Simc.GMI, Simc.RODZ_GMI, Convert.ToDateTime(Simc.STAN_NA)).Result,                      
                              val = Simc.STAN_NA,

                          }).Take(10).ToList();

            var result = search[0].label.Take(10).ToList();
            var final = (from Simc in result
                         where
     Simc.Nazwa.StartsWith(input)
                         select new
                         {
                             label = Simc.Nazwa +" " + Simc.Powiat ,
                             val = Simc.Nazwa,

                         }).Take(5).ToList();
            return Json(final) ;
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