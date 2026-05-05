// =============================================================
// PublishCore Standard — Web Layer (Admin Area)
// Index.cshtml.cs — Listagem de posts no painel admin.
// =============================================================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PublishCoreStandard.Domain.Entities;
using PublishCoreStandard.Infrastructure.Data;

namespace PublishCoreStandard.Web.Areas.Admin.Pages.Posts;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly BlogDbContext _context;
    public List<BlogPost> Posts { get; set; } = new();

        /// <summary>Inicializa a listagem com BlogDbContext.</summary>
    public IndexModel(BlogDbContext context) => _context = context;

        /// <summary>Carrega todos os posts ordenados por data decrescente.</summary>
    public async Task OnGetAsync()
    {
        Posts = await _context.Posts.OrderByDescending(p => p.PublishedDate).ToListAsync();
    }
}
