using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly SupermarketContext _context;
        public CreateModel(SupermarketContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ListCategories();
            return Page();
        }

        

        [BindProperty]
        public Product product { get; set; } = default!;

        public SelectList Categories { get; set; }

        private void ListCategories()
        {
            var categories = _context.Categories.ToList();
            Categories = new SelectList(categories, "Id", "Name");
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Products == null || product == null)
            { 
            ListCategories();
            return Page(); 
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
