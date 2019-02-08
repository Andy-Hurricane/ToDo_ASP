using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ToDo.Areas.Zadania.Models;
using ToDo.Services.Zadania.SearchBar;

namespace ToDo.Services.Zadania
{
    public class SortList
    {
        public bool NormalSort { get; set; } = true;
        public SortFilter ActualFilter { get; set; } = SortFilter.PRIORITY;
        public IEnumerable<Task> actualList { get; set; }
        public int CountAllTasks { get; set; }
        public Content SearchBar { get; set; }
        public bool HandleSearchBar { get; set; }
        private string Error { get; set; }

        private Dictionary<SortFilter, Action> sorterFunctions;
        private IEnumerable<Task> List;


        public SortList()
        {
            SearchBar = new Content();
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

        public void Sort(SortFilter By)
        {

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

        public Task GetTask(int id)
        {
            return List.FirstOrDefault(t => t.Id == id);
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

        public void SetTailView()
        {
            NormalSort = true;
            ActualFilter = SortFilter.PRIORITY;
        }

        public IEnumerable<Task> GetWholeList()
        {
            return actualList;
        }

        public IEnumerable<Task> GetSectionFromList(int actualSite, int taskPerSite)
        {
            IEnumerable<Task> list = SearchBar.Filter(actualList);
            CountAllTasks = actualList.Count();
            return list.Skip((actualSite - 1) * taskPerSite).Take(taskPerSite);
        }


        public bool SwapNext(int swapMe)
        {
            bool result;

            try
            {
                if (swapMe >= actualList.Count() - 1)
                    throw new Exception(Errors.SwapWithEmptyTask);

                List<Task> test = actualList.ToList();

                Task tmp = test[swapMe];
                test[swapMe] = test[swapMe + 1];
                test[swapMe + 1] = tmp;

                actualList = test;
            }
            catch (Exception ex)
            {
                SetError(ex.Message, out result);
            }
            finally
            {
                result = true;
            }

            return result;
        }

        public bool SwapPrevious(int swapMe)
        {
            bool result;

            try
            {
                if (swapMe <= 0)
                    throw new Exception(Errors.SwapWithEmptyTask);

                List<Task> test = actualList.ToList();

                Task tmp = test[swapMe];
                test[swapMe] = test[swapMe - 1];
                test[swapMe - 1] = tmp;

                actualList = test;
            }
            catch (Exception ex)
            {
                SetError(ex.Message, out result);
            }
            finally
            {
                result = true;
            }

            return result;
        }

        private void SetError(string communicat, out bool result)
        {
            Error = communicat;
            result = false;
        }

        public void TryUpdate(IEnumerable<Task> task)
        {
            if (task != null)
            {
                if (List != null)
                {
                    IEnumerable<Task> restFromMyList = List.Except(task);
                    IEnumerable<Task> restFromUpdated = task.Except(List);
                    if (restFromMyList.Count() == restFromUpdated.Count() && restFromMyList.Count() == 0)
                        List = task;
                }


                if (List == null)
                {
                    List = task;
                }
                if (actualList == null)
                    Sort(SortFilter.NULL);
            }
        }

        public void Update(IEnumerable<Task> list, Task myTask)
        {
            List = list;
            int myIdx = 0;

            var task = actualList.ToArray();
            for (int iter = 0; iter < task.Count(); iter++)
                if (task[iter].Id == myTask.Id)
                    myIdx = iter;

            task[myIdx] = myTask;
            actualList = task;
        }
    }
}