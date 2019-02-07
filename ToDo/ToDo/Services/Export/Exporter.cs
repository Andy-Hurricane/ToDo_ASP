using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ToDo.Areas.Zadania.Models;
using ToDo.Services.Zadania;

namespace ToDo.Services.Export
{
    public abstract class Exporter : IExport
    {
        protected HttpResponseBase _response;
        protected string _result;
        protected byte[] _resultByte;
        protected IEnumerable<Task> _tasks;
        public MemoryStream _memory { get; protected set; }

        public Exporter(HttpResponseBase response, IEnumerable<Task> list)
        {
            _response = response;
            _result = "";
            _tasks = list;
        }


        bool IExport.PrepareData(bool actualSite)
        {
            bool result = true;

            Prepare(actualSite);

            return result;
        }

        public virtual void Export()
        {
            _response.Clear();
            _response.ClearHeaders();

            _response.AppendHeader("Content-Length", (this as IExport).Length.ToString());
            _response.ContentType = (this as IExport).ContentType;
            _response.AppendHeader("Content-Disposition", $"attachment;filename=\"{GetName}.{(this as IExport).Extension}\"");

            _response.ContentType = (this as IExport).ContentType;
            _response.Write(_result);

            _response.End();
            _response.Close();
        }


        int IExport.Length {
            get {
                return _result.Length;
            }
        }

        protected abstract void Prepare(bool actualSite);

        public abstract string ContentType { get; }
        public abstract string Extension { get; }
        public string GetName { get { return "TasksList"; } }
    }
}