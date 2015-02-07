using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeWarriors.IITDU.Controllers
{
    public class MapController : Controller
    {
        //
        // GET: /Map/

        public ActionResult ShowMap(string location)
        {
            ViewBag.location = location;
            return View();
        }

    }
}
