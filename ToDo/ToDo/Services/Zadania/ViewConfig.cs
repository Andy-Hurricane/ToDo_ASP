using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ToDo.Services.Zadania.SearchBar;

namespace ToDo.Services.Zadania
{
    [NotMapped]
    public class ViewConfig
    {
        private static Dictionary<string, ViewConfig> _Instance = new Dictionary<string, ViewConfig>();

        private ViewConfig() {}

        public static ViewConfig GetInstance(string key)
        {
            if (!_Instance.ContainsKey(key))
                _Instance.Add(key, new ViewConfig());

            return _Instance[key];
        }

        public static void ClearVisibleModal(string key)
        {
            GetInstance(key).VisibleModal = String.Empty;
        }

        public static void SetVisibleModal(string modalName, string key)
        {
            GetInstance(key).VisibleModal = modalName;
        }
        public static void SetDescriptionTaskId(int id, string key)
        {
            GetInstance(key).DescriptionTaskId = id;
        }
        public static void SetEditedTaskId(int id, string key)
        {
            GetInstance(key).EditedTaskId = id;
        }

        public SortFilter ActualSort(string key) {
            return SortList.GetInstance(key).ActualFilter;
        }

        public bool NormalSort(string key) {
            return SortList.GetInstance(key).NormalSort; 
        }

        private int maxMultiplier = 5;
        private int basePerSite = 10;
        private int basePerSiteTile = 9;

        public int BasePerSite {
            get {
                return ActualViewType == ViewType.LIST
                    ? basePerSite
                    : basePerSiteTile;
            }
        }
        public int CountAllTasks { get; set; }

        public int DescriptionTaskId { get; private set; }
        public int EditedTaskId { get; private set; }

        public ViewType ActualViewType { get; private set; } = ViewType.LIST;

        public void SetListView() { ActualViewType = ViewType.LIST; }
        public void SetTailView(string key) {
            SortList.GetInstance(key).NormalSort = true;
            SortList.GetInstance(key).ActualFilter = SortFilter.PRIORITY;
            ActualViewType = ViewType.TAIL;
        }

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

        public Content SearchBar { get; set; }
        public bool HandleSearchBar { get; set; }
    }
}