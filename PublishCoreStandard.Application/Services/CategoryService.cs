// =============================================================
// PublishCore Standard — Application Layer
// CategoryService.cs — Orquestra os casos de uso de categorias.
// =============================================================

using PublishCoreStandard.Domain.Entities;
using PublishCoreStandard.Domain.Interfaces;

namespace PublishCoreStandard.Application.Services;

/// <summary>Serviço de aplicação para operações com categorias.</summary>
public class CategoryService
{
    private readonly ICategoryRepository _repository;
    public CategoryService(ICategoryRepository repository) => _repository = repository;

    public Task<IEnumerable<Category>> GetAllAsync() => _repository.GetAllAsync();
        // Retorna todas as categorias.
    public Task<Category?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        // Busca categoria por ID.
    public Task<Category?> GetBySlugAsync(string slug) => _repository.GetBySlugAsync(slug);
        // Busca categoria por slug.
    public Task<Category> CreateAsync(Category category) => _repository.CreateAsync(category);
        // Cria uma nova categoria.
    public Task<Category> UpdateAsync(Category category) => _repository.UpdateAsync(category);
        // Atualiza uma categoria existente.
    public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
        // Remove uma categoria pelo ID.
}

