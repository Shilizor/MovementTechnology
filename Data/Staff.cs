using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace MovementTechnology.Data;

public class Staff
{
    public Staff()
    {
        Departament = new Departament();
        Technics = new List<Technic>();
    }
    
    public int Id { get; set; }
    [Required(ErrorMessage = "Вы не ввели фамилию сотрудника")]
    [Display(Name = "Фамилия сотрудника")]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Вы не ввели имя сотрудника")]
    [Display(Name = "Имя сотрудника")]
    public string? FirstName { get; set; }
    
    [Display(Name = "Отчество сотрудника")]
    public string? MiddleName { get; set; }
    
    [Required(ErrorMessage = "Вы не ввели номер кабинета сотрудника")]
    [Display(Name = "Кабинет сотрудника")]
    public string? CabinetNumber { get; set; }
    
    [Required(ErrorMessage = "Вы не выбрали отдел сотрудника")]
    [Display(Name = "Отдел сотрудника")]
    public int DepartamentID { get; set; }
    public Departament Departament { get; set; }
    
    public List<Technic> Technics { get; set; }
    
    public string UserId { get; set; }
}