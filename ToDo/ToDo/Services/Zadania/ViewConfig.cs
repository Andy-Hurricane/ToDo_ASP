using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ToDo.Services.Zadania
{
    [NotMapped]
    public class ViewConfig
    {
        private static ViewConfig _Instance;

        private ViewConfig() { }

        public static ViewConfig GetInstance()
        {
            return (_Instance ?? (_Instance = new ViewConfig()));
        }

        public static void ClearVisibleModal()
        {
            GetInstance().VisibleModal = String.Empty;
        }

        public static void SetVisibleModal(string modalName)
        {
            GetInstance().VisibleModal = modalName;
        }
        public static void SetDescriptionTaskId(int id)
        {
            GetInstance().DescriptionTaskId = id;
        }
        public static void SetEditedTaskId(int id)
        {
            GetInstance().EditedTaskId = id;
        }

        public SortFilter ActualSort {
            get { return SortList.GetInstance().ActualFilter; }
        }

        public bool NormalSort {
            get { return SortList.GetInstance().NormalSort;  }
        }

        private int maxMultiplier = 5;
        private int basePerSite = 10;

        public int BasePerSite { get { return basePerSite; } }
        public int CountAllTasks { get; set; }

        public int DescriptionTaskId { get; private set; }
        public int EditedTaskId { get; private set; }
        /// <summary>
        /// Aktualna strona.
        /// </summary>
        public int ActualSite { get; set; } = 1;

        /// <summary>
        /// Mnożnik ilości zadań na stronę.
        /// </summary>
        public int MultiplierTaskPerSite { get; set; } = 1;

        public int TaskPerSite { get { return MultiplierTaskPerSite * BasePerSite; } }

        public int MaxMultiplierPerSite { get { return maxMultiplier; } }

        public string VisibleModal { get; set; } = String.Empty;
    }
}