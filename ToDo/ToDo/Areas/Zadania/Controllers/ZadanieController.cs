using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.Areas.Zadania.Models;
using ToDo.Areas.Zadania.ViewModel;
using ToDo.Services.Export;
using ToDo.Services.Zadania;
using ToDo.Services.Zadania.SearchBar;

namespace ToDo.Areas.Zadania.Controllers
{
    public class ZadanieController : Controller
    {
        private IZadanieService Service { get; set; }
        private delegate bool CreateAction(Task myTask, HttpPostedFileBase File);

        public ZadanieController(IZadanieService service)
        {
            Service = service;
            
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            
            TasksViewConfigModel ModelView = new TasksViewConfigModel()
            {
                SortList = SessionElement<SortList>(),
                ViewConfig = SessionElement<ViewConfig>()
            };

            ModelView.PrepareForException(filterContext);

            filterContext.Result = View("Index", ModelView);
            filterContext.ExceptionHandled = true;
        }

        // GET: Zadania/Zadanie
        public ActionResult Index()
        {
            SessionElement<ViewConfig>().ClearVisibleModal();


            TasksViewConfigModel ModelView = new TasksViewConfigModel()
            {
                SortList = SessionElement<SortList>(),
                ViewConfig = SessionElement<ViewConfig>()
            };

            ModelView.PrepareForIndex();
            return View(ModelView);
        }

        public ActionResult Exit()
        {
            return View();
        }

        public ActionResult List()
        {
            SessionElement<ViewConfig>().SetView(Services.Zadania.ViewType.LIST);
            TasksViewConfigModel ModelView = new TasksViewConfigModel()
            {
                SortList = SessionElement<SortList>(),
                ViewConfig = SessionElement<ViewConfig>()
            };
            return RedirectToAction("Index");
        }

        public ActionResult Tails()
        {
            SessionElement<ViewConfig>().SetView(Services.Zadania.ViewType.TAIL);
            SessionElement<SortList>().SetTailView();

            TasksViewConfigModel ModelView = new TasksViewConfigModel()
            {
                SortList = SessionElement<SortList>(),
                ViewConfig = SessionElement<ViewConfig>()
            };
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Task task, HttpPostedFileBase File)
        {
            bool isAdd = (task.Id == 0);
             
            SessionElement<ViewConfig>().SetVisibleModal( isAdd 
                ? "Add"
                : "Edit"
            );

            if (ModelState.IsValid)
            {
                CreateAction doIt = isAdd
                    ? new CreateAction(Service.Add)
                    : new CreateAction(Service.Edit);

                if (doIt(task, File))
                {
                    SessionElement<ViewConfig>().ClearVisibleModal();
                    if (isAdd)
                        SessionElement<SortList>().Sort(SortFilter.NULL);
                    else
                        SessionElement<SortList>().Update(Service.GetTasks(), task);
                    return RedirectToAction("Index");
                }
                else return HttpNotFound("Błąd: " + Service.GetError());
            }


            TasksViewConfigModel ModelView = new TasksViewConfigModel()
            {
                SortList = SessionElement<SortList>(),
                ViewConfig = SessionElement<ViewConfig>()
            };

            ModelView.PrepareForIndex();

            return View("Index", ModelView);
        }
        [HttpGet]
        public ActionResult Image(int id)
        {
            Task t = SessionElement<SortList>().GetTask(id);

            return (t == null)
                ? null
                : File(t.ImageContent, t.FileType);
        }
        public ActionResult Delete(int id)
        {
            if (!Service.Remove(id))
                HttpNotFound("Błąd: " + Service.GetError());

            SessionElement<SortList>().Sort(SortFilter.NULL);
            return RedirectToAction("Index");
        }

        public ActionResult Description(int id)
        {
            TasksViewConfigModel ModelView = new TasksViewConfigModel()
            {
                SortList = SessionElement<SortList>(),
                ViewConfig = SessionElement<ViewConfig>()
            };
            ModelView.PrepareForDescription(id);

            return View("Index", ModelView);
        }

