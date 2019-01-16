using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.Areas.Zadania.Models;
using ToDo.Areas.Zadania.ViewModel;
using ToDo.Services.Zadania;

namespace ToDo.Areas.Zadania.Controllers
{
    public class ZadanieController : Controller
    {
        private IZadanieService Service { get; set; }
        private ViewConfig ViewConfig { get; set; }

        public ZadanieController(IZadanieService service)
        {
            Service = service;
            ViewConfig = new ViewConfig();
        }

        // GET: Zadania/Zadanie
        public ActionResult Index()
        {
            var ViewModel = new TasksVIewConfigModel
            {
                Tasks = Service.GetTasks(),
                ViewConfig = ViewConfig
            };

            return View(ViewModel);
        }

        public ActionResult Exit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                if (Service.Add(task))
                    return RedirectToAction("Index");
                else return Content("Błąd: " + Service.GetError());
            }

            ViewConfig.VisibleModal = "Add";

            var ViewModel = new TasksVIewConfigModel
            {
                Tasks = Service.GetTasks(),
                ViewConfig = ViewConfig
            };
            
            return View("Index", ViewModel);
        }
    }
}