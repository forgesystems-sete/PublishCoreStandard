using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using PublishCoreStandard.Application.DTOs;
using PublishCoreStandard.Application.Services;

namespace PublishCoreStandard.Web.Areas.Home.Pages;

public class SitemapModel : PageModel
{
    private readonly BlogPostService _blogService;
    public IEnumerable<BlogPostSummaryDto> Posts { get; private set; } = [];
    public string BaseUrl => $"{Request.Scheme}://{Request.Host}";

    public SitemapModel(BlogPostService blogService)
    {
        _blogService = blogService;
    }

        /// <summary>Carrega todos os posts publicados para o sitemap.</summary>
    public async Task OnGetAsync()
    {
        Posts = await _blogService.GetAllPostSummariesAsync();
    }
}

