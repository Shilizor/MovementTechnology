using Microsoft.AspNetCore.Mvc;

namespace MovementTechnology.Controllers;

public class CartridgesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}