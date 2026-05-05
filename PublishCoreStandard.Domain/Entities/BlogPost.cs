// =============================================================
// PublishCore Standard — Domain Layer
// BlogPost.cs — Entidade central do domínio do blog.
// Representa um post com todos os seus metadados.
// =============================================================
using System.ComponentModel.DataAnnotations.Schema;

namespace PublishCoreStandard.Domain.Entities;

/// <summary>Entidade central do domínio. Representa um post publicado no blog.</summary>
public class BlogPost
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>Identificador único (GUID).</summary>
    public string Title { get; set; } = string.Empty;
        /// <summary>Título do post, exibido nos cards e cabeçalho.</summary>
    public string Excerpt { get; set; } = string.Empty;
        /// <summary>Resumo curto exibido na listagem do blog.</summary>
    public string Content { get; set; } = string.Empty;
        /// <summary>Conteúdo completo do post em HTML ou Markdown.</summary>
    public string Author { get; set; } = string.Empty;
        /// <summary>Nome do autor do post.</summary>
    public DateTime PublishedDate { get; set; }
        /// <summary>Data de publicação. Usada para ordenação.</summary>
    public string Slug { get; set; } = string.Empty;
        /// <summary>Slug URL-friendly gerado a partir do título.</summary>
    public List<string> Tags { get; set; } = new();
    public bool IsPublished { get; set; }
        /// <summary>Indica se o post está visível para os leitores.</summary>
    public int ReadingTimeMinutes { get; set; }
        /// <summary>Tempo estimado de leitura em minutos.</summary>
    public int? CategoryId { get; set; }
        /// <summary>Chave estrangeira para a categoria do post.</summary>
    public Category? Category { get; set; }
        /// <summary>Propriedade de navegação para a categoria.</summary>

    [NotMapped]
    public string? TagsString
    {
        get => Tags != null ? string.Join(", ", Tags) : null;
        set => Tags = value?.Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim())
                            .ToList() ?? new List<string>();
    }
}

