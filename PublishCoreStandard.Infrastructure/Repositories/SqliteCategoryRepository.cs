// =============================================================
// PublishCore Standard — Infrastructure Layer
// SqliteCategoryRepository.cs — Implementação concreta do repositório de categorias com SQLite.
// =============================================================

using Microsoft.EntityFrameworkCore;
using PublishCoreStandard.Domain.Entities;
using PublishCoreStandard.Domain.Interfaces;
using PublishCoreStandard.Infrastructure.Data;

namespace PublishCoreStandard.Infrastructure.Repositories;

public class SqliteCategoryRepository : ICategoryRepository
{
    private readonly BlogDbContext _context;
    public SqliteCategoryRepository(BlogDbContext context) => _context = context;

    public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.ToListAsync();
    public async Task<Category?> GetByIdAsync(int id) => await _context.Categories.FindAsync(id);
    public async Task<Category?> GetBySlugAsync(string slug) => await _context.Categories.FirstOrDefaultAsync(c => c.Slug == slug);
    public async Task<Category> CreateAsync(Category category) { _context.Categories.Add(category); await _context.SaveChangesAsync(); return category; }
    public async Task<Category> UpdateAsync(Category category) { _context.Categories.Update(category); await _context.SaveChangesAsync(); return category; }
    public async Task DeleteAsync(int id) { var c = await GetByIdAsync(id); if (c != null) { _context.Categories.Remove(c); await _context.SaveChangesAsync(); } }
}

