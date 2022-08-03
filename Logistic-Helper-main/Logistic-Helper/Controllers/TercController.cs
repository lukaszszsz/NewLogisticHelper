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
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
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
            IEnumerable<Terc> objTercList = _unitOfWork.Terc.GetAll();

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

            //working decoding from base64 to zip
            Chilkat.BinData zipData = new Chilkat.BinData();
            bool success = zipData.AppendEncoded(zipContent, "base64");
            success = zipData.WriteFile(Directory.GetCurrentDirectory() + @"/File/out.zip");
          
            



            FileStream fs = new FileStream("./File/out.zip", FileMode.Open);
            ZipArchive zipArchive = new ZipArchive(fs);
               string destination = Directory.GetCurrentDirectory()+@"/File/";
              zipArchive.ExtractToDirectory(destination);
            ViewBag.Message = "Selected SS Name: " + zipContent;
            //  ViewBag.Message = "Selected GMI Name: " + search;

            //Above works, need smth to read xml
            XmlReader reader = XmlReader.Create(Directory.GetCurrentDirectory() + @"/File/TERC_Urzedowy_zmiany_2020-06-06_2022-07-31.xml");
            //XDocument reader = XDocument.Load(Directory.GetCurrentDirectory() + @"/File/TERC_Urzedowy_zmiany_2020-06-06_2022-07-31.xml");
            /*XmlDocument reader = new XmlDocument();
            reader.Read(Directory.GetCurrentDirectory() + "/File/TERC_Urzedowy_zmiany_2020-06-06_2022-08-02.xml"); ;
            var line = reader.ToString();*/

            //Odczytaj "zmiana".
            // Sprawdź jaki rodzaj korekty
            //JEŚLI "M"
            //Stwórz słownik, <string, string>
            //var zmiana= new Dictionary<string, string>(){
            /*    { "WOJ", "WojPrzed, WojPo"},
                  { "POW", "PowPrzed , PowPo "},
                  { "GMI", "GmiPrzed , GmiPo"}
                  { "RODZ", "RodzPrzed , RodzPo "},
                  { "NAZWA", "NazwaPrzed , NazwaPo "},
                  { "NAZWA_DOD", "NazwaDodatkowaPrzed  , NazwaDodatkowaPo"}
                  { "STAN_NA", "StanPrzed , StanPo"}

            -------------------------
             Policz ilość value
            availableStats.Values.Sum(x => x.Count)

            https://www.tutorialace.com/csharp-dictionary-foreach/
            ----------------------
            JEŚLI count NotNull>1, weź 2 wartość
            https://codesamplez.com/database/insert-update-delete-linq-to-sql
            
            Pętla przez słownik, weź wartość, jeśli [1] == null, weź [0]

          };*/

            // ----------------------XDocument(LINQ-----------------------------*/
            /*  var q = from zmiany in reader.Descendants("zmiana")
                      select new
                      {
                          qTypZmiany = (string)zmiany.Element("TypKorekty"),
                         *//* qWojPrzed = (string?)zmiany.Element("WojPrzed"),
                          qPowPrzed = (string?)zmiany.Element("PowPrzed"),
                          qGmiPrzed = (string?)zmiany.Element("GmiPrzed"),
                         qRodzPrzed = (string?)zmiany.Element("RodzPrzed"),
                          qNazwaPrzed = (string?)zmiany.Element("NazwaPrzed"),
                          qNazwaDodatkowaPrzed = (string?)zmiany.Element("NazwaDodatkowaPrzed"),*//*

                          qWojPo = (string)zmiany.Element("WojPo ") ?? (string)zmiany.Element("WojPrzed"),
                          qPowPo = (string)zmiany.Element("PowPo ") ?? (string)zmiany.Element("PowPrzed"),
                          qGmiPo = (string)zmiany.Element("GmiPo ") ?? (string)zmiany.Element("GmiPrzed"),
                          qRodzPo = (string)zmiany.Element("RodzPo ") ?? (string)zmiany.Element("RodzPrzed"),
                          qNazwaPo = (string)zmiany.Element("NazwaPo") ?? (string)zmiany.Element("NazwaPrzed"),
                          qNazwaDodatkowaPo = (string?)zmiany.Element("NazwaDodatkowaPo") ?? (string)zmiany.Element("NazwaDodatkowaPrzed"),

                      };
          var find = context.Ter*/
            var line = reader.ToString();

            while (reader.Read())
            {
                switch (linia)
                {
                    case "M":
                        {
                            var zmiana = new Dictionary<string, string>();
                            int i = 1;
                            foreach (var obj in objTercList)
                            {
                                zmiana.Add(obj.ToString(), reader.ChildNodes[i].ToString());
                                zmiana.Add(obj.ToString(), reader.ChildNodes[i + 7].ToString());
                                i++;
                            };


                            /*{ "POW", "reader.ChildNodes[2].ToString() , reader.ChildNodes[9].ToString()"},
                            { "GMI", "reader.ChildNodes[3].ToString() , reader.ChildNodes[10].ToString()"},
                            { "RODZ", "reader.ChildNodes[4].ToString() , reader.ChildNodes[11].ToString() "},
                            { "NAZWA", reader.ChildNodes[5].ToString() , reader.ChildNodes[12].ToString() },
                            { "NAZWA_DOD", "reader.ChildNodes[6].ToString()  , NazwaDodatkowaPo"},
                            { "STAN_NA", "StanPrzed , StanPo"}*/
                            break;
                        }
                }

                   
                }
            

                                

                                // XmlReader reader = XmlReader(fs);
                    /*
                                   if(reader.NodeType == XmlNodeType.Element && reader.Name == "zmiana")
                                   {
                                       string val = reader.Value;
                                   }*/
                                /*   switch (line)
                                       {
                                           case "M":
                                               {
                                                   var change = new Dictionary<string, string>()
                                                   {

                                                   }*/

                                /*for (int i = 0; i < reader.ChildNodes.Count; i++)
                                {


                                    *//*if (reader.ChildNodes[i].InnerText == null || reader.ChildNodes[i].Name.EndsWith("ed") )
                                        continue;
                                    else
                                    {
                                        string valueToChange = reader.ChildNodes[i].InnerText;

                                    }*//*
                                }*/
                            
                        
                    /*case "Location":
                        Console.WriteLine("Your Location is : " + reader.ReadString());
                        break;*/
                
            
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
