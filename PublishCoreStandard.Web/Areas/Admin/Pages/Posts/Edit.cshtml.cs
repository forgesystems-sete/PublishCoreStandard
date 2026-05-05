// =============================================================
// PublishCore Standard — Web Layer (Admin Area)
// Edit.cshtml.cs — Edição de posts existentes no painel admin.
// =============================================================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PublishCoreStandard.Domain.Entities;
using PublishCoreStandard.Infrastructure.Data;

namespace PublishCoreStandard.Web.Areas.Admin.Pages.Posts;

[Authorize(Roles = "Admin")]
public class EditModel : PageModel
{
    private readonly BlogDbContext _context;

        /// <summary>Inicializa o formulário com BlogDbContext.</summary>
    public EditModel(BlogDbContext context) => _context = context;

    [BindProperty]
    public BlogPost Post { get; set; } = default!;

    public SelectList Categories { get; set; } = default!;

        /// <summary>Carrega o post e as categorias para edição.</summary>
    public async Task<IActionResult> OnGetAsync(string id)
    {
        Post = await _context.Posts.FindAsync(id);
        if (Post == null) return NotFound();

        await LoadCategories();
        return Page();
    }

        /// <summary>Atualiza o post no banco após validar slug único.</summary>
    public async Task<IActionResult> OnPostAsync()
    {
        if (await _context.Posts.AnyAsync(p => p.Slug == Post.Slug && p.Id != Post.Id))
        {
            ModelState.AddModelError("Post.Slug", "Slug already exists.");
        }

        if (!ModelState.IsValid)
        {
            await LoadCategories();
            return Page();
        }

        _context.Attach(Post).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private async Task LoadCategories()
    {
        var categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        Categories = new SelectList(categories, nameof(Category.Id), nameof(Category.Name));
    }
}

