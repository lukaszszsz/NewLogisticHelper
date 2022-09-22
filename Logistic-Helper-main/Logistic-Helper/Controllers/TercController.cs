using LogisticHelper.Models;
using LogisticHelper.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using ServiceReference1;
using System.IO.Compression;
using System.Xml;
//using System.Linq.Dynamic;
using System.Linq;
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

        //Connection function
        public TerytWs1Client connection()
        {
            ServiceReference1.TerytWs1Client client = new ServiceReference1.TerytWs1Client();
            /*  serviceteryt.TerytWs1Client client = new serviceteryt.TerytWs1Client();*/
            

            client.ClientCredentials.UserName.UserName = "Mariusz.Sobota";
            client.ClientCredentials.UserName.Password = "so6QT8ahG";
            client.OpenAsync().Wait();
            return client;
        }

        //Function which allows for checking value of nodes

        string handleNull(string xAfter, string xBefore)
        {
            if (xAfter == "")
                xAfter = xBefore;
            return xAfter;

        }
        string nullBefore(string xBefore)
        {
            if (xBefore == "")
               xBefore = "0";
            return xBefore;

        }
       
        [HttpPost]
        public object Search(string search)
        {
            TerytWs1Client client = connection();

            IEnumerable<Terc> TercObjList = _unitOfWork.Terc.GetAll();
           

            //Teec is updated once a year

            DateTime s = DateTime.Now;
            string startingDate = s.ToShortDateString();

            DateTime e = s.AddYears(-1);
            string endingDate = e.ToShortDateString();

          

            //Here we have XML compressed to ZIP, now figure out how to suck it to db
            //FileChange is a variable in which is file, it doesnt exist phyisically on disc, how to unzip it?
            var UpdateFile = client.PobierzZmianyTercUrzedowyAsync(e, s);
            PlikZmiany fileChange = UpdateFile.Result;
            string fileName = fileChange.nazwa_pliku;
            string zipContent = fileChange.plik_zawartosc;
            string scenario = fileChange.opis;

            //working decoding from base64 to zip
            Chilkat.BinData zipData = new Chilkat.BinData();
            bool success = zipData.AppendEncoded(zipContent, "base64");
            success = zipData.WriteFile(Directory.GetCurrentDirectory() + @"/File/out.zip");





            FileStream fs = new FileStream("./File/out.zip", FileMode.Open);
            ZipArchive zipArchive = new ZipArchive(fs);
            string destination = Directory.GetCurrentDirectory() + @"/File/";
            zipArchive.ExtractToDirectory(destination);
            ViewBag.Message = "Selected SS Name: " + zipContent;
            //  ViewBag.Message = "Selected GMI Name: " + search;

            //Above works, need smth to read xml
           
           
            XmlDocument doc = new XmlDocument();

            doc.Load(Directory.GetCurrentDirectory() + "/File/TERC_Urzedowy_zmiany_"+ endingDate + "_"+ startingDate + ".xml");
            var xList = doc.SelectNodes("/zmiany/zmiana"); // Znajdź węzeł zmiany, w której znajdują się informacje dot. modernizacji
            foreach(XmlNode xNode in xList)
            {
                var xTypKorekty = xNode.SelectSingleNode("TypKorekty");
                switch(xTypKorekty.InnerText)
                {
                    //Dodanie jednostki administracyjnej
                    case "D":
                        string xWojPo = (xNode.SelectSingleNode("WojPo").InnerText);
                        string xPowPo = (xNode.SelectSingleNode("PowPo").InnerText);
                        string xGmiPo = (xNode.SelectSingleNode("GmiPo").InnerText);
                        string xRodzPo = (xNode.SelectSingleNode("RodzPo").InnerText);
                        string xNazwaPo = xNode.SelectSingleNode("NazwaPo").InnerText;
                        string xNazwaDodatkowaPo = xNode.SelectSingleNode("NazwaDodatkowaPo").InnerText;
                        string xStanPo = (xNode.SelectSingleNode("StanPo").InnerText);
                        var query = new Terc
                        {
                            WOJ = xWojPo,
                            POW = xPowPo,
                            GMI = xGmiPo,
                            RODZ = xRodzPo,
                            NAZWA = xNazwaPo,
                            NAZWA_DOD = xNazwaDodatkowaPo,
                            STAN_NA = xStanPo
                        };
                        _unitOfWork.Terc.Add(query);
                        _unitOfWork.Save();

                        break;

                    //Delete old Jednostka Administracyjna
                    case "U":

                        var xWojPrzed = (xNode.SelectSingleNode("WojPrzed").InnerText);
                        var xPowPrzed = (xNode.SelectSingleNode("PowPrzed").InnerText);
                        var xGmiPrzed = (xNode.SelectSingleNode("GmiPrzed").InnerText);
                        var xRodzPrzed = (xNode.SelectSingleNode("RodzPrzed").InnerText);
                        var xNazwaPrzed = xNode.SelectSingleNode("NazwaPrzed").InnerText;
                        var xNazwaDodatkowaPrzed = xNode.SelectSingleNode("NazwaDodatkowaPrzed").InnerText;
                        var xStanPrzed = xNode.SelectSingleNode("Stan_Na").InnerText;
                        

                        query = (from toj in TercObjList where toj.WOJ ==  xWojPrzed && toj.POW == xPowPrzed && toj.GMI == xGmiPrzed && toj.RODZ == xRodzPrzed && toj.NAZWA == xNazwaPrzed select toj).First();
                        _unitOfWork.Terc.Remove(query);
                        _unitOfWork.Save();

                        break;

                    default:
                        break;


                    //Update current Jednostka Administracyjne
                    case "M":
                         xWojPrzed = (xNode.SelectSingleNode("WojPrzed").InnerText);
                         xPowPrzed = ((xNode.SelectSingleNode("PowPrzed").InnerText));
                         xGmiPrzed = (xNode.SelectSingleNode("GmiPrzed").InnerText);
                         xRodzPrzed = (xNode.SelectSingleNode("RodzPrzed").InnerText);
                         xNazwaPrzed = xNode.SelectSingleNode("NazwaPrzed").InnerText;
                         xNazwaDodatkowaPrzed = xNode.SelectSingleNode("NazwaDodatkowaPrzed").InnerText;
                         xStanPrzed = xNode.SelectSingleNode("StanPrzed").InnerText;



                         xWojPo = handleNull((xNode.SelectSingleNode("WojPo").InnerText), xWojPrzed);
                         xPowPo = handleNull((xNode.SelectSingleNode("PowPo").InnerText), xPowPrzed);

                         xGmiPo = handleNull((xNode.SelectSingleNode("GmiPo").InnerText), xGmiPrzed);
                         xRodzPo = handleNull((xNode.SelectSingleNode("RodzPo").InnerText), xRodzPrzed);
                         xNazwaPo = handleNull(xNode.SelectSingleNode("NazwaPo").InnerText, xNazwaPrzed);
                         xNazwaDodatkowaPo = handleNull(xNode.SelectSingleNode("NazwaDodatkowaPo").InnerText, xNazwaDodatkowaPrzed);
                         xStanPo = (xNode.SelectSingleNode("StanPo").InnerText);


                        // etc

                        // Jak zrobić żeby ignorował wartości NULL ??

                        //How to use string as requirement?
                        if (xNazwaDodatkowaPrzed.StartsWith("gmina") || xNazwaDodatkowaPrzed.Contains("miasto"))
                        {
                            query = (from toj in TercObjList where toj.WOJ == xWojPrzed && toj.POW != null && toj.POW == xPowPrzed && toj.GMI == xGmiPrzed && toj.RODZ == xRodzPrzed && toj.NAZWA == xNazwaPrzed select toj).First();
                            query.WOJ = xWojPo;
                            query.POW = xPowPo;
                            query.GMI = xGmiPo;
                            query.RODZ = xRodzPo;
                            query.NAZWA = xNazwaPo;
                            query.NAZWA_DOD = xNazwaDodatkowaPo;
                            query.STAN_NA = xStanPo;

                            _unitOfWork.Terc.Update(query);
                        }
                        else if(xNazwaDodatkowaPrzed.StartsWith("powiat"))
                        {
                            query = (from toj in TercObjList where toj.WOJ == xWojPrzed  && toj.POW == xPowPrzed  && toj.NAZWA == xNazwaPrzed select toj).First();
                            query.WOJ = xWojPo;
                            query.POW = xPowPo;
                            query.NAZWA = xNazwaPo;
                            query.NAZWA_DOD = xNazwaDodatkowaPo;
                            query.STAN_NA = xStanPo;

                            _unitOfWork.Terc.Update(query);
                        }
                        else if (xNazwaDodatkowaPrzed.StartsWith("województwo"))
                        {
                            
                            query = (from toj in TercObjList where toj.WOJ == xWojPrzed && toj.NAZWA == xNazwaPrzed select toj).First();
                            
                            query.WOJ = xWojPo;
                           
                            query.NAZWA = xNazwaPo;
                            query.NAZWA_DOD = xNazwaDodatkowaPo;
                            query.STAN_NA = xStanPo;
                            
                            _unitOfWork.Terc.Update(query);

                        }
                        
                        _unitOfWork.Save();
                        break;

                      
                //WORKS, now polishing this little boy!!!

                }

                // wezły


            }






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
