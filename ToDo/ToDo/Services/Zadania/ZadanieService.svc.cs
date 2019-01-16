using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ToDo.Areas.Zadania.Models;
using ToDo.Models;

namespace ToDo.Services.Zadania
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę klasy „ZadanieService2” w kodzie, usłudze i pliku konfiguracji.
    // UWAGA: aby uruchomić klienta testowego WCF w celu przetestowania tej usługi, wybierz plik ZadanieService2.svc lub ZadanieService2.svc.cs w eksploratorze rozwiązań i rozpocznij debugowanie.
    public class ZadanieService : IZadanieService, IDisposable
    {
        private ApplicationDbContext Context { get; set; }

        private string Error { get; set; }

        public ZadanieService()
        {
            Context = new ApplicationDbContext();
        }

        public IEnumerable<Task> GetTasks()
        {
            return Context.Tasks.ToList();
        }

        public bool Add(Task newTask)
        {
            bool result;

            if (newTask == null)
                SetError(Errors.AddEmptyTask, out result);

            try
            {
                Context.Tasks.Add(newTask);

                Context.SaveChanges();
            }
            catch (EntityException ex)
            {
                SetError(ex.Message, out result);
            }
            finally
            {
                result = true;
            }



            return result;
        }

        public bool Edit(int idOld, Task newTask)
        {
            bool result;

            try
            {
                Task task = Context.Tasks.FirstOrDefault(el => el.Id == idOld);

                if (task == null)
                    SetError(Errors.EditFromEmptyTask, out result);

                if (newTask == null)
                    SetError(Errors.EditToEmptyTask, out result);

                task.Action = newTask.Action;
                task.Description = newTask.Description;
                task.End = newTask.End;
                task.File = newTask.File;
                task.Priority = newTask.Priority;
                task.Progress = newTask.Progress;
                task.Start = newTask.Start;
                task.Status = newTask.Status;
                task.Topic = newTask.Topic;

                Context.SaveChanges();
            }
            catch (EntityException ex)
            {
                SetError(ex.Message, out result);
            }
            finally
            {
                result = true;
            }

            return result;
        }

        public bool Remove(int id)
        {
            bool result;

            if (id < 0)
                SetError(Errors.IdBelowZero, out result);

            try
            {
                Task task = Context.Tasks.FirstOrDefault(el => el.Id == id);

                if (task == null)
                    SetError(Errors.RemoveEmptyTask, out result);

                Context.Tasks.Remove(task);

                Context.SaveChanges();
            }

            catch (EntityException ex)
            {
                SetError(ex.Message, out result);
            }
            finally
            {
                result = true;
            }

            return result;
        }

        public bool SwapNext(int swapMe)
        {
            bool result;

            try
            {
                Task selectedTask = Context.Tasks.FirstOrDefault(el => el.Id == swapMe);

                if (selectedTask == null)
                    SetError(Errors.SwapEmptyTask, out result);

                Task nextTask = Context.Tasks.SkipWhile(el => el == selectedTask).Skip(1).FirstOrDefault();

                if (nextTask == null)
                    SetError(Errors.SwapWithEmptyTask, out result);

                Task tmp = selectedTask;
                selectedTask = nextTask;
                nextTask = selectedTask;
            }
            catch (EntityException ex)
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
                Task selectedTask = Context.Tasks.FirstOrDefault(el => el.Id == swapMe);

                if (selectedTask == null)
                    SetError(Errors.SwapEmptyTask, out result);

                Task previousTask = Context.Tasks.TakeWhile(el => el == selectedTask).LastOrDefault();

                if (previousTask == null)
                    SetError(Errors.SwapWithEmptyTask, out result);

                Task tmp = selectedTask;
                selectedTask = previousTask;
                previousTask = selectedTask;
            }
            catch (EntityException ex)
            {
                SetError(ex.Message, out result);
            }
            finally
            {
                result = true;
            }

            return result;
        }

        public string GetError()
        {
            string actualError = Error;
            Error = String.Empty;
            return actualError;
        }

        public void Dispose()
        {
            Context.Dispose();
        }


        private void SetError(string communicat, out bool result)
        {
            Error = communicat;
            result = false;
        }
    }
}
