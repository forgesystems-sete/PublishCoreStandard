using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using PublishCoreStandard.Application.DTOs;
using PublishCoreStandard.Application.Services;
using PublishCoreStandard.Domain.Entities;

namespace PublishCoreStandard.Web.Areas.Home.Pages;

public class IndexModel : PageModel
{
    private readonly BlogPostService _blogService;
    public SiteSettings SiteSettings { get; }
    public IEnumerable<BlogPostSummaryDto> Posts { get; private set; } = [];
    public string? ActiveTag { get; private set; }
    public string? ActiveCategory { get; private set; }
    public string? SearchQuery { get; private set; }
    public int CurrentPage { get; private set; } = 1;
    public int TotalPages { get; private set; } = 1;
    private int PageSize => SiteSettings.PostsPerPage;

        /// <summary>Inicializa o PageModel com BlogPostService e SiteSettings.</summary>
    public IndexModel(BlogPostService blogService, IOptions<SiteSettings> options)
    {
        _blogService = blogService;
        SiteSettings = options.Value;
    }

    public async Task OnGetAsync(string? tag = null, string? category = null, string? q = null, int page = 1)
    {
        ActiveTag = tag;
        ActiveCategory = category;
        SearchQuery = q;
        CurrentPage = page < 1 ? 1 : page;

        IEnumerable<BlogPostSummaryDto> allPosts;

        if (!string.IsNullOrWhiteSpace(q))
        {
            allPosts = await _blogService.SearchPostsAsync(q);
        }
        else if (!string.IsNullOrWhiteSpace(tag))
        {
            allPosts = await _blogService.GetPostsByTagAsync(tag);
        }
        else
        {
            allPosts = await _blogService.GetAllPostSummariesAsync();
        }

        // Filtro por categoria (simples, aplicado em memória)
        if (!string.IsNullOrWhiteSpace(category))
        {
            allPosts = allPosts.Where(p => string.Equals(p.CategorySlug, category, StringComparison.OrdinalIgnoreCase));
        }

        var total = allPosts.Count();
        TotalPages = (int)Math.Ceiling(total / (double)PageSize);

        Posts = allPosts
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();
    }

        /// <summary>Gera URL de paginação preservando os filtros ativos.</summary>
    public string GetPageUrl(int page)
    {
        var query = new List<string>();
        if (!string.IsNullOrEmpty(ActiveTag))
            query.Add($"tag={Uri.EscapeDataString(ActiveTag)}");
        if (!string.IsNullOrEmpty(ActiveCategory))
            query.Add($"category={Uri.EscapeDataString(ActiveCategory)}");
        if (!string.IsNullOrEmpty(SearchQuery))
            query.Add($"q={Uri.EscapeDataString(SearchQuery)}");
        query.Add($"page={page}");
        return $"/?{string.Join("&", query)}";
    }
}

