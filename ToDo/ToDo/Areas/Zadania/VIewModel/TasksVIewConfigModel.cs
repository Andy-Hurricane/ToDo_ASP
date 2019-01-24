using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.Areas.Zadania.Models;
using ToDo.Services.Zadania;

namespace ToDo.Areas.Zadania.ViewModel
{
    public class TasksViewConfigModel
    {
        public IEnumerable<Task> Tasks { get; set; }
        public ViewConfig ViewConfig { get; set; }
        public string Exception { get; set; }


        public static TasksViewConfigModel PrepareForException(IZadanieService Service, ExceptionContext exception, string key)
        {
            CheckSearchBar(key);
            return new TasksViewConfigModel
            {
                Tasks = Service.GetTasks(),
                ViewConfig = ViewConfig.GetInstance(key),
                Exception = exception.Exception.Message
            };
        }

        public static TasksViewConfigModel PrepareForIndex(IZadanieService Service, string key)
        {
            CheckSearchBar(key);
            return new TasksViewConfigModel
            {
                Tasks = Service.GetTasks(),
                ViewConfig = ViewConfig.GetInstance(key),
                Exception = String.Empty
            };
        }

        public static TasksViewConfigModel PrepareForDescription(IZadanieService Service, int id, string key)
        {
            CheckSearchBar(key);
            IEnumerable<Task> list = Service.GetTasks();
            bool isInList = (list.FirstOrDefault(t => t.Id == id) != null);

            ViewConfig.SetDescriptionTaskId(id, key);
            ViewConfig.SetVisibleModal( isInList 
                ? "Description"
                : String.Empty,
                key
            );

            return new TasksViewConfigModel
            {
                Tasks = Service.GetTasks(),
                ViewConfig = ViewConfig.GetInstance(key),
                Exception = isInList ? String.Empty : "Brak takiego zadania."
            };
        }

        public static TasksViewConfigModel PrepareForEdit(IZadanieService Service, int id, string key)
        {
            CheckSearchBar(key);
            IEnumerable<Task> list = Service.GetTasks();
            bool isInList = (list.FirstOrDefault(t => t.Id == id) != null);

            ViewConfig.SetEditedTaskId(id, key);
            ViewConfig.SetVisibleModal(isInList
                ? "Edit"
                : String.Empty,
                key
            );

            return new TasksViewConfigModel
            {
                Tasks = Service.GetTasks(),
                ViewConfig = ViewConfig.GetInstance(key),
                Exception = isInList ? String.Empty : "Brak takiego zadania."
            };
        }
        
        private static void CheckSearchBar(string key)
        {
            ViewConfig vc = ViewConfig.GetInstance(key);
            if (vc.HandleSearchBar)
                vc.HandleSearchBar = false;
            else
                vc.SearchBar = new Services.Zadania.SearchBar.Content();
        }
    }

}