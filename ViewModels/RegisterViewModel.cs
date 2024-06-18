using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovementTechnology.ViewModels;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Имя пользователя")]
    [NotMapped]
    public string Username { get; set; }

    [Required]
    [Display(Name = "Введите фамилию")]
    public string LastName { get; set; }
    
    [Required]
    [Display(Name = "Введите имя")]
    public string FirstName { get; set; }
    
    [Required]
    [Display(Name = "Введите отчество")]
    public string MiddleName { get; set; }
    
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
 
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
 
    [Required]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "Подтвердить пароль")]
    public string PasswordConfirm { get; set; }
}