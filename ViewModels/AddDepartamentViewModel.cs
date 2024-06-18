using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace MovementTechnology.ViewModels;
public class AddDepartamentViewModel
{
    [Required(ErrorMessage = "Не ввели название отдела")]
    [Display(Name = "Название отдела")]
    public string Name { get; set; }
    [NotMapped]
    public int Id { get; set; }
    [NotMapped]
    public string url { get; set; }
}