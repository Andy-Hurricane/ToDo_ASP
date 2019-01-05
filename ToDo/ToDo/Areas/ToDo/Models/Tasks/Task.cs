using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Areas.ToDo.Models.Tasks
{
    /// <summary>
    /// Klasa reprezentująca zadanie.
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Klucz.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Czynność zadania.
        /// </summary>
        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(255)]
        [DataType(DataType.Text)]
        [DisplayName("Czynność")]
        public string Action { get; set; }

        /// <summary>
        /// Tytuł zadania.
        /// </summary>
        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(255)]
        [DataType(DataType.Text)]
        [DisplayName("Temat")]
        public string Topic { get; set; }

        /// <summary>
        /// Początek zadania.
        /// </summary>
        [Required(ErrorMessage = "Pole wymagane")]
        [DataType(DataType.Date)]
        [DisplayName("Data rozpoczęcia")]
        public DateTime Start { get; set; }

        /// <summary>
        /// Koniec zadania.
        /// </summary>
        [Required(ErrorMessage = "Pole wymagane")]
        [DataType(DataType.Date)]
        [DisplayName("Data zakończenia")]
        public DateTime End { get; set; }

        /// <summary>
        /// Aktualny status zadania.
        /// </summary>
        [Required(ErrorMessage = "Pole wymagane")]
        [DisplayName("Status")]
        public int ActualStatus { get; set; }

        /// <summary>
        /// Aktualny priorytet zadania.
        /// </summary>
        [Required(ErrorMessage = "Pole wymagane")]
        [DisplayName("Priorytet")]
        public int ActualPriority { get; set; }

        /// <summary>
        /// Progres zadania.
        /// </summary>
        [Required(ErrorMessage = "Pole wymagane")]
        [Range(0, 100)]
        [DisplayName("% zakończenia")]
        public int Progress { get; set; }

        /// <summary>
        /// Opis zadania.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Ścieżka do pliku.
        /// </summary>
        public string FilePath { get; set; }
    }
}
