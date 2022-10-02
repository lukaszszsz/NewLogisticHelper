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
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            //Enumerable<Simc> objSimcList = _unitOfWork.Simc.GetAll();
            

            return View();
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
        public string AutoComplete(string input)
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

                            label = Simc.NAZWA,
                            val = Simc.NAZWA,
                            woj = Simc.WOJ,
                            pow = Simc.POW,
                            gmi = Simc.GMI,
                            rodz_gmi = Simc.RODZ_GMI,
                            rm = Simc.RM,
                            mz = Simc.MZ,
                            nazwa = Simc.NAZWA,

                            sym = Simc.SYM,
                            sympod = Simc.SYMPOD,
                            stan_na = Simc.STAN_NA,
    


                          }).Take(5).ToList();

 
            string jsson = JsonConvert.SerializeObject(search);
            return jsson ;
        }

        [HttpPost]
        public async Task<ActionResult> SearchAsync(string search)
      {
            var client = connection();
            //Zastanowić się jak rozgryźć wyszukiwarkę, 2 autocomplete? Jedna ze stringiem dla użytkownika, jedna dla sprzętu?
            var ss = AutoComplete(search);
           dynamic jsoon = JsonConvert.DeserializeObject(ss);
            ServiceReference1.Miejscowosc[] village;

            foreach (var obj in jsoon)
            {
                string objNazwa = obj.nazwa;
                string objSym= obj.sym;
              //  jsoon.Add(obj);
                village = await client.WyszukajMiejscowoscAsync(objNazwa,objSym );
                
            }
            //WORKS!!!!!
            //Now have to write correct instruction to show data, but the principal of it works 
            //Whole JSON is being send, so np to choose data
            string info = jsoon.First.pow;
          


            //znajdz
           

           

            return View();
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