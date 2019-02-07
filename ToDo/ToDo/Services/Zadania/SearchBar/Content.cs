using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using ToDo.Areas.Zadania.Models;

namespace ToDo.Services.Zadania.SearchBar
{
    public class Content
    {
        [Display(Name = "Tytuł")]
        public string Topic { get; set; }

        [Display(Name = "Akcja")]
        public string Action { get; set; }

        public DataSearch? StartSearch { get; set; }
        public DataSearch? EndSearch { get; set; }
        public NumbersSearch? PrioritySearch { get; set; }
        public NumbersSearch? ProgressSearch { get; set; }
        public NumbersSearch? StatusSearch { get; set; }

        public IEnumerable<Task> Filter(IEnumerable<Task> list)
        {
            IEnumerable<Task> result = ByTopic(list);
            result = ByAction(result);
            result = ByStart(result);
            result = ByEnd(result);
            result = ByPriority(result);
            result = ByProgress(result);
            result = ByStatus(result);
            return result;
        }

        private IEnumerable<Task> ByTopic(IEnumerable<Task> list)
        {
            return String.IsNullOrEmpty(Topic)
                ? list
                : list.Where(t => Regex.IsMatch(t.Topic, Topic, RegexOptions.IgnoreCase));
        }
        private IEnumerable<Task> ByAction(IEnumerable<Task> list)
        {
            return String.IsNullOrEmpty(Action)
                ? list
                : list.Where(t => Regex.IsMatch(t.Action, Action, RegexOptions.IgnoreCase));
        }
        private IEnumerable<Task> ByStart(IEnumerable<Task> list)
        {
            IEnumerable<Task> result = ByTopic(list);

            if (StartSearch != null)
            {
                switch (StartSearch)
                {
                    case DataSearch.Today:
                        result = list.Where(t => t.Start.Date.CompareTo(DateTime.Now.Date) == 0);
                        break;
                    case DataSearch.AroundWeek:
                        result = list.Where(t => t.Start.Date > DateTime.Now.Date.AddDays(-7) && t.Start.Date < DateTime.Now.Date.AddDays(7));
                        break;
                }
            }

            return result;
        }

        private IEnumerable<Task> ByEnd(IEnumerable<Task> list)
        {
            IEnumerable<Task> result = ByTopic(list);

            if (EndSearch != null)
            {
                switch (EndSearch)
                {
                    case DataSearch.Today:
                        result = list.Where(t => t.End.Date.CompareTo(DateTime.Now.Date) == 0);
                        break;
                    case DataSearch.AroundWeek:
                        result = list.Where(t => t.End.Date > DateTime.Now.Date.AddDays(-7) && t.End.Date < DateTime.Now.Date.AddDays(7));
                        break;
                }
            }

            return result;
        }
        private IEnumerable<Task> ByPriority(IEnumerable<Task> list)
        {
            IEnumerable<Task> result = ByTopic(list);
            if (PrioritySearch != null)
            {
                switch (PrioritySearch)
                {
                    case NumbersSearch.AZ:
                        result = list.OrderBy(t => t.Priority).ToList<Task>();
                        break;
                    case NumbersSearch.ZA:
                        result = list.OrderByDescending(t => t.Priority).ToList<Task>();
                        break;
                }
            }
            return result;
        }
        private IEnumerable<Task> ByProgress(IEnumerable<Task> list)
        {
            IEnumerable<Task> result = ByTopic(list);
            if (ProgressSearch != null)
            {
                switch (ProgressSearch)
                {
                    case NumbersSearch.AZ:
                        result = list.OrderBy(t => t.Progress).ToList<Task>();
                        break;
                    case NumbersSearch.ZA:
                        result = list.OrderByDescending(t => t.Progress).ToList<Task>();
                        break;
                }
            }
            return result;
        }
        private IEnumerable<Task> ByStatus(IEnumerable<Task> list)
        {
            IEnumerable<Task> result = ByTopic(list);
            if (PrioritySearch != null)
            {
                switch (StatusSearch)
                {
                    case NumbersSearch.AZ:
                        result = list.OrderBy(t => t.Status).ToList<Task>();
                        break;
                    case NumbersSearch.ZA:
                        result = list.OrderByDescending(t => t.Status).ToList<Task>();
                        break;
                }
            }
            return result;
        }
    }
}