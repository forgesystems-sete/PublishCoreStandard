// =============================================================
// PublishCore Standard — Domain Layer
// IHtmlSanitizer.cs — Contrato para sanitização de HTML.
// =============================================================

namespace PublishCoreStandard.Domain.Interfaces;

/// <summary>Define a sanitização de HTML para prevenção de XSS.</summary>
public interface IHtmlSanitizer
{
    string Sanitize(string html);
        /// <summary>Remove scripts e tags perigosas do HTML.</summary>
}

