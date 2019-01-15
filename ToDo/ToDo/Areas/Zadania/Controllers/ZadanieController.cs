using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDo.Areas.Zadania.Controllers
{
    public class ZadanieController : Controller
    {
        // GET: Zadania/Zadanie
        public ActionResult Index()
        {
            return View();
        }
    }
}