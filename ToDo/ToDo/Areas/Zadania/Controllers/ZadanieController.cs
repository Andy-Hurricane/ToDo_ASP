using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.Areas.Zadania.Models;
using ToDo.Areas.Zadania.ViewModel;
using ToDo.Services.Export;
using ToDo.Services.Zadania;

namespace ToDo.Areas.Zadania.Controllers
{
    public class ZadanieController : Controller
    {
        private IZadanieService Service { get; set; }
        private delegate bool CreateAction(Task myTask);

        public ZadanieController(IZadanieService service)
        {
            Service = service;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var ModelView = TasksViewConfigModel.PrepareForException(Service, filterContext);
            filterContext.Result = View("Index", ModelView);
            filterContext.ExceptionHandled = true;
        }

        // GET: Zadania/Zadanie
        public ActionResult Index()
        {
            ViewConfig.ClearVisibleModal();

            var ViewModel = TasksViewConfigModel.PrepareForIndex(Service);

            return View(ViewModel);
        }

        public ActionResult Exit()
        {
            return View();
        }
        public ActionResult List()
        {
            ViewConfig.GetInstance().SetListView();
            return RedirectToAction("Index");
        }
        public ActionResult Tails()
        {
            ViewConfig.GetInstance().SetTailView();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(Task task)
        {
            bool isAdd = (task.Id == 0);

            ViewConfig.SetVisibleModal( isAdd 
                ? "Add"
                : "Edit"
            );

            if (ModelState.IsValid)
            {
                CreateAction doIt = isAdd
                    ? new CreateAction(Service.Add)
                    : new CreateAction(Service.Edit);

                if (doIt(task))
                {
                    ViewConfig.ClearVisibleModal();
                    if (isAdd)
                        Service.Sort(SortFilter.NULL);
                    return RedirectToAction("Index");
                }
                else return HttpNotFound("Błąd: " + Service.GetError());
            }


            var ViewModel = TasksViewConfigModel.PrepareForIndex(Service);
            
            return View("Index", ViewModel);
        }

        public ActionResult Delete(int id)
        {
            Service.Remove(id);
            Service.Sort(SortFilter.NULL);
            return RedirectToAction("Index");
        }

        public ActionResult Description(int id)
        {
            var ViewModel = TasksViewConfigModel.PrepareForDescription(Service, id);

            return View("Index", ViewModel);
        }

        public ActionResult Edit(int id)
        {
            var ViewModel = TasksViewConfigModel.PrepareForEdit(Service, id);

            return View("Index", ViewModel);
        }

        public ActionResult SortByTopic()
        {
            Service.Sort(SortFilter.TOPIC);
            return RedirectToAction("Index");
        }

        public ActionResult SortByEnd()
        {
            Service.Sort(SortFilter.END);
            return RedirectToAction("Index");
        }

        public ActionResult SortByPriority()
        {
            Service.Sort(SortFilter.PRIORITY);
            return RedirectToAction("Index");
        }

        public ActionResult SortByStart()
        {
            Service.Sort(SortFilter.START);
            return RedirectToAction("Index");
        }

        public ActionResult SortByStatus()
        {
            Service.Sort(SortFilter.STATUS);
            return RedirectToAction("Index");
        }

        public ActionResult SwapPrevious(int id)
        {
            Service.SwapPrevious(id);
            return RedirectToAction("Index");
        }

        public ActionResult SwapNext(int id)
        {
            Service.SwapNext(id);
            return RedirectToAction("Index");
        }

        public ActionResult ChangeTasksPerSite(int id)
        {
            ViewConfig.GetInstance().MultiplierTaskPerSite = id;
            return RedirectToAction("Index");
        }

        public ActionResult ChangeActualSite(int id)
        {
            ViewConfig.GetInstance().ActualSite = id;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Export(string ExportType, string OneSite)
        {
            ExportResponse respo = Service.Export(Request, Response, ExportType, OneSite);

            if (respo.Type == ExportResponseType.FILE)
                return File(respo.FileArray, respo.ContentType, respo.Name);
            
            return RedirectToAction(respo.Redirect);
        }

    }
}