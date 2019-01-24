using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDo.Services.Zadania.SearchBar
{
    public enum DataSearch
    {
        [Display(Name = "Dzisiaj")]
        Today,
        [Display(Name = "+/- tydzień")]
        AroundWeek
    }
}