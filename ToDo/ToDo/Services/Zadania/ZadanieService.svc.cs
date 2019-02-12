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
    /// <summary>
    /// Ta usługa służy jedynie do komunikacji z bazą danych.
    /// </summary>
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
            return Context.Tasks;
        }

        public bool Add(Task newTask, HttpPostedFileBase File)
        {
            bool result;

            try
            {
                ValidateTask.GetInstance().ValidateWithID(newTask, Context.Tasks);
                if (File != null && File.ContentLength > 0)
                {
                    newTask.File = File.FileName;
                    newTask.FileType = File.ContentType;
                    using (var reader = new System.IO.BinaryReader(File.InputStream))
                        newTask.ImageContent = reader.ReadBytes(File.ContentLength);
                }
                Context.Tasks.Add(newTask);

                Context.SaveChanges();
                result = true;
            }
            catch (EntityException ex)
            {
                SetError(ex.Message, out result);
            }

            return result;
        }

        public bool Edit(Task newTask, HttpPostedFileBase File)
        {
            bool result;

            try
            {
                if (Context.Tasks == null)
                    throw new Exception("Lista jest pusta.");

                if (newTask == null)
                    throw new Exception(Errors.EditFromEmptyTask);

                Task task = Context.Tasks.FirstOrDefault(el => el.Id == newTask.Id);

                if (task == null)
                    throw new Exception(Errors.EditToEmptyTask);

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

                task.File = newTask.File;
                task.FileType = newTask.FileType;
                task.ImageContent = newTask.ImageContent;

                if (newTask.ClearImage)
                {
                    task.File = string.Empty;
                    task.FileType = string.Empty;
                    task.ImageContent = null;
                }

                Context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                SetError(ex.Message, out result);
            }

            return result;
        }

        public bool Remove(int id)
        {
            bool result;

            try
            {
                if (id < 0)
                    throw new Exception(Errors.IdBelowZero);

                if (Context.Tasks == null)
                    throw new Exception("Lista jest pusta.");

                Task task = Context.Tasks.FirstOrDefault(el => el.Id == id);

                if (task == null)
                    SetError(Errors.RemoveEmptyTask, out result);

                Context.Tasks.Remove(task);

                Context.SaveChanges();
                result = true;
            }
            catch (EntityException ex)
            {
                SetError(ex.Message, out result);
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
