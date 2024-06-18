namespace MovementTechnology.Data;

public class Technic
{
    public int Id { get; set; }
    public int TypeTechnicId { get; set; }
    public TypeTechnic? TypeTechnic { get; set; }
    public string? Name { get; set; }
    public string? Model { get; set; }
    public int DepartamentId { get; set; }
    public Departament? Departament { get; set; }
    public int StaffId { get; set; }
    public Staff? Staff { get; set; }
    public string IdUser { get; set; }
    public string? NetworkAddress { get; set; }
    public string? InventoryNumber { get; set; }
    public string? Commisioning { get; set; }
    public string? Status { get; set; }
}