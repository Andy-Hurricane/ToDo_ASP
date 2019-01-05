using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDo.Areas.ToDo.Models.View;
using ToDo.Areas.ToDo.Models.Tasks;

namespace ToDo.Areas.ToDo.Models.Export
{
    public abstract class Exporter : IExport
    {
        protected string _result;
        protected Config _viewConfig;
        protected IListOfTasks _tasks;

        public Exporter()
        {
            _result = "";
            _viewConfig = Config.GetInstance();
            _tasks = ListOfTasks.GetInstance();
        }


        bool IExport.PrepareData(bool actualSite)
        {
            bool result = true;
            
            Prepare(actualSite);

            return result;
        }

        string IExport.Export()
        {
            return _result;
        }

        int IExport.Length {
            get {
                return _result.Length;
            }
        }

        protected abstract void Prepare(bool actualSite);

        public abstract string ContentType { get; }
        public abstract string Extension { get; }
    }
}