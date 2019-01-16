using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ToDo.Areas.Zadania.Models;

namespace ToDo.Services.Zadania
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę klasy „ZadanieService2” w kodzie, usłudze i pliku konfiguracji.
    // UWAGA: aby uruchomić klienta testowego WCF w celu przetestowania tej usługi, wybierz plik ZadanieService2.svc lub ZadanieService2.svc.cs w eksploratorze rozwiązań i rozpocznij debugowanie.
    public class ZadanieService : IZadanieService
    {
        private string Error { get; set; }

        public void DoWork()
        {
        }

        public IEnumerable<Task> GetTasks()
        {
            return new List<Task>
            {
                new Task { Topic = "A" },
                new Task { Topic = "B" }
            };
        }

        public bool Add(Task newTask)
        {
            throw new NotImplementedException();
        }

        public bool Edit(Task editedTask)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool SwapNext(Task swapMe)
        {
            throw new NotImplementedException();
        }

        public bool SwapPrevious(Task swapMe)
        {
            throw new NotImplementedException();
        }

        public string GetError()
        {
            return Error;
        }
    }
}
