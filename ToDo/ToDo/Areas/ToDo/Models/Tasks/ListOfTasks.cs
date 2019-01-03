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

        // TODO - jak będzie połączenie z bazą danych, to wtedy nie będzie 1, tylko ostatnie ID z bazy danych.
        private static int ID = 1;

        /// <summary>
        /// Prywatny konstruktor listy zadań.
        /// </summary>
        private ListOfTasks() {
            _tasks = LoadTasks();
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
                _tasks = (from Task t in _tasks
                         where t.ID != id
                         select t).ToList<Task>();
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
            Task actual = OrderedByID().First(task => task.ID == actualId);
            Task next = OrderedByID().SkipWhile(task => task.ID != actualId).Skip(1).First();

            int actualID = actual.ID;
            int nextID = next.ID;

            next.ID = actualID;
            actual.ID = nextID;
            
            // TODO: zrób sobie sprawdzeniem czy aby przypadkiem nie są daufltem - jeśli chociaż jeden jest defaultem, to wtedy false
            return true;
        }
        
        bool IListOfTasks.SwapPrevious(int actualId)
        {
            Task actual = OrderedByID().First(task => task.ID == actualId);
            Task previous = OrderedByID().TakeWhile((task => task.ID != actualId)).Last(); ;

            int actualID = actual.ID;
            int previousID = previous.ID;

            previous.ID = actualID;
            actual.ID = previousID;
            // TODO: zrób sobie sprawdzeniem czy aby przypadkiem nie są daufltem - jeśli chociaż jeden jest defaultem, to wtedy false
            return true;
        }

        IEnumerable<Task> OrderedByID() { return _tasks.OrderBy(task => task.ID); }
    }
}
