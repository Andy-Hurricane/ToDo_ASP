using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ToDo.Areas.Zadania.Models;

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
        bool Edit(int idOld, Task newTask);

        /// <summary>
        /// Usuń zadanie.
        /// </summary>
        /// <param name="id">Id usuwanego zadania.</param>
        /// <returns>Prawdę dla powodzenia akcji, w przeciwnym wypadku zapisuje błąd, który można odczytać z GetError().</returns>
        [OperationContract]
        bool Remove(int id);

        /// <summary>
        /// Zamień kolejność wybranego zadania z następnym.
        /// </summary>
        /// <param name="swapMe">Id wybranego zadania do zamiany kolejności.</param>
        /// <returns>Prawdę dla powodzenia akcji, w przeciwnym wypadku zapisuje błąd, który można odczytać z GetError().</returns>
        [OperationContract]
        bool SwapNext(int swapMe);

        /// <summary>
        /// Zamień kolejność wybranego zadania z poprzednim.
        /// </summary>
        /// <param name="swapMe">Id wybranego zadania do zamiany kolejności.</param>
        /// <returns>Prawdę dla powodzenia akcji, w przeciwnym wypadku zapisuje błąd, który można odczytać z GetError().</returns>
        [OperationContract]
        bool SwapPrevious(int swapMe);

        /// <summary>
        /// Pobierz komunikat błędu.
        /// </summary>
        /// <returns>Zwraca komunikat błędu, jaki wystąpił.</returns>
        [OperationContract]
        string GetError();
    }
}
