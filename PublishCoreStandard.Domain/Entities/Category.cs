// =============================================================
// PublishCore Standard — Domain Layer
// Category.cs — Representa uma categoria de posts.
// =============================================================

namespace PublishCoreStandard.Domain.Entities;

/// <summary>Representa uma categoria à qual os posts podem pertencer.</summary>
public class Category
{
    public int Id { get; set; }
        /// <summary>Identificador único da categoria.</summary>
    public string Name { get; set; } = string.Empty;
        /// <summary>Nome de exibição da categoria.</summary>
    public string Slug { get; set; } = string.Empty;
        /// <summary>Slug URL-friendly da categoria.</summary>
    public string? Description { get; set; }
        /// <summary>Descrição opcional para SEO.</summary>
    public ICollection<BlogPost> Posts { get; set; } = new List<BlogPost>();
        /// <summary>Coleção de posts pertencentes a esta categoria.</summary>
}

