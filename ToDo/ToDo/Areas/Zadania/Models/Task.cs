using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Areas.Zadania.Models
{
    public class Task
    {
        /// <summary>
        /// Id zadania.
        /// </summary>
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// Temat zadania.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Temat nie może być pusty.")]
        [MaxLength(255, ErrorMessage = "Długość nie może przekraczać 255 znaków.")]
        [Index(IsUnique = true)]
        [DisplayName("Temat")]
        public string Topic { get; set; }

        /// <summary>
        /// Czynność zadania.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Czynność nie może być pusta.")]
        [MaxLength(255, ErrorMessage = "Długość nie może przekraczać 255 znaków.")]
        [DisplayName("Czynność")]
        public string Action { get; set; }

        /// <summary>
        /// Status zadania.
        /// </summary>
        [Required(ErrorMessage = "Status nie może być pusty.")]
        [DisplayName("Status")]
        public int Status { get; set; }

        /// <summary>
        /// Data rozpoczęcia zadania.
        /// </summary>
        [Required(ErrorMessage = "Data rozpoczęcia nie może być pusta.")]
        [DisplayName("Data rozpoczęcia")]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        /// <summary>
        /// Data zakończenia zadania.
        /// </summary>
        [Required(ErrorMessage = "Data zakończenia nie może być pusta.")]
        [DisplayName("Data zakończenia")]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        /// <summary>
        /// Priorytet zadania.
        /// </summary>
        [Required(ErrorMessage = "Priorytet nie może być pusty.")]
        [DisplayName("Priorytet")]
        public int Priority { get; set; }

        /// <summary>
        /// Progres postępu zadania.
        /// </summary>
        [Required(ErrorMessage = "Progres nie może być pusty.")]
        [DisplayName("% zakończenia")]
        public int Progress { get; set; }


        /// <summary>
        /// Opis zadania.
        /// </summary>
        [MaxLength(255, ErrorMessage = "Długość nie może przekraczać 255 znaków.")]
        [DisplayName("Opis")]
        public string Description { get; set; }

        /// <summary>
        /// Ścieżka do pliku dla zadania.
        /// </summary>
        [DisplayName("Ścieżka do pliku")]
        public string FilePath { get; set; }
    }
}