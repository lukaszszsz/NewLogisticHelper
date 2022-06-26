using LogisticHelper.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LogisticHelper.DataAccess;
using LogisticHelper.Models;
using ServiceReference1;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;
//using ServiceReference1;

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
        public IActionResult Search()
        {

            IEnumerable<Terc> objUserList = _unitOfWork.Terc.GetAll();
            
            return View(objUserList);
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
            var search = (from Terc in objTercList
                          where
                           Terc.NAZWA.StartsWith(input)
                          select new
                          {
                              label = Terc.NAZWA,
                              val = Terc.STAN_NA,

                          }).Take(5).ToList();
            return Json(search);
        }
        [HttpPost]
        public ActionResult Search(string search)
        {
            
            ServiceReference1.TerytWs1Client client = new ServiceReference1.TerytWs1Client();
            /*  serviceteryt.TerytWs1Client client = new serviceteryt.TerytWs1Client();*/


            client.ClientCredentials.UserName.UserName = "Mariusz.Sobota";
            client.ClientCredentials.UserName.Password = "so6QT8ahG";
            client.OpenAsync().Wait();

            DateTime date = new DateTime(2020, 06, 06);

            //Here we have XML compressed to ZIP, now figure out how to suck it to db
            //FileChange is a variable in which is file, it doesnt exist phyisically on disc, how to unzip it?
            var UpdateFile = client.PobierzZmianyTercUrzedowyAsync(date, DateTime.Now);
            PlikZmiany fileChange = UpdateFile.Result;
            string fileName = fileChange.nazwa_pliku  ;
            string zipContent = fileChange.plik_zawartosc;
            string scenario = fileChange.opis;


            Chilkat.BinData zipData = new Chilkat.BinData();
            bool success = zipData.AppendEncoded(zipContent, "base64");
            success = zipData.WriteFile("N:/Programowanie/Inzynierka/PI2023/NewLogistic/NewLogisticHelper/Logistic-Helper-main/Logistic-Helper/File/out.zip");
          
            //downloading file

            /*  var webClient = new WebClient();
              webClient.Credentials = CredentialCache.DefaultCredentials;
              webClient.DownloadFile("https://uslugaterytws1.stat.gov.pl/wsdl/terytws1.wsdl", fileName);
             */



            FileStream fs = new FileStream(fileName, FileMode.Open);
            ZipArchive zipArchive = new ZipArchive(fs);
               /* string destination = @"N:\Programowanie\Inzynierka\PI2023\NewLogistic\NewLogisticHelper\Logistic-Helper-main\Logistic-Helper\File";
              zipArchive.ExtractToDirectory(destination);*/
            ViewBag.Message = "Selected SS Name: " + zipContent;
          //  ViewBag.Message = "Selected GMI Name: " + search;




            return View();
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
