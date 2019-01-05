\using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo.Areas.ToDo.Models.Export
{
    public class ExportTXT : IExport
    {
        private string _result;

        string IExport.ContentType { get; } = "text/plain";

        bool IExport.PrepareData(string actualSite)
        {
            bool result = true;

            if (actualSite.Equals("on"))
                ;
            else
                ;

            return result;
        }

        string IExport.Export()
        {
            throw new System.NotImplementedException();
        }

        int IExport.Legnth()
        {
            return _result.Length;
        }
    }
}