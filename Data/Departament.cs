using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovementTechnology.Data;

public class Departament
{
    public Departament()
    {
        Technics = new List<Technic>();
        Staffs = new List<Staff>();
    }
    
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Не ввели название отдела")]
    [Display(Name = "Название отдела")]
    public string Name { get; set; }
    public string IdUser { get; set; }
    public List<Technic> Technics { get; set; }
    
    public List<Staff> Staffs { get; set; }
}