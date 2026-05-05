// =============================================================
// PublishCore Standard — Web Layer (Admin Area)
// Index.cshtml.cs — Dashboard do painel administrativo.
// =============================================================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PublishCoreStandard.Infrastructure.Data;

namespace PublishCoreStandard.Web.Areas.Admin.Pages;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly BlogDbContext _context;
    public int PostCount { get; set; }
    public int CategoryCount { get; set; }

        /// <summary>Inicializa o dashboard com BlogDbContext para contadores.</summary>
    public IndexModel(BlogDbContext context) => _context = context;

        /// <summary>Carrega os contadores de posts e categorias do banco.</summary>
    public async Task OnGetAsync()
    {
        PostCount = await _context.Posts.CountAsync();
        CategoryCount = await _context.Categories.CountAsync();
    }
}
