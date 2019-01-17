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


        public static TasksViewConfigModel PrepareForException(IZadanieService Service, ExceptionContext exception)
        {
            return new TasksViewConfigModel
            {
                Tasks = Service.GetTasks(),
                ViewConfig = ViewConfig.GetInstance(),
                Exception = exception.Exception.Message
            };
        }

        public static TasksViewConfigModel PrepareForIndex(IZadanieService Service)
        {
            return new TasksViewConfigModel
            {
                Tasks = Service.GetTasks(),
                ViewConfig = ViewConfig.GetInstance(),
                Exception = String.Empty
            };
        }

        public static TasksViewConfigModel PrepareForDescription(IZadanieService Service, int id)
        {
            IEnumerable<Task> list = Service.GetTasks();
            bool isInList = (list.FirstOrDefault(t => t.Id == id) != null);

            ViewConfig.SetDescriptionTaskId(id);
            ViewConfig.SetVisibleModal( isInList 
                ? "Description"
                : String.Empty
            );

            return new TasksViewConfigModel
            {
                Tasks = Service.GetTasks(),
                ViewConfig = ViewConfig.GetInstance(),
                Exception = isInList ? String.Empty : "Brak takiego zadania."
            };
        }

        public static TasksViewConfigModel PrepareForEdit(IZadanieService Service, int id)
        {
            IEnumerable<Task> list = Service.GetTasks();
            bool isInList = (list.FirstOrDefault(t => t.Id == id) != null);

            ViewConfig.SetEditedTaskId(id);
            ViewConfig.SetVisibleModal(isInList
                ? "Edit"
                : String.Empty
            );

            return new TasksViewConfigModel
            {
                Tasks = Service.GetTasks(),
                ViewConfig = ViewConfig.GetInstance(),
                Exception = isInList ? String.Empty : "Brak takiego zadania."
            };
        }
    }

}