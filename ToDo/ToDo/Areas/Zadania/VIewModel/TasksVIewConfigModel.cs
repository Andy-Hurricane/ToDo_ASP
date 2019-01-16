using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDo.Areas.Zadania.Models;
using ToDo.Services.Zadania;

namespace ToDo.Areas.Zadania.ViewModel
{
    public class TasksVIewConfigModel
    {
        public IEnumerable<Task> Tasks { get; set; }
        public ViewConfig ViewConfig { get; set; }
        public string Exception { get; set; }
    }
}