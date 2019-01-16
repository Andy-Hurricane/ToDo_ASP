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
        private int maxMultiplier = 5;
        private int basePerSite = 10;

        public int BasePerSite { get { return basePerSite; } }

        /// <summary>
        /// Aktualna strona.
        /// </summary>
        public int ActualSite { get; set; } = 1;

        /// <summary>
        /// Mnożnik ilości zadań na stronę.
        /// </summary>
        public int MultiplierTaskPerSite { get; set; } = 1;

        public int TaskPerSite { get { return MultiplierTaskPerSite * BasePerSite; } }

        public int MaxMultiplierPerSite { get { return maxMultiplier; } }
    }
}