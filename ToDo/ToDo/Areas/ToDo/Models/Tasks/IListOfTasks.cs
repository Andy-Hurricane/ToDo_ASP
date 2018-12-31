﻿using System.Collections.Generic;

namespace ToDo.Areas.ToDo.Models.Tasks
{
    /// <summary>
    /// Obsługa listy zadań.
    /// </summary>
    public interface IListOfTasks
    {
        /// <summary>
        /// Dodanie zadania do listy.
        /// </summary>
        /// <param name="task">Dodawane zadanie.</param>
        /// <returns>Wynik logiczny operacji.</returns>
        bool Add(Task task);

        /// <summary>
        /// Usunięcie zadania z listy.
        /// </summary>
        /// <param name="task">Usuwane zadanie.</param>
        /// <returns>Wynik logiczny operacji.</returns>
        bool Remove(Task task);
        /// <summary>
        /// Usunięcie zadania z listy.
        /// </summary>
        /// <param name="id">Id zadania.</param>
        /// <returns>Wynik logiczny operacji</returns>
        bool Remove(int id);

        /// <summary>
        /// Edytowanie zadania.
        /// </summary>
        /// <param name="oldTask">Zadanie, które będzie edytowane.</param>
        /// <param name="newTask">Zadanie zedytowane.</param>
        /// <returns>Wynik logiczny operacji.</returns>
        bool Edit(Task oldTask, Task newTask);
        /// <summary>
        /// Edytowanie zadania.
        /// </summary>
        /// <param name="id">Id zadania, które będzie edytowane.</param>
        /// <param name="newTask">Zadanie zedytowane.</param>
        /// <returns>Wynik logiczny operacji.</returns>
        bool Edit(int id, Task newTask);

        /// <summary>
        /// Pobieranie zadania.
        /// </summary>
        /// <param name="id">Id zadania, które chce się pobrać.</param>
        /// <returns>Pobrane zadanie.</returns>
        Task Get(int id);

        /// <summary>
        /// Zwraca listę ze wszystkimi zadaniami.
        /// </summary>
        /// <returns>Lista z zadaniami.</returns>
        List<Task> GetList();
    }
}