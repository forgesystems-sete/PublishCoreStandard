// =============================================================
// PublishCore Standard — Web Layer
// Program.cs — Pipeline principal da aplicação.
// Configura serviços, middlewares, autenticação e inicializa o banco.
// =============================================================
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PublishCoreStandard.Application.Services;
using PublishCoreStandard.Domain.Entities;
using PublishCoreStandard.Domain.Interfaces;
using PublishCoreStandard.Infrastructure.Data;
using PublishCoreStandard.Infrastructure.Repositories;
using PublishCoreStandard.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection("Site"));

var connString = builder.Configuration.GetConnectionString("BlogDatabase") ?? "Data Source=./Data/blog.db";
builder.Services.AddDbContext<BlogDbContext>(o => o.UseSqlite(connString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(o =>
{
    o.Password.RequireDigit = false;
    o.Password.RequiredLength = 6;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireLowercase = false;
    o.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<BlogDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(o =>
{
    o.LoginPath = "/Admin/Login";
    o.AccessDeniedPath = "/Admin/AccessDenied";
});

builder.Services.AddScoped<IBlogPostRepository, SqliteBlogPostRepository>();
builder.Services.AddScoped<ICategoryRepository, SqliteCategoryRepository>();
builder.Services.AddScoped<IReadingTimeCalculator, ReadingTimeCalculator>();
builder.Services.AddScoped<IHtmlSanitizer, PublishCoreStandard.Infrastructure.Services.HtmlSanitizer>();

builder.Services.AddScoped<BlogPostService>();
builder.Services.AddScoped<CategoryService>();

// ═══════════════════════════════════════════════════════════════
// Razor Pages com Areas — mapeamento da raiz para Area Home
// ═══════════════════════════════════════════════════════════════
builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        // Mapeia a raiz do site (/) para Areas/Home/Pages/Index
        options.Conventions.AddAreaPageRoute("Home", "/Index", "");
    });

builder.Services.AddResponseCompression(o => o.EnableForHttps = true);

var app = builder.Build();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
    db.Database.EnsureCreated();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedData.InitializeAsync(userManager, roleManager, db);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

if (app.Configuration.GetValue<bool>("BEHIND_PROXY"))
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor |
                           Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
    });

app.UseResponseCompression();
app.UseStaticFiles();

app.Use(async (ctx, next) =>
{
    ctx.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
    ctx.Response.Headers["X-Content-Type-Options"] = "nosniff";
    ctx.Response.Headers["Referrer-Policy"] = "no-referrer";
    ctx.Response.Headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=(), interest-cohort=()";
    await next();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
