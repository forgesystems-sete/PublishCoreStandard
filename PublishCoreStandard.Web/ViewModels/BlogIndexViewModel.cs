using PublishCoreStandard.Application.DTOs;
using PublishCoreStandard.Domain.Entities;

namespace PublishCoreStandard.Web.ViewModels;

public class BlogIndexViewModel
{
    public IEnumerable<BlogPostSummaryDto> Posts { get; init; } = [];
    public SiteSettings SiteSettings { get; init; } = new();
    public string? ActiveTag { get; init; }
    public int CurrentPage { get; init; } = 1;
    public int TotalPages { get; init; } = 1;
    public bool HasNoPosts => !Posts.Any();
    public bool ShowPagination => TotalPages > 1;
}
