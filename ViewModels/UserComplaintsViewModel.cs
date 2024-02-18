using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TaskSched.Data.Models;

namespace TaskSched.ViewModels
{
    public class UserComplaintsViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Введіть опис звернення")]
        [MaxLength(300, ErrorMessage = "Довжина звернення має бути не більше 300 символів")]
        [Display(Name = "Опис")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Введіть назву звернення")]
        [MaxLength(50, ErrorMessage = "Довжина назви звернення має бути не більше 50 символів")]
        [Display(Name = "Назва")]
        public string Name { get; set; } = null!;

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата створення")]
        public DateTime CreatedAt { get; set; }
        
    }
}
