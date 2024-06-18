using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MovementTechnology.Data;
using MovementTechnology.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MovementTechnology.Controllers;

public class StaffsController : Controller
{
    private ApplicationContext _context;

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    
    public StaffsController(ApplicationContext context, UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        var dep = _context.Departaments.ToListAsync();
        var technic = _context.Technics.ToListAsync();
        var data = await _context.Staffs.ToListAsync();
        return View(data);
    }
    
    public ActionResult StaffsData()
    {
        var dep = _context.Departaments.ToList();
        var technic = _context.Technics.ToList();
        var Staffs = _context.Staffs.ToList();
        if (Staffs.Count == 0)
        {
            return PartialView();
        }

        return PartialView(Staffs);
    }
    
    [Helper.NoDirectAccess]
    [HttpGet]
    public async Task<IActionResult> HistoryView(int id=0)
    {
        var typeTechnic = await _context.TypeTechnics.ToListAsync();
        var departaments = await _context.Departaments.ToListAsync();
        var technics = await _context.Technics.ToListAsync();
        var staffs = await _context.Staffs.ToListAsync();

        var staff = staffs.FirstOrDefault(x => x.Id == id);
        
        return View("HistoryView", staff);
    }
    
    [HttpGet]
    public async Task<IActionResult> RemoveStaffData(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var departamentModel = await _context.Staffs
            .FirstOrDefaultAsync(m => m.Id == id);
        if (departamentModel == null)
        {
            return NotFound();
        }

        return View(departamentModel);
    }

    [HttpPost, ActionName("RemoveStaffData")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var departamentModel = await _context.Staffs.FindAsync(id);
        if (departamentModel == null)
        {
            return NotFound();
        }
        _context.Staffs.Remove(departamentModel);
        await _context.SaveChangesAsync();
        
        return Json(new { url = Helper.RenderRazorViewToString(this, "StaffsData", _context.Staffs.ToList()), isValid = false });
    }
    
    [Helper.NoDirectAccess]
    [HttpGet]
    public async Task<IActionResult> AddOrEditStaff(int id = 0)
    {
        var deps = await _context.Departaments.ToListAsync();
        IEnumerable<SelectListItem> Departaments = new SelectList(deps, "Id", "Name");
        ViewBag.Departaments = Departaments;
        if (id == 0)
            return Json(new { url = Helper.RenderRazorViewToString(this, "AddOrEditStaff", new Staff()), isInsert = true });
        else
        {
            var transactionModel = await _context.Staffs.FindAsync(id);
            if (transactionModel == null)
            {
                return NotFound();
            }
    
            return Json(new { url = Helper.RenderRazorViewToString(this, "AddOrEditStaff", transactionModel), isInsert = false });
        }
    }
    
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEditStaff(int id, [Bind("Id, LastName, FirstName, MiddleName, CabinetNumber, DepartamentID, UserId, Departament, Technics")] Staff staff)
    {
        var deps = await _context.Departaments.ToListAsync();
        IEnumerable<SelectListItem> Departaments = new SelectList(deps, "Id", "Name");
        ViewBag.Departaments = Departaments;
        var user = await _userManager.GetUserAsync(User);
        staff.UserId = user.Id;
        staff.Departament = deps.FirstOrDefault(x => x.Id == staff.DepartamentID);

        ModelState.Remove("Departament.Name");
        ModelState.Remove("Departament.IdUser");
        ModelState.Remove("UserId");
        
        if (ModelState.IsValid)
        {
            if (id == 0)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
            }
            //Update
            else
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentModelExists(staff.Id))
                    { return NotFound(); }
                    else
                    { throw; }
                }
            }
            return Json(new { url = Helper.RenderRazorViewToString(this, "AddOrEditStaff", staff), id=staff.DepartamentID, isValid = true});
        }
        
        return Json(new { url = Helper.RenderRazorViewToString(this, "AddOrEditStaff", staff), id=staff.DepartamentID, isValid = false});
    }
    
    private bool DepartamentModelExists(int id)
    {
        return _context.Staffs.Any(e => e.Id == id);
    }
}