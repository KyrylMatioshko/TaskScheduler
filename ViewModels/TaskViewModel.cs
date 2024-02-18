using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TaskSched.Data.Enums;
using TaskSched.ViewModels.ValidationAttributes;

namespace TaskSched.ViewModels
{
    public class TaskViewModel
    {
        public Guid? TaskId { get; set; }
        public Guid ProjectId { get; set; }

        [Display(Name = "Назва задачі")]
        [Required(ErrorMessage = "Введіть назву задачі")]
        [MaxLength(50, ErrorMessage = "Довжина назви задачі має бути не більше 50 символів")]
        public string Name { get; set; } = null!;

        [Display(Name = "Опис задачі")]
        [Required(ErrorMessage = "Введіть опис задачі")]
        [MaxLength(1000, ErrorMessage = "Довжина опису має бути не більше 1000 символів")]
        public string Description { get; set; } = null!;

        [Display(Name = "Пріоритет")]
        [Required(ErrorMessage = "Оберіть пріоритет")]
        public Priority Priority { get; set; }

        [Display(Name = "Дата створення")]
        [DataType(DataType.Date)]
        public DateTime? DateCreate { get; set; }

        [Date(ErrorMessage = "Дата завершення має бути не менша за сьогоднішню")]
        [Display(Name = "Дата завершення")]
        [Required(ErrorMessage = "Оберіть дату завершення")]
        [DataType(DataType.Date)]
        public DateTime? DateEnd { get; set; }

        [Time]
        [Display(Name = "Час завершення")]
        [Required(ErrorMessage = "Оберіть час завершення")]
        [DataType(DataType.Time)]
        public DateTime? TimeEnd { get; set; }
    }
}
