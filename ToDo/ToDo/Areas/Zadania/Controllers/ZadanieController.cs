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

        public ZadanieController(IZadanieService service)
        {
            Service = service;
        }

        protected override void OnException(ExceptionContext filterContext)
        {            
            var ViewModel = new TasksVIewConfigModel
            {
                Tasks = Service.GetTasks(),
                ViewConfig = ViewConfig.GetInstance(),
                Exception = filterContext.Exception.Message
            };

            filterContext.Result = View("Index", ViewModel);
            filterContext.ExceptionHandled = true;
        }

        // GET: Zadania/Zadanie
        public ActionResult Index()
        {
            ViewConfig.ClearVisibleModal();

            var ViewModel = new TasksVIewConfigModel
            {
                Tasks = Service.GetTasks(),
                ViewConfig = ViewConfig.GetInstance(),
                Exception = String.Empty
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
            ViewConfig.SetVisibleModal("Add");

            if (ModelState.IsValid)
            {
                if (Service.Add(task))
                {
                    ViewConfig.ClearVisibleModal();
                    return RedirectToAction("Index");
                }
                else return Content("Błąd: " + Service.GetError());
            }


            var ViewModel = new TasksVIewConfigModel
            {
                Tasks = Service.GetTasks(),
                ViewConfig = ViewConfig.GetInstance()
            };
            
            return View("Index", ViewModel);
        }

        public ActionResult Delete(int id)
        {
            Service.Remove(id);
            return RedirectToAction("Index");
        }

        public ActionResult Description(int id)
        {
            ViewConfig.SetVisibleModal("Description");
            ViewConfig.SetDescriptionTaskId(id);

            var ViewModel = new TasksVIewConfigModel
            {
                Tasks = Service.GetTasks(),
                ViewConfig = ViewConfig.GetInstance()
            };

            return View("Index", ViewModel);
        }
    }
}