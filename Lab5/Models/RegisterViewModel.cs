using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Ім'я користувача не може бути більше 50 символів.")]
        public string Username { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "ФІО не може бути більше 500 символів.")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Пароль має бути від 8 до 16 символів.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$",
            ErrorMessage = "Пароль має містити хоча б 1 велику літеру, 1 цифру та 1 спеціальний символ.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Phone]
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Телефон має бути у форматі +380XXXXXXXXX.")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Некоректна електронна адреса.")]
        public string Email { get; set; }
    }
}
