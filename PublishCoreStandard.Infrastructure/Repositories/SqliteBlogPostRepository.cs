// =============================================================
// PublishCore Standard — Infrastructure Layer
// SqliteBlogPostRepository.cs — Implementação concreta do repositório de posts com SQLite.
// =============================================================

using Microsoft.EntityFrameworkCore;
using PublishCoreStandard.Domain.Entities;
using PublishCoreStandard.Domain.Interfaces;
using PublishCoreStandard.Infrastructure.Data;

namespace PublishCoreStandard.Infrastructure.Repositories;

public class SqliteBlogPostRepository : IBlogPostRepository
{
    private readonly BlogDbContext _context;
    public SqliteBlogPostRepository(BlogDbContext context) => _context = context;

    public async Task<IEnumerable<BlogPost>> GetAllPublishedAsync() =>
        await _context.Posts.Where(p => p.IsPublished).OrderByDescending(p => p.PublishedDate).ToListAsync();

    public async Task<BlogPost?> GetBySlugAsync(string slug) =>
        await _context.Posts.FirstOrDefaultAsync(p => p.IsPublished && p.Slug == slug);

    public async Task<IEnumerable<BlogPost>> GetRecentAsync(int count) =>
        await _context.Posts.Where(p => p.IsPublished).OrderByDescending(p => p.PublishedDate).Take(count).ToListAsync();

    public async Task<IEnumerable<BlogPost>> GetByTagAsync(string tag) =>
        await _context.Posts.Where(p => p.IsPublished && p.Tags.Contains(tag)).OrderByDescending(p => p.PublishedDate).ToListAsync();
}

