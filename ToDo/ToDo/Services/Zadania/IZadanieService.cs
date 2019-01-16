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
        [OperationContract]
        void DoWork();
        [OperationContract]
        IEnumerable<Task> GetTasks();
        [OperationContract]
        bool Add(Task newTask);
        [OperationContract]
        bool Edit(Task editedTask);
        [OperationContract]
        bool Remove(int id);

        [OperationContract]
        bool SwapNext(Task swapMe);
        [OperationContract]
        bool SwapPrevious(Task swapMe);

        [OperationContract]
        string GetError();
    }
}
