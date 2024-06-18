using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MovementTechnology.Data;

public class ApplicationContext : IdentityDbContext<User>
{
    public DbSet<Departament> Departaments { get; set; }
    public DbSet<Cartridge> Cartridges { get; set; }
    public DbSet<HistoryCartridge> HistoryCartridges { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Technic> Technics { get; set; }
    public DbSet<HistoryMovement> HistoryMovements { get; set; }
    public DbSet<TypeTechnic> TypeTechnics { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}