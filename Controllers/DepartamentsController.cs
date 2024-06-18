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

public class DepartamentsController : Controller
{
    private ApplicationContext _context;

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    public List<Staff> staffs { get; set; }
    public List<Departament> Departaments { get; set; }
    public DepartamentsController(ApplicationContext context, UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        
        staffs = _context.Staffs.AsNoTracking().ToList();
        Departaments = _context.Departaments.AsNoTracking().ToList();
    }
    
    [Helper.NoDirectAccess]
    [HttpGet]
    public async Task<IActionResult> HistoryView(int id=0)
    {
        var technics = await _context.Technics.ToListAsync();
        var typeTechnic = await _context.TypeTechnics.ToListAsync();
        var staffs = await _context.Staffs.ToListAsync();
        var departaments = await _context.Departaments.ToListAsync();

        var departament = departaments.FirstOrDefault(x => x.Id == id);
        
        return View("HistoryView", departament);
    }


    // GET
    public async Task<IActionResult> Index()
    {
        var data = await _context.Departaments.ToListAsync();
        return View(data);
    }
    
    public ActionResult DepartamentsData()
    {
        if (Departaments.Count == 0)
        {
            return PartialView();
        }

        return PartialView(Departaments);
    }

    [HttpGet]
    public async Task<IActionResult> RemoveDepData(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var departamentModel = await _context.Departaments
            .FirstOrDefaultAsync(m => m.Id == id);
        if (departamentModel == null)
        {
            return NotFound();
        }

        return View(departamentModel);
    }

    [HttpPost, ActionName("RemoveDepData")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var departamentModel = await _context.Departaments.FindAsync(id);
        if (departamentModel == null)
        {
            return NotFound();
        }
        _context.Departaments.Remove(departamentModel);
        await _context.SaveChangesAsync();
        
        return Json(new { url = Helper.RenderRazorViewToString(this, "DepartamentsData", _context.Departaments.ToList()), isValid = false });
    }
    
    [Helper.NoDirectAccess]
    [HttpGet]
    public async Task<IActionResult> AddOrEditDep(int id = 0)
    {
        if (id == 0)
            return Json(new { url = Helper.RenderRazorViewToString(this, "AddOrEditDep", new Departament()), isInsert = true });
        else
        {
            var transactionModel = await _context.Departaments.FindAsync(id);
            if (transactionModel == null)
            {
                return NotFound();
            }
    
            return Json(new { url = Helper.RenderRazorViewToString(this, "AddOrEditDep", transactionModel), isInsert = false });
        }
    }

    
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEditDep(int id, [Bind("Id, Name, IdUser, Technics, Staffs")] Departament dep)
    {
        var user = await _userManager.GetUserAsync(User);
        dep.IdUser = user.Id;
        
        ModelState["IdUser"].ValidationState = ModelValidationState.Valid;
        if (ModelState.IsValid)
        {
            //Insert
            if (id == 0)
            {
                _context.Add(dep);
                await _context.SaveChangesAsync();
            }
            //Update
            else
            {
                try
                {
                    _context.Update(dep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentModelExists(dep.Id))
                    { return NotFound(); }
                    else
                    { throw; }
                }
            }
            return Json(new { url = Helper.RenderRazorViewToString(this, "AddOrEditDep", dep), isValid = true });
        }
        return Json(new { url = Helper.RenderRazorViewToString(this, "AddOrEditDep", dep), isValid = false });
    }
    
    private bool DepartamentModelExists(int id)
    {
        return _context.Departaments.Any(e => e.Id == id);
    }
}