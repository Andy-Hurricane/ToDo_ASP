using System.Collections.Generic;

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
                task.ID = ID;
                ID++;
                _tasks.Remove(task);
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
    }
}
