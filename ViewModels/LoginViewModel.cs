using System.ComponentModel.DataAnnotations;

namespace MovementTechnology.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Не верный имя пользователя")]
    [Display(Name = "Username")]
    public string Username { get; set; }
         
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
         
    [Display(Name = "Запомнить?")]
    public bool RememberMe { get; set; }
         
    public string ReturnUrl { get; set; }
}