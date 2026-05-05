// =============================================================
// PublishCore Standard — Web Layer (Admin Area)
// Create.cshtml.cs — Criação de categorias no painel admin.
// =============================================================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PublishCoreStandard.Domain.Entities;
using PublishCoreStandard.Infrastructure.Data;

namespace PublishCoreStandard.Web.Areas.Admin.Pages.Categories;

[Authorize(Roles = "Admin")]
public class CreateModel : PageModel
{
    private readonly BlogDbContext _context;
    [BindProperty] public Category Category { get; set; } = default!;
        /// <summary>Inicializa o formulário com BlogDbContext.</summary>
    public CreateModel(BlogDbContext context) => _context = context;

        /// <summary>Exibe o formulário em branco.</summary>
    public IActionResult OnGet() => Page();

        /// <summary>Valida slug único e persiste a nova categoria.</summary>
    public async Task<IActionResult> OnPostAsync()
    {
        if (await _context.Categories.AnyAsync(c => c.Slug == Category.Slug))
            ModelState.AddModelError("Category.Slug", "Slug already exists.");
        if (!ModelState.IsValid) return Page();
        _context.Categories.Add(Category);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
