using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ToDo.Areas.ToDo.Models.Tasks;

namespace ToDo.Areas.ToDo.Models.Export
{
    public class ExportTxt : Exporter
    {
        public ExportTxt(HttpResponseBase response) : base(response) {}

        protected override void Prepare(bool actualSite)
        {
            IEnumerable<Task> list = MyList(actualSite);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Czynność\tTemat\tData rozpoczęcia\tData zakończenia\tStatus\tPriorytet\t% zakończenia;");

            foreach (Task t in list)
                sb.AppendLine($"{t.Action}\t{t.Topic}\t{t.Start}\t{t.End}\t{t.ActualStatus}\t{t.ActualPriority}\t{t.Progress};");

            _result = sb.ToString();
        }

        public override string ContentType { get; } = "text/plain";
        public override string Extension { get; } = "txt";

    }
}