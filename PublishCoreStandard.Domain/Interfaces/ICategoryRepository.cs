// =============================================================
// PublishCore Standard — Domain Layer
// ICategoryRepository.cs — Contrato de acesso a dados de categorias.
// =============================================================

using PublishCoreStandard.Domain.Entities;

namespace PublishCoreStandard.Domain.Interfaces;

/// <summary>Define as operações de acesso a dados para categorias.</summary>
public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
        /// <summary>Retorna todas as categorias.</summary>
    Task<Category?> GetByIdAsync(int id);
        /// <summary>Busca categoria por ID.</summary>
    Task<Category?> GetBySlugAsync(string slug);
        /// <summary>Busca categoria por slug.</summary>
    Task<Category> CreateAsync(Category category);
        /// <summary>Cria uma nova categoria.</summary>
    Task<Category> UpdateAsync(Category category);
        /// <summary>Atualiza uma categoria existente.</summary>
    Task DeleteAsync(int id);
        /// <summary>Remove uma categoria pelo ID.</summary>
}

