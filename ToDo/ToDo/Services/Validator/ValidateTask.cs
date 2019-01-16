using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDo.Areas.Zadania.Models;

namespace ToDo.Services.Validator
{
    public class ValidateTask
    {
        private const string NULL_EXCEPTION = "Niestety, obiekt zadania jest pusty.";
        private const string EXIST_ID_EXCEPTION = "Istnieje już zadanie z podanym ID.";

        private const string NULL_TOPIC_EXCEPTION = "Niestety, temat zadania jest pusty.";
        private const string LENGTH_TOPIC_EXCEPTION = "Niestety, temat zadania zawiera zbyt dużą ilość znaków (max 255).";
        private const string EXIST_TOPIC_EXCEPTION = "Niestety, istnieje już taki temat w kolekcji.";

        private const string NULL_ACTION_EXCEPTION = "Niestety, czynność zadania jest pusta.";
        private const string LENGTH_ACTION_EXCEPTION = "Niestety, czynność zawiera zbyt dużą ilośc znaków (max 255).";

        private const string NULL_DATE_START_EXCEPTION = "Niestety, data rozpoczęcia zadania nie może być pusta.";
        private const string NULL_DATE_END_EXCEPTION = "Niestety, data zakończenia zadania nie może być pusta.";
        private const string DATE_EXCEPTION = "Nie można zakończyć zadania przed jego utworzeniem ;).";

        private const string LOWER_PROGRESS_EXCEPTION = "Progres nie może być ujemny.";
        private const string HIGHER_PROGRESS_EXCEPTION = "Progres nie może być większy jak 100%.";

        private const string LENGTH_DESCRIPTION_EXCEPTION = "Niestety, opis zawiera zbyt dużą ilość znaków (max 255)."

        private static ValidateTask _Instance { get; set; }
        private ValidateTask() { }

        public static ValidateTask GetInstance()
        {
            return (_Instance ?? (_Instance = new ValidateTask()));
        }

        /// <summary>
        /// Przeprowadza kompleksową walidację obiektu Task razem z porównaniem ID.
        /// </summary>
        /// <param name="task">Obiekt task do przetestowania.</param>
        /// <param name="tasks">Lista obiektów, do której ma być dodane zadanie.</param>
        public void ValidateWithID(Task task, IEnumerable<Task> tasks = null)
        {
            ValidateId(task, tasks);
            ValidateWithoutID(task, tasks);
        }
        
        /// <summary>
        /// Przeprowadza kompleksową walidację obiektu Task bez porównania ID.
        /// </summary>
        /// <param name="task">Obiekt task do przetestowania.</param>
        /// <param name="tasks">Lista obiektów, do której ma być dodane zadanie.</param>
        public void ValidateWithoutID(Task task, IEnumerable<Task> tasks = null)
        {
            ValidateAction(task);
            ValidateDates(task);
            ValidateProgress(task);
            ValidateTopic(task);
        }

        /// <summary>
        /// Przeprowadza walidację ID obiektu Task.
        /// </summary>
        /// <param name="task">Obiekt task do przetestowania.</param>
        /// <param name="tasks">Lista obiektów, do której ma być dodane zadanie.</param>
        public void ValidateId(Task task, IEnumerable<Task> tasks = null)
        {
            if (task == null)
                throw new Exception(NULL_EXCEPTION);

            Task tmp = tasks.FirstOrDefault(t => t.Id == task.Id);
                if (tmp != null)
                    throw new Exception(EXIST_ID_EXCEPTION);
        }

        /// <summary>
        /// Przeprowadza walidację tematu obiektu Task.
        /// </summary>
        /// <param name="task">Obiekt task do przetestowania.</param>
        /// <param name="tasks">Lista obiektów, do której ma być dodane zadanie.</param>
        public void ValidateTopic(Task task, IEnumerable<Task> tasks = null)
        {
            if (task == null)
                throw new Exception(NULL_EXCEPTION);

            if (String.IsNullOrEmpty(task.Topic.Trim()))
                throw new Exception(NULL_TOPIC_EXCEPTION);
            if (task.Topic.Length > 255)
                throw new Exception(LENGTH_TOPIC_EXCEPTION);
            if ( tasks != null )
            {
                Task tmp = tasks.FirstOrDefault(t => t.Topic.Trim().Equals(task.Topic.Trim()));

                if (tmp != null)
                    throw new Exception(NULL_TOPIC_EXCEPTION);
            }
        }

        /// <summary>
        /// Przeprowadza walidację czynności obiektu Task.
        /// </summary>
        /// <param name="task">Obiekt task do przetestowania.</param>
        public void ValidateAction(Task task)
        {
            if (task == null)
                throw new Exception(NULL_EXCEPTION);

            if (String.IsNullOrEmpty(task.Action.Trim()))
                throw new Exception(NULL_ACTION_EXCEPTION);
            if (task.Topic.Length > 255)
                throw new Exception(LENGTH_ACTION_EXCEPTION);
        }

        /// <summary>
        /// Przeprowadza walidację dat obiektu Task.
        /// </summary>
        /// <param name="task">Obiekt task do przetestowania.</param>
        public void ValidateDates(Task task)
        {
            if (task == null)
                throw new Exception(NULL_EXCEPTION);

            if (task.Start == null)
                throw new Exception(NULL_DATE_START_EXCEPTION);
            if (task.End == null)
                throw new Exception(NULL_DATE_END_EXCEPTION);

            if (task.End < task.Start)
                throw new Exception(DATE_EXCEPTION);
        }

        /// <summary>
        /// Przeprowadza walidację progresu obiektu Task.
        /// </summary>
        /// <param name="task">Obiekt task do przetestowania.</param>
        public void ValidateProgress(Task task)
        {
            if (task == null)
                throw new Exception(NULL_EXCEPTION);

            if (task.Progress < 0)
                throw new Exception(LOWER_PROGRESS_EXCEPTION);
            if (task.Progress > 100)
                throw new Exception(HIGHER_PROGRESS_EXCEPTION);
        }

        /// <summary>
        /// Przeprowadza walidację opisu obiektu Task.
        /// </summary>
        /// <param name="task">Obiekt task do przetestowania.</param>
        public void ValidateDetails(Task task)
        {
            if (task == null)
                throw new Exception(NULL_EXCEPTION);

            if (task.Description.Length > 255)
                throw new Exception(LENGTH_DESCRIPTION_EXCEPTION);
        }
    }
}