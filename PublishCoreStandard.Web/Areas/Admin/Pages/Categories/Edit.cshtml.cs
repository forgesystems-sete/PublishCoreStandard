// =============================================================
// PublishCore Standard — Web Layer (Admin Area)
// Edit.cshtml.cs — Edição de categorias no painel admin.
// =============================================================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublishCoreStandard.Domain.Entities;
using PublishCoreStandard.Infrastructure.Data;

namespace PublishCoreStandard.Web.Areas.Admin.Pages.Categories;

[Authorize(Roles = "Admin")]
public class EditModel : PageModel
{
    private readonly BlogDbContext _context;

        /// <summary>Inicializa o formulário com BlogDbContext.</summary>
    public EditModel(BlogDbContext context) => _context = context;

    [BindProperty]
    public Category Category { get; set; } = default!;

        /// <summary>Carrega a categoria para edição.</summary>
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Category = await _context.Categories.FindAsync(id);
        if (Category == null) return NotFound();
        return Page();
    }

        /// <summary>Atualiza a categoria no banco.</summary>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        _context.Categories.Update(Category);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
