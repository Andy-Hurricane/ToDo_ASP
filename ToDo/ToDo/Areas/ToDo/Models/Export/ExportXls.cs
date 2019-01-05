using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ToDo.Areas.ToDo.Models.Tasks;

namespace ToDo.Areas.ToDo.Models.Export
{
    public class ExportXls : Exporter
    {
        protected override void Prepare(bool actualSite)
        {
            IEnumerable<Task> list = actualSite
                ? _tasks.GetTasks(_viewConfig.SkipPages(), _viewConfig.ActualPerSite)
                : _tasks.GetTasks();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"\"Czynność\",\"Temat\",\"Data rozpoczęcia\",\"Data zakończenia\",\"Status\",\"Priorytet\",\"% zakończenia\"");

            foreach (Task t in list)
                sb.AppendLine($"{t.Action},\"{t.Topic}\",{t.Start},{t.End},{t.ActualStatus},{t.ActualPriority},{t.Progress}");

            _result = sb.ToString();
        }

        public override string ContentType { get; } = "text/xls";
        public override string Extension { get; } = "xls";
    }
}