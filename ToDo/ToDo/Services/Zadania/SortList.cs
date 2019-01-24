using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ToDo.Areas.Zadania.Models;

namespace ToDo.Services.Zadania
{
    public class SortList
    {
        public bool NormalSort { get; set; } = true;
        public SortFilter ActualFilter { get; set; } = SortFilter.PRIORITY;
        public IEnumerable<Task> actualList;

        private static Dictionary<string, SortList> _Instance = new Dictionary<string, SortList>();
        private Dictionary<SortFilter, Action> sorterFunctions;
        private IEnumerable<Task> List;

        private SortList() {
            sorterFunctions = new Dictionary<SortFilter, Action>
            {
                { SortFilter.ID, SortByID },
                { SortFilter.TOPIC, SortByTopic },
                { SortFilter.END, SortByEnd },
                { SortFilter.PRIORITY, SortByPriority },
                { SortFilter.START, SortByStart },
                { SortFilter.STATUS, SortByStatus }
            };
        }

        public static SortList GetInstance(string key)
        {
            if (!_Instance.ContainsKey(key))
                _Instance.Add(key, new SortList());

            return _Instance[key];
        }

        public void Sort(SortFilter By, IEnumerable<Task> list)
        {
            List = list;

            if (By != SortFilter.NULL)
            {
                NormalSort = (ActualFilter != By)
                    ? true
                    : !NormalSort;

                ActualFilter = By;
            }
            if (sorterFunctions.ContainsKey(ActualFilter))
                sorterFunctions[ActualFilter]();
            else throw new Exception("Nie ma jeszcze takiego sortowania");
        }

        private void SortByID()
        {
            actualList = (NormalSort)
                ? List.OrderBy(t => t.Id).ToList()
                : List.OrderByDescending(t => t.Id).ToList();
        }

        private void SortByTopic()
        {
            actualList = (NormalSort)
                ? List.OrderBy(t => t.Topic).ToList()
                : List.OrderByDescending(t => t.Topic).ToList();
        }

        private void SortByEnd()
        {
            actualList = (NormalSort)
                ? List.OrderBy(t => t.End).ToList()
                : List.OrderByDescending(t => t.End).ToList();
        }

        private void SortByPriority()
        {
            actualList = (NormalSort)
                ? List.OrderBy(t => t.Priority).ToList()
                : List.OrderByDescending(t => t.Priority).ToList();
        }

        private void SortByStart()
        {
            actualList = (NormalSort)
                ? List.OrderBy(t => t.Start).ToList()
                : List.OrderByDescending(t => t.Start).ToList();
        }

        private void SortByStatus()
        {
            actualList = (NormalSort)
                ? List.OrderBy(t => t.Status).ToList()
                : List.OrderByDescending(t => t.Status).ToList();
        }

    }
}