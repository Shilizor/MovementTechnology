namespace MovementTechnology.Data;

public class Cartridge
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Model { get; set; }
    public int Substitutions { get; set; }
    public int TechnicId { get; set; }
    public Technic Technic { get; set; }
    public string UserId { get; set; }
}