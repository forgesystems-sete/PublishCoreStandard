// =============================================================
// PublishCore Standard — Web Layer (Admin Area)
// Delete.cshtml.cs — Exclusão de posts no painel admin.
// =============================================================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublishCoreStandard.Domain.Entities;
using PublishCoreStandard.Infrastructure.Data;

namespace PublishCoreStandard.Web.Areas.Admin.Pages.Posts;

[Authorize(Roles = "Admin")]
public class DeleteModel : PageModel
{
    private readonly BlogDbContext _context;

        /// <summary>Inicializa a página de exclusão com BlogDbContext.</summary>
    public DeleteModel(BlogDbContext context) => _context = context;

    [BindProperty]
    public BlogPost Post { get; set; } = default!;

        /// <summary>Carrega o post para confirmação.</summary>
    public async Task<IActionResult> OnGetAsync(string id)
    {
        Post = await _context.Posts.FindAsync(id);
        if (Post == null) return NotFound();
        return Page();
    }

        /// <summary>Remove o post do banco e redireciona à listagem.</summary>
    public async Task<IActionResult> OnPostAsync()
    {
        _context.Posts.Remove(Post);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}

