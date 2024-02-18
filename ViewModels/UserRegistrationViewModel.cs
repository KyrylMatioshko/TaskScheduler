using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TaskSched.ViewModels
{
    public class UserRegistrationViewModel
    {
        [Required(ErrorMessage = "Поле не заповнене")]
        [MaxLength(35, ErrorMessage = "Максимум 35 символів")]
        [DataType(DataType.Text)]
        [Display(Name = "Ім'я:")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Поле не заповнене")]
        [MaxLength(35, ErrorMessage = "Максимум 35 символів")]
        [DataType(DataType.Text)]
        [Display(Name = "Прізвище:")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Поле не заповнене")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Некоректний Email")]
        [MaxLength(50, ErrorMessage = "Максимум 50 символів")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email:")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Поле не заповнене")]
        [MinLength(8, ErrorMessage = "Мінімум 8 символів")]
        [MaxLength(20, ErrorMessage = "Максимум 20 символів")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Поле не заповнене")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердіть пароль:")]
        public string PasswordConfirmed { get; set; } = null!;
    }
}
