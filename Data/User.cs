using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MovementTechnology.Data;

public class User : IdentityUser
{
    public int privilege { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
}