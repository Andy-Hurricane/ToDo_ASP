using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.Areas.ToDo.Models.Tasks;

namespace ToDo.Areas.ToDo.Controllers
{
    public class ToDoController : Controller {
        IListOfTasks Tasks = ListOfTasks.GetInstance();

        // GET: ToDo/ToDo
        public ActionResult Index() {
            return View( GetOrderedList() );
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

            return View( "Index", GetOrderedList() );
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

        [NonAction] 
        public IEnumerable<Task> GetOrderedList() {
            return from tasks in Tasks.GetList()
                   orderby tasks.ID
                   select tasks;
        }
    }
}
