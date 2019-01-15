using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ToDo.Services.Zadania
{
    [NotMapped]
    public class ViewConfig
    {
        public int ActualSite { get; set; } = 1;
        public int PagePerSite { get; set; } = 10;
    }
}