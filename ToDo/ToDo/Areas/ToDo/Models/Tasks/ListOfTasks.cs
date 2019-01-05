using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDo.Areas.ToDo.Models.Tasks {
    /// <summary>
    /// Klasa zarządzająca listą zadań.
    /// </summary>
    public class ListOfTasks : IListOfTasks {
        /// <summary>
        /// Instancja obiektu zarządzającego listą zadań.
        /// </summary>
        private static ListOfTasks _instance;
        /// <summary>
        /// Lista zadań.
        /// </summary>
        private List<Task> _tasks { get; set; }

        public bool ReverseSort { get; private set; } = false;

        private Dictionary<AvailableSort, Action> _availableOrder;

        private AvailableSort _selectedSort = AvailableSort.ID;
        AvailableSort IListOfTasks.SelectedSort {
            get {
                return _selectedSort;
            }
            set {
                ReverseSort = (_selectedSort == value && !ReverseSort);
                _selectedSort = value;
            }
        }


        // TODO - jak będzie połączenie z bazą danych, to wtedy nie będzie 1, tylko ostatnie ID z bazy danych.
        private static int ID = 1;

        /// <summary>
        /// Prywatny konstruktor listy zadań.
        /// </summary>
        private ListOfTasks() {
            _tasks = LoadTasks();
            _availableOrder = new Dictionary<AvailableSort, Action> {
                { AvailableSort.ID, OrderedByID },
                { AvailableSort.DateEnd, OrderedByDateEnd },
                { AvailableSort.DateStart, OrderedByDateStart },
                { AvailableSort.Priority, OrderedByPriority },
                { AvailableSort.State, OrderedByState }
            };
        }

        /// <summary>
        /// Funkcja zwracająca instancję obiektu zarządzającego listą zadań.
        /// </summary>
        /// <returns>Instancja obiektu zarządzającego listą zadań.</returns>
        public static ListOfTasks GetInstance()
        {
            return _instance ?? ( _instance = new ListOfTasks() );
        }

        /// <summary>
        /// Funkcja wczytująca listę zadań. Jeśli napotka jakiś problem, to zwraca pustą listę.
        /// </summary>
        /// <returns>Lista zadań.</returns>
        private List<Task> LoadTasks() {
            if (false) {
                // połączenie z bazą danych
            }
            _tasks = new List<Task> {
                new Task {
                    Topic = "test"
                }
            };
            return  _tasks;
        }

        bool IListOfTasks.Add(Task task)
        {
            bool result = true;

            try {
                task.ID = ID;
                ID++;
                _tasks.Add(task);
            } catch {
                result = false;
            }

            return result;
        }

        bool IListOfTasks.Remove(Task task)
        {
            bool result = true;

            try {
                _tasks = (from Task t in _tasks
                          where t != task
                          select t).ToList<Task>();
            } catch {
                result = false;
            }

            return result;
        }

        bool IListOfTasks.Remove(int id)
        {
            bool result = true;

            try {
                _tasks.RemoveAt(id);
            } catch {
                result = false;
            }

            return result;
        }

        bool IListOfTasks.Edit(Task oldTask, Task newTask)
        {
            bool result = true;

            try {
                int id = _tasks.IndexOf(oldTask);
                _tasks.Remove(oldTask);
                _tasks.Insert(id, newTask);
            } catch {
                result = false;
            }

            return result;
        }

        bool IListOfTasks.Edit(int id, Task newTask)
        {
            bool result = true;

            try {
                _tasks.RemoveAt(id);
                _tasks.Insert(id, newTask);
            } catch {
                result = false;
            }

            return result;
        }

        Task IListOfTasks.Get(int id)
        {            
            return (id < 0 || id >= _tasks.Count)
                ? new Task()
                : _tasks[id];
        }

        List<Task> IListOfTasks.GetList()
        {
            return _tasks;
        }

        bool IListOfTasks.Clear()
        {
            _tasks.Clear();

            return true;
        }

        int IListOfTasks.AllElements()
        {
            return _tasks.Count;
        }

        bool IListOfTasks.SwapNext(int actualId)
        {
            Task tmp = _tasks[actualId];
            _tasks[actualId] = _tasks[actualId + 1];
            _tasks[actualId + 1] = tmp;

            // TODO: Zabezpieczenie na warunki graniczne
            return true;
        }
        
        bool IListOfTasks.SwapPrevious(int actualId)
        {

            Task tmp = _tasks[actualId];
            _tasks[actualId] = _tasks[actualId - 1];
            _tasks[actualId - 1] = tmp;

            // TODO: Zabezpieczenie na warunki graniczne
            return true;
        }

        IEnumerable<Task> IListOfTasks.GetTasks()
        {
            return _tasks;
        }

        void IListOfTasks.OrderNow()
        {
            _availableOrder[_selectedSort](); ;
        }

        IEnumerable<Task> IListOfTasks.GetTasks(int skip, int perPage)
        {
            return _tasks.Skip(skip).Take(perPage);
        }

        private void OrderedByID() { _tasks = _tasks.OrderBy(task => task.ID).ToList(); }

        private void OrderedByDateStart()
        {
            _tasks = (ReverseSort
                ? _tasks.OrderByDescending(task => task.Start)
                : _tasks.OrderBy(task => task.Start)).ToList();
        }

        private void OrderedByDateEnd()
        {
            _tasks = (ReverseSort
                ? _tasks.OrderByDescending(task => task.End)
                : _tasks.OrderBy(task => task.End)).ToList();
        }

        private void OrderedByPriority()
        {
            _tasks = (ReverseSort
                ? _tasks.OrderByDescending(task => task.ActualPriority)
                : _tasks.OrderBy(task => task.ActualPriority)).ToList();
        }

        private void OrderedByState()
        {
            _tasks = (ReverseSort
                ? _tasks.OrderByDescending(task => task.ActualStatus)
                : _tasks.OrderBy(task => task.ActualStatus)).ToList();
        }
    }
}
