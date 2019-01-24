using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDo.Services.Zadania.SearchBar
{
    public enum NumbersSearch
    {  
        [Display(Name = "Rosnąco")]
        AZ,
        [Display(Name = "Malejące")]
        ZA
    }
}