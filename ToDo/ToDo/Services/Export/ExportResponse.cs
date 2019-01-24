using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ToDo.Services.Export
{
    [DataContract]
    public class ExportResponse
    {
        public ExportResponseType Type { get; private set; }
        public MemoryStream FileArray { get; private set; } = null;
        public string ContentType { get; private set; } = String.Empty;
        public string Name { get; private set; } = String.Empty;
        public string Redirect { get; private set; } = String.Empty;

        public ExportResponse(string Redirect)
        {
            Type = ExportResponseType.REDIRECT;
            this.Redirect = Redirect;
        }

        public ExportResponse(string Name, string ContentType, MemoryStream FileArray)
        {
            Type = ExportResponseType.FILE;
            this.Name = Name;
            this.ContentType = ContentType;
            this.FileArray = FileArray;
        }
    }
}