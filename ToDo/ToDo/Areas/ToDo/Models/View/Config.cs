using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDo.Areas.ToDo.Models.Tasks;

namespace ToDo.Areas.ToDo.Models.View
{
    /// <summary>
    /// Klasa odpowiedzialna za konfigurację widoku.
    /// </summary>
    public class Config
    {
        private static Config _instance;

        private Config() {
            this.ActualSite = 1;
            this.BaseElementPerSite = 10;
            this.ActualPerSite = this.BaseElementPerSite;
        }

        public static Config GetInstance()
        {
            return _instance ?? (_instance = new Config());
        }
        /// <summary>
        /// Domyślna ilość wyświetlanych zadań na stronie.
        /// </summary>
        public int BaseElementPerSite { get; private set; }

        /// <summary>
        /// Aktualna strona z zadaniami.
        /// </summary>
        public int ActualSite { get; set; }

        /// <summary>
        /// Aktualna ilość zadań na stronę.
        /// </summary>
        public int ActualPerSite { get; set; }

        public int SkipPages()
        {
            return (ActualSite - 1) * ActualPerSite;
        }
        
    }
}