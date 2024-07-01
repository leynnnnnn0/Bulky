using Microsoft.AspNetCore.Mvc.RazorPages;
using BulkyBookWebRazor_Temp.Data;
using BulkyBookWebRazor_Temp.Model;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWebRazor_Temp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
