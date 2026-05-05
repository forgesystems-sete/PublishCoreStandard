// =============================================================
// PublishCore Standard — Application Layer
// BlogPostDto.cs — Data Transfer Objects para posts.
// =============================================================

namespace PublishCoreStandard.Application.DTOs;

public record BlogPostSummaryDto(
    string Slug,
    string Title,
    string Excerpt,
    string Author,
    DateTime PublishedDate,
    IReadOnlyList<string> Tags,
    int ReadingTimeMinutes,
    string? CategoryName,
    string? CategorySlug
);

public record BlogPostDetailDto(
    string Slug,
    string Title,
    string Content,
    string Author,
    DateTime PublishedDate,
    IReadOnlyList<string> Tags,
    int ReadingTimeMinutes,
    string? CategoryName,
    string? CategorySlug
);

