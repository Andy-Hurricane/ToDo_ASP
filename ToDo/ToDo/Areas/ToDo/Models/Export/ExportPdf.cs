using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDo.Areas.ToDo.Models.Tasks;
using System.IO;
using System.Web.UI;
using System.Web.Helpers;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Web.Mvc;

namespace ToDo.Areas.ToDo.Models.Export
{
    public class ExportPdf : Exporter
    {
        private IEnumerable<Task> _list;

        public ExportPdf(HttpResponseBase response) : base(response) { }

        protected override void Prepare(bool actualSite)
        {
            _list = MyList(actualSite);
        }

        public override void Export()
        {
            WebGrid grid = new WebGrid(source: _list, canPage: false, canSort: false);
            string gridHtml = grid.GetHtml(
                   columns: grid.Columns(
                            grid.Column("Action", "Czynność"),
                            grid.Column("Topic", "Temat"),
                            grid.Column("Start", "Data rozpoczęcia"),
                            grid.Column("End", "Data zakończenia"),
                            grid.Column("ActualStatus", "Status"),
                            grid.Column("ActualPriority", "Priorytet"),
                            grid.Column("Progress", "%")
                           )
                    ).ToString();
            string exportData = String.Format("{0}{1}", "", gridHtml);
            var bytes = System.Text.Encoding.UTF8.GetBytes(exportData);
            using (var input = new MemoryStream(bytes)) {
                var output = new MemoryStream();
                Document document = new Document(PageSize.A4, 50, 50, 50, 50);
                var writer = PdfWriter.GetInstance(document, output);
                writer.CloseStream = false;
                document.Open();
                var xmlWorker = iTextSharp.tool.xml.XMLWorkerHelper.GetInstance();
                xmlWorker.ParseXHtml(writer, document, input, System.Text.Encoding.UTF8);
                document.Close();
                output.Position = 0;
                _memory = output;
            }
        }

        public override string ContentType { get; } = "application/pdf";
        public override string Extension { get; } = "pdf";
    }
}