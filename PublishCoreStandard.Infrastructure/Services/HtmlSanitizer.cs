// =============================================================
// PublishCore Standard — Infrastructure Layer
// HtmlSanitizer.cs — Implementação de sanitização HTML.
// =============================================================
using Ganss.Xss;
using DomainHtmlSanitizer = PublishCoreStandard.Domain.Interfaces.IHtmlSanitizer;

namespace PublishCoreStandard.Infrastructure.Services;

public class HtmlSanitizer : DomainHtmlSanitizer
{
    private readonly Ganss.Xss.HtmlSanitizer _sanitizer;

    public HtmlSanitizer()
    {
        _sanitizer = new Ganss.Xss.HtmlSanitizer();
        _sanitizer.AllowedTags.Add("h2");
        _sanitizer.AllowedTags.Add("h3");
        _sanitizer.AllowedTags.Add("code");
        _sanitizer.AllowedTags.Add("pre");
        _sanitizer.AllowedAttributes.Add("class");
    }

    public string Sanitize(string html) => _sanitizer.Sanitize(html);
}
