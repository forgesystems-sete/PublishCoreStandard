using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using PublishCoreStandard.Application.DTOs;
using PublishCoreStandard.Application.Services;
using PublishCoreStandard.Domain.Entities;

namespace PublishCoreStandard.Web.Areas.Home.Pages;

public class RssModel : PageModel
{
    private readonly BlogPostService _blogService;
    public SiteSettings SiteSettings { get; }
    public IEnumerable<BlogPostSummaryDto> Posts { get; private set; } = [];
    public string BaseUrl => $"{Request.Scheme}://{Request.Host}";

    public RssModel(BlogPostService blogService, IOptions<SiteSettings> siteSettings)
    {
        _blogService = blogService;
        SiteSettings = siteSettings.Value;
    }

        /// <summary>Carrega todos os posts publicados para o feed.</summary>
    public async Task OnGetAsync()
    {
        Posts = await _blogService.GetAllPostSummariesAsync();
    }
}

