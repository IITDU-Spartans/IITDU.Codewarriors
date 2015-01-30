using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dummy()
        {
            DatabaseContext databaseContext = new DatabaseContext();
            databaseContext.Database.Delete();
            databaseContext.Database.Create();
            return RedirectToAction("Index");
        }

    }
}
