using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;
using ToDo.Areas.Zadania.Models;
using ToDo.Services.Zadania.SearchBar;

namespace ToDo.Services.Zadania
{
    [NotMapped]
    public class ViewConfig
    {
        private string Error { get; set; }

        private int maxMultiplier = 5;
        private int basePerSite = 10;
        private int basePerSiteTile = 9;
        public ViewType ActualViewType { get; private set; } = ViewType.LIST;

        public int DescriptionTaskId { get; private set; }
        public int EditedTaskId { get; private set; }

        public int BasePerSite {
            get {
                return ActualViewType == ViewType.LIST
                    ? basePerSite
                    : basePerSiteTile;
            }
        }

        public void SetView(ViewType vt)
        {
            ActualViewType = vt;
        }

        public ViewConfig() {}

        public void ClearVisibleModal()
        {
            VisibleModal = String.Empty;
        }

        public void SetVisibleModal(string modalName)
        {
            VisibleModal = modalName;
        }
        public void SetDescriptionTaskId(int id)
        {
            DescriptionTaskId = id;
        }
        public void SetEditedTaskId(int id)
        {
            EditedTaskId = id;
        }

       
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

        public string VisibleModal { get; set; } = String.Empty;




        private void SetError(string communicat, out bool result)
        {
            Error = communicat;
            result = false;
        }
    }
}