using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TaskSched.ViewModels
{
    public class ProjectViewModel
    {
        public Guid? ProjectId { get; set; }

        [Required(ErrorMessage = "Введіть назву проєкту")]
        [MaxLength(50, ErrorMessage = "Довжина назви проєкту має бути не більше 50 символів")]
        [Display(Name = "Назва проєкту")]
        public string Name { get; set; } = null!;

        public DateTime DateCreated { get; set; }
    }
}
