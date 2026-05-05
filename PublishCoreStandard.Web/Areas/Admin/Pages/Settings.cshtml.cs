// =============================================================
// PublishCore Standard — Web Layer (Admin Area)
// Settings.cshtml.cs — Configurações do site no painel admin.
// =============================================================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using PublishCoreStandard.Domain.Entities;
using System.Text.Json;

namespace PublishCoreStandard.Web.Areas.Admin.Pages;

[Authorize(Roles = "Admin")]
public class SettingsModel : PageModel
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;

    [BindProperty]
    public SiteSettings Settings { get; set; } = default!;

        /// <summary>Inicializa com IWebHostEnvironment e IConfiguration.</summary>
    public SettingsModel(IWebHostEnvironment env, IConfiguration configuration)
    {
        _env = env;
        _configuration = configuration;
    }

        /// <summary>Carrega as configurações atuais do appsettings.json.</summary>
    public IActionResult OnGet()
    {
        Settings = _configuration.GetSection("Site").Get<SiteSettings>() ?? new SiteSettings();
        return Page();
    }

        /// <summary>Persiste as alterações no appsettings.json.</summary>
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var configPath = Path.Combine(_env.ContentRootPath, "appsettings.json");
        var json = System.IO.File.ReadAllText(configPath);
        var jsonDoc = JsonDocument.Parse(json);
        var newSiteSection = JsonSerializer.Serialize(Settings, new JsonSerializerOptions { WriteIndented = true });

        using var stream = new MemoryStream();
        using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }))
        {
            writer.WriteStartObject();
            foreach (var prop in jsonDoc.RootElement.EnumerateObject())
            {
                if (prop.Name == "Site")
                {
                    writer.WritePropertyName("Site");
                    writer.WriteRawValue(newSiteSection);
                }
                else
                {
                    prop.WriteTo(writer);
                }
            }
            writer.WriteEndObject();
        }
        System.IO.File.WriteAllBytes(configPath, stream.ToArray());

        (_configuration as IConfigurationRoot)?.Reload();
        return RedirectToPage();
    }
}

