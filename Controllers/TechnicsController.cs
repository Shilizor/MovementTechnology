using Microsoft.AspNetCore.Mvc;

namespace MovementTechnology.Controllers;

public class TechnicsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}