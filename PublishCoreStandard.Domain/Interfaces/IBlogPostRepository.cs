// =============================================================
// PublishCore Standard — Domain Layer
// IBlogPostRepository.cs — Contrato de acesso a dados de posts.
// =============================================================

using PublishCoreStandard.Domain.Entities;

namespace PublishCoreStandard.Domain.Interfaces;

/// <summary>Define as operações de acesso a dados para posts do blog.</summary>
public interface IBlogPostRepository
{
    Task<IEnumerable<BlogPost>> GetAllPublishedAsync();
        /// <summary>Retorna todos os posts publicados.</summary>
    Task<BlogPost?> GetBySlugAsync(string slug);
        /// <summary>Busca um post pelo slug (case-insensitive).</summary>
    Task<IEnumerable<BlogPost>> GetRecentAsync(int count);
        /// <summary>Retorna os N posts mais recentes.</summary>
    Task<IEnumerable<BlogPost>> GetByTagAsync(string tag);
        /// <summary>Filtra posts por tag.</summary>
}

