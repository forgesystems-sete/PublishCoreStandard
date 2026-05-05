// =============================================================
// PublishCore Standard — Domain Layer
// SiteSettings.cs — Configurações do blog carregadas do appsettings.json.
// =============================================================

namespace PublishCoreStandard.Domain.Entities;

/// <summary>Configurações globais do site, vinculadas à seção "Site" do appsettings.json.</summary>
public class SiteSettings
{
    public string SiteName { get; init; } = "PublishCore Standard";
        /// <summary>Nome do blog (aparece no cabeçalho e <title>).</summary>
    public string Tagline { get; init; } = "A privacy-first blog template";
        /// <summary>Descrição curta usada em meta tags.</summary>
    public string AuthorName { get; init; } = "Blog Author";
        /// <summary>Nome do autor/proprietário do blog.</summary>
    public string ThemeColor { get; init; } = "#2563eb";
        /// <summary>Cor primária do tema (hex). Injetada como variável CSS.</summary>
    public string AccentColor { get; init; } = "#1d4ed8";
        /// <summary>Cor de destaque para hover e elementos interativos.</summary>
    public string FooterText { get; init; } = "© {year} PublishCore Standard. Built with care. No trackers.";
        /// <summary>Texto do rodapé. Suporta placeholder {year}.</summary>
    public int PostsPerPage { get; init; } = 6;
        /// <summary>Quantidade de posts por página na listagem.</summary>
}

