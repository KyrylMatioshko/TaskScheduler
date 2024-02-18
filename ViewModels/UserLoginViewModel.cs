using System.ComponentModel.DataAnnotations;

namespace TaskSched.ViewModels
{
    public class UserLoginViewModel
    {
       
            [Required(ErrorMessage = "Поле не заповнене")]
            [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Некоректний Email")]
            [MaxLength(50, ErrorMessage = "Максимум 50 символів")]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email:")]

            public string Email { get; set; } = null!;

            [Required(ErrorMessage = "Поле не заповнене")]
            [MinLength(8, ErrorMessage = "Пароль має складатися мінімум з 8 символів")]
            [MaxLength(20, ErrorMessage = "Максимум 20 символів")]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль:")]
            public string Password { get; set; } = null!;

            [UIHint("Checkbox")]
            [Display(Name = "Запам'ятати акаунт")]
            public bool RememberMe { get; set; }
        
    }
}
