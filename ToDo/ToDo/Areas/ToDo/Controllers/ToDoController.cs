using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ToDo.Areas.ToDo.Models.Tasks;
using ToDo.Areas.ToDo.Models.View;

namespace ToDo.Areas.ToDo.Controllers
{
    public class ToDoController : Controller {
        IListOfTasks Tasks = ListOfTasks.GetInstance();
        Config ViewConfig = Config.GetInstance();


        // GET: ToDo/ToDo
        public ActionResult Index()
        {
            PrepareViewData();

            return View();
        }

        // GET: ToDo/ToDo/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: ToDo/ToDo/Create
        public ActionResult Create() {
            Task tmp = new Task {
                Topic = "Zadanie",
                Description = "test"
            };
            Tasks.Add(tmp);

            PrepareViewData();

            return View( "Index" );
        }

        // POST: ToDo/ToDo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection) {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDo/ToDo/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: ToDo/ToDo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection) {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDo/ToDo/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: ToDo/ToDo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {

            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Exit() {
            Tasks.Clear();
            return View("Exit");
        }

        [NonAction]
        public void PrepareViewData()
        {

            ViewData["Tasks"] = GetOrderedList();
            ViewData["ViewConfig"] = ViewConfig;
            ViewData["AvailableSites"] =
                Convert.ToInt32(
                    Math.Floor(
                        Convert.ToDecimal(
                            (Tasks.AllElements() - 1) / ViewConfig.ActualPerSite
                            )
                        )
                    );
        }

        [NonAction] 
        public IEnumerable<Task> GetOrderedList() {
            return (from tasks in Tasks.GetList()
                   orderby tasks.ID
                   select tasks).Skip( ViewConfig.SkipPages() ).Take(ViewConfig.ActualPerSite);
        }

        [HttpPost]
        public JsonResult ElementsPerSite(string element)
        {
            ViewConfig.ActualPerSite = Convert.ToInt32(element);

            return Json("OK");
        }

        [HttpPost]
        public JsonResult ActualSite(string element)
        {
            ViewConfig.ActualSite = Convert.ToInt32(element);

            return Json("OK");
        }
    }
}
