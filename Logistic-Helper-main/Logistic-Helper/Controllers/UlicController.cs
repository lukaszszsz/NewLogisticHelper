using LogisticHelper.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceReference1;

namespace LogisticHelper.Controllers
{
    public class UlicController : Controller
    {
        public TerytWs1Client connection()
        {
            ServiceReference1.TerytWs1Client client = new ServiceReference1.TerytWs1Client();
            /*  serviceteryt.TerytWs1Client client = new serviceteryt.TerytWs1Client();*/


            client.ClientCredentials.UserName.UserName = "Mariusz.Sobota";
            client.ClientCredentials.UserName.Password = "so6QT8ahG";
            client.OpenAsync().Wait();
            return client;
        }
        // GET: UlicController
        public ActionResult Index(List <UlicaDrzewo> streets)
        {
            


            return View(streets);
        } 
       

        // GET: UlicController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UlicController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UlicController/Create
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

        // GET: UlicController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UlicController/Edit/5
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

        // GET: UlicController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UlicController/Delete/5
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
