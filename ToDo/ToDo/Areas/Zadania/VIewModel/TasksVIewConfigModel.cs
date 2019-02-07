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
        public SortList SortList { get; set; }
        public ViewConfig ViewConfig { get; set; }
        public string Exception { get; set; }

        public void PrepareForException(ExceptionContext exception)
        {
            CheckSearchBar();
            Exception = exception.Exception.Message;
        }

        public void PrepareForIndex()
        {
            CheckSearchBar();

            Exception = String.Empty;
        }

        public void PrepareForDescription(int id)
        {
            CheckSearchBar();
            bool isInList = (SortList.actualList.FirstOrDefault(t => t.Id == id) != null);

            ViewConfig.SetDescriptionTaskId(id);
            ViewConfig.SetVisibleModal( isInList 
                ? "Description"
                : String.Empty
            );
            Exception = isInList ? String.Empty : "Brak takiego zadania.";
        }

        public void PrepareForEdit(int id)
        {
            CheckSearchBar();
            bool isInList = (SortList.actualList.FirstOrDefault(t => t.Id == id) != null);

            ViewConfig.SetEditedTaskId(id);
            ViewConfig.SetVisibleModal(isInList
                ? "Edit"
                : String.Empty
            );

            Exception = isInList ? String.Empty : "Brak takiego zadania.";
        }
        
        private void CheckSearchBar()
        {
            if (SortList.HandleSearchBar)
                SortList.HandleSearchBar = false;
            else
                SortList.SearchBar = new Services.Zadania.SearchBar.Content();
        }
    }

}