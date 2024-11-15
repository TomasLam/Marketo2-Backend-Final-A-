using Microsoft.AspNetCore.Mvc;
using MarketoTest2.Data;
using MarketoTest2.Models;
using Microsoft.EntityFrameworkCore;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
         _context = context;

    }

   public async Task<IActionResult> Index(string category)
{
   
    
    if (string.IsNullOrEmpty(category) || category == "All")
    {
         var products = await _context.Products
            .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
             .ToListAsync();
        
        return View("~/Views/Home/Index.cshtml", products);
    }

    var categoryFilter = await _context.Categories
        .Where(c => c.Name == category)
        .FirstOrDefaultAsync();

     if (categoryFilter == null)
    {
        return NotFound();
    }

    var productsInCategory = await _context.Products
        .Where(p => p.ProductCategories
            .Any(pc => pc.CategoryId == categoryFilter.Id))
        .ToListAsync();

    return View("~/Views/Home/Index.cshtml", productsInCategory);
}


    
    public async Task<IActionResult> Details(int id)
    {
        var product = await _context.Products
            .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        if (product.ProductCategories == null || !product.ProductCategories.Any())
        {
             ViewBag.Message = "Den här produkten har inga associerade kategorier.";
        }

         return View("Details", product);
    }

    public IActionResult Create()
    {
        return View("Create");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {

         if (ModelState.IsValid)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
         return View("Create", product);
    }
}
