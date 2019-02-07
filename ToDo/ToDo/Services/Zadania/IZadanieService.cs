﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ToDo.Areas.Zadania.Models;
using ToDo.Services.Export;

namespace ToDo.Services.Zadania
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę interfejsu „IZadanieService2” w kodzie i pliku konfiguracji.
    [ServiceContract]
    public interface IZadanieService
    {
        /// <summary>
        /// Pobierz wszystkie zadania.
        /// </summary>
        /// <returns>Zwraca wszystkie zadania.</returns>
        [OperationContract]
        IEnumerable<Task> GetTasks();
        
        /// <summary>
        /// Dodaj zadanie.
        /// </summary>
        /// <param name="newTask">Zadanie, które ma być dodane.</param>
        /// <returns>Prawdę dla powodzenia akcji, w przeciwnym wypadku zapisuje błąd, który można odczytać z GetError().</returns>
        [OperationContract]
        bool Add(Task newTask);

        /// <summary>
        /// Edytuj zadanie.
        /// </summary>
        /// <param name="idOld">Id edytowanego zadania.</param>
        /// <param name="newTask">Nowe wartości zadania.</param>
        /// <returns>Prawdę dla powodzenia akcji, w przeciwnym wypadku zapisuje błąd, który można odczytać z GetError().</returns>
        [OperationContract]
        bool Edit(Task newTask);

        /// <summary>
        /// Usuń zadanie.
        /// </summary>
        /// <param name="id">Id usuwanego zadania.</param>
        /// <returns>Prawdę dla powodzenia akcji, w przeciwnym wypadku zapisuje błąd, który można odczytać z GetError().</returns>
        [OperationContract]
        bool Remove(int id);

        /// <summary>
        /// Pobierz komunikat błędu.
        /// </summary>
        /// <returns>Zwraca komunikat błędu, jaki wystąpił.</returns>
        [OperationContract]
        string GetError();
        
    }
}
