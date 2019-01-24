using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ToDo.Areas.Zadania.Models;

namespace ToDo.Services.Export
{
    public class ExportXls : Exporter
    {
        public ExportXls(HttpResponseBase response, IEnumerable<Task> list, string key) : base(response, list, key) { }

        protected override void Prepare(bool actualSite)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"\"Czynność\",\"Temat\",\"Data rozpoczęcia\",\"Data zakończenia\",\"Status\",\"Priorytet\",\"% zakończenia\"");

            foreach (Task t in _tasks)
                sb.AppendLine($"{t.Action},\"{t.Topic}\",{t.Start},{t.End},{t.Status},{t.Priority},{t.Progress}");

            _result = sb.ToString();
        }

        public override string ContentType { get; } = "text/xls";
        public override string Extension { get; } = "xls";
    }
}