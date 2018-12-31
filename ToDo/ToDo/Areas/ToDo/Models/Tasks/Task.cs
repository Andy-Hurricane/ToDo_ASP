using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.Tasks
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
        /// Tytuł zadania.
        /// </summary>
        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(255)]
        [DataType(DataType.Text)]
        public string Topic { get; set; }

        /// <summary>
        /// Czynność zadania.
        /// </summary>
        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(255)]
        [DataType(DataType.Text)]
        public string Action { get; set; }

        /// <summary>
        /// Początek zadania.
        /// </summary>
        [Required(ErrorMessage = "Pole wymagane")]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        /// <summary>
        /// Koniec zadania.
        /// </summary>
        [Required(ErrorMessage = "Pole wymagane")]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        /// <summary>
        /// Aktualny status zadania.
        /// </summary>
        [Required(ErrorMessage = "Pole wymagane")]
        public Status ActualStatus { get; set; }

        /// <summary>
        /// Aktualny priorytet zadania.
        /// </summary>
        [Required(ErrorMessage = "Pole wymagane")]
        public Priority ActualPriority { get; set; }

        /// <summary>
        /// Progres zadania.
        /// </summary>
        [Required(ErrorMessage = "Pole wymagane")]
        [Range(0, 100)]
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
