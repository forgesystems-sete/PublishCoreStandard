// =============================================================
// PublishCore Standard — Web Layer (Admin Area)
// Create.cshtml.cs — Criação de novos posts no painel admin.
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
public class CreateModel : PageModel
{
    private readonly BlogDbContext _context;

        /// <summary>Inicializa o formulário com BlogDbContext.</summary>
    public CreateModel(BlogDbContext context) => _context = context;

    [BindProperty]
    public BlogPost Post { get; set; } = default!;

    public SelectList Categories { get; set; } = default!;

        /// <summary>Carrega as categorias para o dropdown do formulário.</summary>
    public async Task<IActionResult> OnGetAsync()
    {
        await LoadCategories();
        return Page();
    }

        /// <summary>Valida slug único e persiste o novo post no banco.</summary>
    public async Task<IActionResult> OnPostAsync()
    {
        if (await _context.Posts.AnyAsync(p => p.Slug == Post.Slug))
        {
            ModelState.AddModelError("Post.Slug", "Slug already exists.");
        }

        if (!ModelState.IsValid)
        {
            await LoadCategories();
            return Page();
        }

        Post.Id = Guid.NewGuid().ToString();
        _context.Posts.Add(Post);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private async Task LoadCategories()
    {
        var categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        Categories = new SelectList(categories, nameof(Category.Id), nameof(Category.Name));
    }
}

