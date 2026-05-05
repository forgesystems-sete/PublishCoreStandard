// =============================================================
// PublishCore Standard — Web Layer (Admin Area)
// Delete.cshtml.cs — Exclusão de categorias no painel admin.
// =============================================================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublishCoreStandard.Domain.Entities;
using PublishCoreStandard.Infrastructure.Data;

namespace PublishCoreStandard.Web.Areas.Admin.Pages.Categories;

[Authorize(Roles = "Admin")]
public class DeleteModel : PageModel
{
    private readonly BlogDbContext _context;

        /// <summary>Inicializa a página com BlogDbContext.</summary>
    public DeleteModel(BlogDbContext context) => _context = context;

    [BindProperty]
    public Category Category { get; set; } = default!;

        /// <summary>Carrega a categoria para confirmação.</summary>
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Category = await _context.Categories.FindAsync(id);
        if (Category == null) return NotFound();
        return Page();
    }

        /// <summary>Remove a categoria do banco.</summary>
    public async Task<IActionResult> OnPostAsync()
    {
        _context.Categories.Remove(Category);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
