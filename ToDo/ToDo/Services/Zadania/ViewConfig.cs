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
        /// <summary>
        /// Aktualna strona.
        /// </summary>
        public int ActualSite { get; set; } = 1;

        /// <summary>
        /// Ilość zadań na stronę.
        /// </summary>
        public int TaskPerSite { get; set; } = 10;
    }
}