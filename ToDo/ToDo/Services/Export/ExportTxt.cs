using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ToDo.Areas.Zadania.Models;

namespace ToDo.Services.Export
{
    public class ExportTxt : Exporter
    {
        public ExportTxt(HttpResponseBase response, IEnumerable<Task> list) : base(response, list) { }

        protected override void Prepare(bool actualSite)
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Czynność\tTemat\tData rozpoczęcia\tData zakończenia\tStatus\tPriorytet\t% zakończenia;");

            foreach (Task t in _tasks)
                sb.AppendLine($"{t.Action}\t{t.Topic}\t{t.Start}\t{t.End}\t{t.Status}\t{t.Priority}\t{t.Progress};");

            _result = sb.ToString();
        }

        public override string ContentType { get; } = "text/plain";
        public override string Extension { get; } = "txt";

    }
}