        public ActionResult Edit(int id)
        {
            TasksViewConfigModel ModelView = new TasksViewConfigModel()
            {
                SortList = SessionElement<SortList>(),
                ViewConfig = SessionElement<ViewConfig>()
            };
            ModelView.PrepareForEdit(id);

            return View("Index", ModelView);
        }

        public ActionResult SortByTopic()
        {
            SessionElement<SortList>().Sort(SortFilter.TOPIC);
            return RedirectToAction("Index");
        }

        public ActionResult SortByEnd()
        {
            SessionElement<SortList>().Sort(SortFilter.END);
            return RedirectToAction("Index");
        }

        public ActionResult SortByPriority()
        {
            SessionElement<SortList>().Sort(SortFilter.PRIORITY);
            return RedirectToAction("Index");
        }

        public ActionResult SortByStart()
        {
            SessionElement<SortList>().Sort(SortFilter.START);
            return RedirectToAction("Index");
        }

        public ActionResult SortByStatus()
        {
            SessionElement<SortList>().Sort(SortFilter.STATUS);
            return RedirectToAction("Index");
        }

        public ActionResult SwapPrevious(int id)
        {
            SessionElement<SortList>().SwapPrevious(id);
            return RedirectToAction("Index");
        }

        public ActionResult SwapNext(int id)
        {
            SessionElement<SortList>().SwapNext(id);
            return RedirectToAction("Index");
        }

        public ActionResult ChangeTasksPerSite(int id)
        {

            SessionElement<ViewConfig>().MultiplierTaskPerSite = id;
            return RedirectToAction("Index");
        }

        public ActionResult ChangeActualSite(int id)
        {

            SessionElement<ViewConfig>().ActualSite = id;
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Search(Content content)
        {
            SessionElement<SortList>().SearchBar = content;
            SessionElement<SortList>().HandleSearchBar = true;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Export(string ExportType, string OneSite)
        {
            ExportResponse respo;
            if (Request.HttpMethod == "POST")
            {
                bool onlyOneSite;
                try
                {
                    onlyOneSite = OneSite.Equals("on");
                }
                catch (NullReferenceException)
                {
                    onlyOneSite = false;
                }

                int actualPage = SessionElement<ViewConfig>().ActualSite;
                int perSite = SessionElement<ViewConfig>().TaskPerSite;

                IEnumerable<Task> list = onlyOneSite
                    ? SessionElement<SortList>().GetSectionFromList(actualPage, perSite)
                    : SessionElement<SortList>().GetWholeList();
                
                if (list == null)
                {
                    throw new Exception("Nie można eksportować pustej listy.");
                }
                Dictionary<string, IExport> exporters = new Dictionary<string, IExport> {
                    { "TXT", new ExportTxt(Response, list) },
                    { "XLS", new ExportXls(Response, list) },
                    { "PDF", new ExportPdf(Response, list) }
                };

                IExport exporter = exporters[ExportType];

                exporter.PrepareData(onlyOneSite);

                exporter.Export();

                if (ExportType.Equals("PDF"))
                    respo = new ExportResponse($"{exporter.GetName}.{exporter.Extension}", exporter.ContentType, (exporter as ExportPdf)._memory);
            }

            respo = new ExportResponse("Index");

            if (respo.Type == ExportResponseType.FILE)
                return File(respo.FileArray, respo.ContentType, respo.Name);
            
            return RedirectToAction(respo.Redirect);
        }

        private T SessionElement<T>() where T : class, new() {
            Dictionary<Type, string> names = new Dictionary<Type, string>
            {
                { typeof(ViewConfig), "ViewConfig" },
                { typeof(SortList), "SortList" }
            };

            if (!names.ContainsKey(typeof(T)))
                throw new Exception("Sesja nie obsługuje obiektu tego typu.");

            string name = names[typeof(T)];

            T result =((Session[name] as T == null)
                ? Session[name] = new T()
                : Session[name]
            ) as T;

            if (typeof(T) == typeof(SortList))
                (result as SortList).TryUpdate(Service.GetTasks());

            return result;
        }
    }
}