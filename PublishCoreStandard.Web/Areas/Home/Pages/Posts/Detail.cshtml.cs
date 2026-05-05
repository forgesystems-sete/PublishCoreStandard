using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using PublishCoreStandard.Application.DTOs;
using PublishCoreStandard.Application.Services;
using PublishCoreStandard.Domain.Entities;

namespace PublishCoreStandard.Web.Areas.Home.Pages.Posts;

public class DetailModel : PageModel
{
    private readonly BlogPostService _blogService;
    private readonly SiteSettings _siteSettings;

        /// <summary>Inicializa o PageModel com BlogPostService e SiteSettings.</summary>
    public DetailModel(BlogPostService blogService, IOptions<SiteSettings> siteSettings)
    {
        _blogService = blogService;
        _siteSettings = siteSettings.Value;
    }

    public BlogPostDetailDto Post { get; private set; } = null!;
    public SiteSettings SiteSettings => _siteSettings;

        /// <summary>Busca o post pelo slug. Retorna 404 se não encontrado.</summary>
    public async Task<IActionResult> OnGetAsync(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return NotFound();

        var post = await _blogService.GetPostDetailAsync(slug);
        if (post == null)
            return NotFound();

        Post = post;
        return Page();
    }
}

