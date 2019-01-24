using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using ToDo.Areas.Zadania.Models;

namespace ToDo.Services.Export
{
    public class ExportPdf : Exporter
    {
        public ExportPdf(HttpResponseBase response, IEnumerable<Task> list, string key) : base(response, list, key) { }


        public override void Export()
        {
            WebGrid grid = new WebGrid(source: _tasks, canPage: false, canSort: false);
            string gridHtml = grid.GetHtml(
                   columns: grid.Columns(
                            grid.Column("Action", "Czynność"),
                            grid.Column("Topic", "Temat"),
                            grid.Column("Start", "Data rozpoczęcia"),
                            grid.Column("End", "Data zakończenia"),
                            grid.Column("Status", "Status"),
                            grid.Column("Priority", "Priorytet"),
                            grid.Column("Progress", "%")
                           )
                    ).ToString();
            string exportData = String.Format("{0}{1}", "", gridHtml);
            var bytes = System.Text.Encoding.UTF8.GetBytes(exportData);
            using (var input = new MemoryStream(bytes))
            {
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

        protected override void Prepare(bool actualSite)
        {
            
        }
    }
}