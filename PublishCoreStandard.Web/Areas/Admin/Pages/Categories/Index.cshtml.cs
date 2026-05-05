// =============================================================
// PublishCore Standard — Web Layer (Admin Area)
// Index.cshtml.cs — Listagem de categorias no painel admin.
// =============================================================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PublishCoreStandard.Domain.Entities;
using PublishCoreStandard.Infrastructure.Data;

namespace PublishCoreStandard.Web.Areas.Admin.Pages.Categories;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly BlogDbContext _context;
    public List<Category> Categories { get; set; } = new();
        /// <summary>Inicializa a listagem com BlogDbContext.</summary>
    public IndexModel(BlogDbContext context) => _context = context;
        /// <summary>Carrega todas as categorias do banco.</summary>
    public async Task OnGetAsync() => Categories = await _context.Categories.ToListAsync();
}
