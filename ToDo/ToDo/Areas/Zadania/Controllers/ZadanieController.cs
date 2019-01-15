using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.Services.Zadania;

namespace ToDo.Areas.Zadania.Controllers
{
    public class ZadanieController : Controller
    {
        private IZadanieService Service {get;set;}

        public ZadanieController(IZadanieService service)
        {
            Service = service;
        }

        // GET: Zadania/Zadanie
        public ActionResult Index()
        {
            return View();
        }
    }
}