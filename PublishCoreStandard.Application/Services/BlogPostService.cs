// =============================================================
// PublishCore Standard — Application Layer
// BlogPostService.cs — Orquestra os casos de uso do blog.
// =============================================================
using PublishCoreStandard.Application.DTOs;
using PublishCoreStandard.Domain.Entities;
using PublishCoreStandard.Domain.Interfaces;

namespace PublishCoreStandard.Application.Services;

/// <summary>Serviço de aplicação que orquestra as operações de posts.</summary>
public class BlogPostService
{
    private readonly IBlogPostRepository _repository;
    private readonly IReadingTimeCalculator _readingTime;
    private readonly IHtmlSanitizer _sanitizer;

    public BlogPostService(IBlogPostRepository repository, IReadingTimeCalculator readingTime, IHtmlSanitizer sanitizer)
    {
        _repository = repository;
        _readingTime = readingTime;
        _sanitizer = sanitizer;
    }

        // Obtém todos os posts publicados, ordenados por data (mais novos primeiro).
    public async Task<IEnumerable<BlogPostSummaryDto>> GetAllPostSummariesAsync()
    {
        var posts = await _repository.GetAllPublishedAsync();
        return posts.OrderByDescending(p => p.PublishedDate).Select(MapToSummary);
    }

        // Obtém o detalhe de um post pelo slug. Aplica sanitização HTML.
    public async Task<BlogPostDetailDto?> GetPostDetailAsync(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug)) return null;
        var post = await _repository.GetBySlugAsync(slug.ToLowerInvariant());
        if (post == null) return null;

        var clean = _sanitizer.Sanitize(post.Content);
        var time = post.ReadingTimeMinutes > 0 ? post.ReadingTimeMinutes : _readingTime.Calculate(clean);

        return new BlogPostDetailDto(
            post.Slug, post.Title, clean, post.Author, post.PublishedDate,
            post.Tags, time, post.Category?.Name, post.Category?.Slug);
    }

        // Filtra posts por tag (case-insensitive).
    public async Task<IEnumerable<BlogPostSummaryDto>> GetPostsByTagAsync(string tag)
    {
        var posts = await _repository.GetByTagAsync(tag);
        return posts.OrderByDescending(p => p.PublishedDate).Select(MapToSummary);
    }

    public async Task<IEnumerable<BlogPostSummaryDto>> SearchPostsAsync(string query)
    {
        var posts = await _repository.GetAllPublishedAsync();
        return posts.Where(p =>
                p.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                p.Content.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                p.Excerpt.Contains(query, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(p => p.PublishedDate)
            .Select(MapToSummary);
    }

    public async Task<int> GetTotalCountAsync()
    {
        var posts = await _repository.GetAllPublishedAsync();
        return posts.Count();
    }

    private BlogPostSummaryDto MapToSummary(BlogPost post)
    {
        var time = post.ReadingTimeMinutes > 0 ? post.ReadingTimeMinutes : _readingTime.Calculate(post.Content);
        return new BlogPostSummaryDto(
            post.Slug, post.Title, post.Excerpt, post.Author, post.PublishedDate,
            post.Tags, time, post.Category?.Name, post.Category?.Slug);
    }
}

