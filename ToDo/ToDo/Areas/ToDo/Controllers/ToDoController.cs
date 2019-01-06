using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ToDo.Areas.ToDo.Models.Export;
using ToDo.Areas.ToDo.Models.Tasks;
using ToDo.Areas.ToDo.Models.View;

namespace ToDo.Areas.ToDo.Controllers
{
    public class ToDoController : Controller {
        IListOfTasks Tasks = ListOfTasks.GetInstance();
        Config ViewConfig = Config.GetInstance();
        static Task editTask;
        
        static int tmpTest = 1;
        // GET: ToDo/ToDo
        public ActionResult Index()
        {
            PrepareViewData();

            return View();
        }


        [HttpPost]
        public ActionResult Export(string ExportType, string OneSite)
        {

            if (Request.HttpMethod == "POST") {
                Dictionary<string, IExport> exporters = new Dictionary<string, IExport> {
                    { "TXT", new ExportTxt(Response) },
                    { "XLS", new ExportXls(Response) },
                    { "PDF", new ExportPdf(Response) }
                };

                IExport exporter = exporters[ExportType];

                bool onlyOneSite;
                try {
                    onlyOneSite = OneSite.Equals("on");
                } catch (NullReferenceException) {
                    onlyOneSite = false;
                }

                exporter.PrepareData( onlyOneSite );

                exporter.Export();

                if (ExportType.Equals("PDF")) 
                    return File((exporter as ExportPdf)._memory, exporter.ContentType, $"{exporter.GetName}.{exporter.Extension}");
            }

            PrepareViewData();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Sort(string sortType)
        {

            if (Request.HttpMethod == "POST") {
                AvailableSort selectedSort = (AvailableSort)Enum.Parse(typeof(AvailableSort), sortType);
                Tasks.SelectedSort = selectedSort;
                Tasks.OrderNow();
            }
            PrepareViewData();

            return RedirectToAction("Index");
        }
        // GET: ToDo/ToDo/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: ToDo/ToDo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection) {
            if (Request.HttpMethod == "POST") {
                string action = Request.Form["Action"];
                string topic = Request.Form["Topic"];
                string start = Request.Form["Start"];
                string end = Request.Form["End"];
                string status = Request.Form["ActualStatus"];
                string priority = Request.Form["ActualPriority"];
                string progress = Request.Form["Progress"];
                string desc = Request.Form["Desc"];
                string file = Request.Form["File"];

                Task tmp = new Task {
                    Topic = topic,
                    Action = action,
                    Start = Convert.ToDateTime(start),
                    End = Convert.ToDateTime(end),
                    ActualStatus = Convert.ToInt32(status),
                    ActualPriority = Convert.ToInt32(priority),
                    Progress = Convert.ToInt32(progress),
                    Description = desc
                };
                Tasks.Add(tmp);

                tmpTest++;
                return RedirectToAction("Index");
                
            } else {

                PrepareViewData();

                return View("Index");
            }
        }
        
        [HttpPost]
        public JsonResult SetEdit(string element)
        {
            editTask = Tasks.Get(Convert.ToInt32(element));
            PrepareViewData();

            return Json("OK open Edit");
        }
        
        // POST: ToDo/ToDo/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection) {
            try
            {
                int ID = editTask.ID;
                string action = Request.Form["Action"];
                string topic = Request.Form["Topic"];
                string start = Request.Form["Start"];
                string end = Request.Form["End"];
                string status = Request.Form["ActualStatus"];
                string priority = Request.Form["ActualPriority"];
                string progress = Request.Form["Progress"];
                string desc = Request.Form["Desc"];
                string file = Request.Form["File"];

                Task tmp = new Task {
                    ID = ID, 
                    Topic = topic,
                    Action = action,
                    Start = Convert.ToDateTime(start),
                    End = Convert.ToDateTime(end),
                    ActualStatus = Convert.ToInt32(status),
                    ActualPriority = Convert.ToInt32(priority),
                    Progress = Convert.ToInt32(progress),
                    Description = desc
                };

                Tasks.Edit(ID, tmp);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // POST: ToDo/ToDo/Delete/5
        [HttpPost]
        public JsonResult Delete(string element) {
            int selectedId = Convert.ToInt32(element);

            Tasks.Remove(selectedId);
            PrepareViewData();
            
            return Json( "OK" );
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
            ViewData["ReverseSort"] = Tasks.ReverseSort;
            ViewData["ActualSort"] = Tasks.SelectedSort;
            ViewData["CountOfTasks"] = Tasks.AllElements();
            ViewData["EditTask"] = editTask;
        }

        [NonAction] 
        public IEnumerable<Task> GetOrderedList() {
            return Tasks.GetTasks(ViewConfig.SkipPages(), ViewConfig.ActualPerSite);
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

        [HttpPost]
        public JsonResult NextInList(string element)
        {
            int actualId = Convert.ToInt32(element);
            Tasks.SwapNext(actualId);

            return Json( "OK" );
        }
        [HttpPost]
        public JsonResult PreviousInList(string element)
        {
            int actualId = Convert.ToInt32(element);
            Tasks.SwapPrevious(actualId);

            return Json("OK");
        }
    }
}
