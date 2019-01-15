using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Areas.Zadania.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Temat nie może być pusty.")]
        [MaxLength(255, ErrorMessage = "Długość nie może przekraczać 255 znaków.")]
        [Index(IsUnique = true)]
        [DisplayName("Temat")]
        public string Topic { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Czynność nie może być pusta.")]
        [MaxLength(255, ErrorMessage = "Długość nie może przekraczać 255 znaków.")]
        [DisplayName("Czynność")]
        public string Action { get; set; }

        [Required(ErrorMessage = "Status nie może być pusty.")]
        [DisplayName("Status")]
        public int Status { get; set; }

        [Required(ErrorMessage = "Data rozpoczęcia nie może być pusta.")]
        [DisplayName("Data rozpoczęcia")]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "Data zakończenia nie może być pusta.")]
        [DisplayName("Data zakończenia")]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        [Required(ErrorMessage = "Priorytet nie może być pusty.")]
        [DisplayName("Priorytet")]
        public int Priority { get; set; }

        [Required(ErrorMessage = "Progres nie może być pusty.")]
        [DisplayName("Progres")]
        public int Progress { get; set; }


        [MaxLength(255, ErrorMessage = "Długość nie może przekraczać 255 znaków.")]
        [DisplayName("Opis")]
        public string Description { get; set; }

        [DisplayName("Ścieżka do pliku")]
        public string FilePath { get; set; }
    }
}