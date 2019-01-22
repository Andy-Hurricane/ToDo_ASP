﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.Areas.Zadania.Models;
using ToDo.Models;
using ToDo.Services.Export;
using ToDo.Services.Validator;


namespace ToDo.Services.Zadania
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę klasy „ZadanieService2” w kodzie, usłudze i pliku konfiguracji.
    // UWAGA: aby uruchomić klienta testowego WCF w celu przetestowania tej usługi, wybierz plik ZadanieService2.svc lub ZadanieService2.svc.cs w eksploratorze rozwiązań i rozpocznij debugowanie.
    public class ZadanieService : IZadanieService, IDisposable
    {
        private ApplicationDbContext Context { get; set; }

        private string Error { get; set; }

        private SortList SortedList;

        public ZadanieService()
        {
            Context = new ApplicationDbContext();
            SortedList = SortList.GetInstance();

            if (SortedList.actualList == null) 
                SortedList.actualList = Context.Tasks.ToList();
        }

        public void Sort(SortFilter By)
        {
            SortedList.Sort(By, Context.Tasks.ToList());
        }

        private IEnumerable<Task> GetWholeList()
        {
            return SortedList.actualList;
        }

        private IEnumerable<Task> GetSectionFromList()
        {
            ViewConfig vc = ViewConfig.GetInstance();
            vc.CountAllTasks = SortedList.actualList.Count();
            return SortedList.actualList.Skip((vc.ActualSite - 1) * vc.TaskPerSite).Take(vc.TaskPerSite);
        }


        public IEnumerable<Task> GetTasks()
        {
            return GetSectionFromList();
        }

        public bool Add(Task newTask)
        {
            bool result;

            try
            {
                ValidateTask.GetInstance().ValidateWithID(newTask, GetTasks());

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

        public bool Edit(Task newTask)
        {
            bool result;

            try
            {
                Task task = Context.Tasks.FirstOrDefault(el => el.Id == newTask.Id);

                if (task == null)
                    SetError(Errors.EditFromEmptyTask, out result);

                ValidateTask.GetInstance().ValidateWithoutID(newTask, Context.Tasks);

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
                if (swapMe >= SortedList.actualList.Count() - 1)
                    SetError(Errors.SwapWithEmptyTask, out result);

                List<Task> test = SortedList.actualList.ToList();

                Task tmp = test[swapMe];
                test[swapMe] = test[swapMe + 1];
                test[swapMe + 1] = tmp;

                SortedList.actualList = test;
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
                if (swapMe <= 0)
                    SetError(Errors.SwapWithEmptyTask, out result);

                List<Task> test = SortedList.actualList.ToList();

                Task tmp = test[swapMe];
                test[swapMe] = test[swapMe - 1];
                test[swapMe - 1] = tmp;

                SortedList.actualList = test;
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


        public ExportResponse Export(HttpRequestBase Request, HttpResponseBase Response, string ExportType, string OneSite)
        {
            if (Request.HttpMethod == "POST")
            {
                bool onlyOneSite;
                try
                {
                    onlyOneSite = OneSite.Equals("on");
                }
                catch (NullReferenceException)
                {
                    onlyOneSite = false;
                }

                IEnumerable<Task> list = onlyOneSite
                    ? GetSectionFromList()
                    : GetWholeList();

                Dictionary<string, IExport> exporters = new Dictionary<string, IExport> {
                    { "TXT", new ExportTxt(Response, list) },
                    { "XLS", new ExportXls(Response, list) },
                    { "PDF", new ExportPdf(Response, list) }
                };

                IExport exporter = exporters[ExportType];

                exporter.PrepareData(onlyOneSite);

                exporter.Export();
                
                if (ExportType.Equals("PDF"))
                    return new ExportResponse($"{exporter.GetName}.{exporter.Extension}", exporter.ContentType, (exporter as ExportPdf)._memory);
            }

            return new ExportResponse("Index");
        }
    }
}
