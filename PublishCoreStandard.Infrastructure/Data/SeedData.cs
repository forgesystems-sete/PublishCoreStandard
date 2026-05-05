using Microsoft.AspNetCore.Identity;
using PublishCoreStandard.Domain.Entities;

namespace PublishCoreStandard.Infrastructure.Data;

public static class SeedData
{
    public static async Task InitializeAsync(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        BlogDbContext context)
    {
        // ═══════════════════════════════════════════════════════
        // 1. Admin user (Identity)
        // ═══════════════════════════════════════════════════════
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        if (await userManager.FindByEmailAsync("admin@blog.com") == null)
        {
            var admin = new IdentityUser
            {
                UserName = "admin@blog.com",
                Email = "admin@blog.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, "Admin123!");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        // ═══════════════════════════════════════════════════════
        // 2. Posts de exemplo (herdados do PublishCore Lite)
        // ═══════════════════════════════════════════════════════
        if (!context.Posts.Any())
        {
            var category = new Category
            {
                Name = "Getting Started",
                Slug = "getting-started",
                Description = "Introductory posts about PublishCore Standard."
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var posts = new List<BlogPost>
            {
                new BlogPost
                {
                    Id = "1",
                    Title = "Welcome to PublishCore Standard",
                    Excerpt = "A clean, privacy-first blog template built with .NET. No trackers, no external dependencies — just fast, readable content delivered to your readers.",
                    Content = "<p>Welcome to <strong>PublishCore Standard</strong> — a production-ready blog engine built with .NET 8, Razor Pages and SQLite.</p><p>This template was designed with one goal in mind: give developers a clean, professional starting point that respects their readers' privacy while remaining easy to customize and extend.</p><h2>What's included</h2><ul><li>Clean Architecture with 4 projects (Domain, Application, Infrastructure, Web)</li><li>Full admin panel (posts, categories, site settings)</li><li>PWA — installable and offline-ready</li><li>Dark mode with toggle</li><li>RSS Feed, Sitemap XML, search and pagination</li><li>Zero external dependencies — everything runs locally</li><li>Fully responsive, mobile-first layout</li></ul><h2>Getting started</h2><p>Run <code>dotnet run</code> and you'll have a working blog in under a minute. Access <code>/admin</code> to manage your content.</p><p>We hope you enjoy building with PublishCore Standard!</p>",
                    Author = "PublishCore Team",
                    PublishedDate = new DateTime(2026, 1, 15, 9, 0, 0, DateTimeKind.Utc),
                    Slug = "welcome-to-publishcore-standard",
                    Tags = new List<string> { "announcements", "getting-started" },
                    IsPublished = true,
                    ReadingTimeMinutes = 2,
                    CategoryId = category.Id
                },
                new BlogPost
                {
                    Id = "2",
                    Title = "Why Privacy-First Matters for Your Blog",
                    Excerpt = "Most blog platforms silently load dozens of trackers. We break down why eliminating them makes your site faster, more trustworthy, and legally simpler.",
                    Content = "<p>When you visit most blogs today, your browser quietly loads trackers...</p><p>PublishCore Standard takes a different stance: <strong>zero external requests</strong>.</p>",
                    Author = "Jane Doe",
                    PublishedDate = new DateTime(2026, 1, 20, 10, 30, 0, DateTimeKind.Utc),
                    Slug = "why-privacy-first-matters",
                    Tags = new List<string> { "privacy", "web", "philosophy" },
                    IsPublished = true,
                    ReadingTimeMinutes = 3,
                    CategoryId = category.Id
                },
                new BlogPost
                {
                    Id = "3",
                    Title = "Clean Architecture in .NET: A Practical Guide",
                    Excerpt = "Clean Architecture can sound daunting, but applied thoughtfully to a small project it improves maintainability without adding ceremony.",
                    Content = "<p>Clean Architecture, popularized by Robert C. Martin...</p>",
                    Author = "John Smith",
                    PublishedDate = new DateTime(2026, 2, 1, 8, 0, 0, DateTimeKind.Utc),
                    Slug = "clean-architecture-dotnet-guide",
                    Tags = new List<string> { "dotnet", "architecture", "tutorial" },
                    IsPublished = true,
                    ReadingTimeMinutes = 4,
                    CategoryId = category.Id
                },
                new BlogPost
                {
                    Id = "4",
                    Title = "Customizing PublishCore Standard for Your Brand",
                    Excerpt = "From colors to typography, here's a step-by-step guide to making PublishCore Standard feel like your own product in under 30 minutes.",
                    Content = "<p>PublishCore Standard is designed to be a starting point...</p>",
                    Author = "Jane Doe",
                    PublishedDate = new DateTime(2026, 2, 10, 11, 0, 0, DateTimeKind.Utc),
                    Slug = "customizing-publishcore-standard",
                    Tags = new List<string> { "tutorial", "customization", "getting-started" },
                    IsPublished = true,
                    ReadingTimeMinutes = 3,
                    CategoryId = category.Id
                },
                new BlogPost
                {
                    Id = "5",
                    Title = "Switching from SQLite to PostgreSQL in 10 Minutes",
                    Excerpt = "The clean architecture in PublishCore Standard makes swapping your data source trivial. Here's how to move from SQLite to PostgreSQL.",
                    Content = "<p>One of the core promises of Clean Architecture is that swapping infrastructure should be painless...</p>",
                    Author = "John Smith",
                    PublishedDate = new DateTime(2026, 2, 18, 9, 0, 0, DateTimeKind.Utc),
                    Slug = "switching-sqlite-to-postgresql",
                    Tags = new List<string> { "dotnet", "database", "tutorial", "postgresql" },
                    IsPublished = true,
                    ReadingTimeMinutes = 4,
                    CategoryId = category.Id
                },
                new BlogPost
                {
                    Id = "6",
                    Title = "The Case for Minimal JavaScript",
                    Excerpt = "Modern websites often ship megabytes of JavaScript for features users don't need. PublishCore Standard takes a different approach: HTML-first, JS only when necessary.",
                    Content = "<p>JavaScript is a powerful tool — and one of the most overused ones...</p>",
                    Author = "PublishCore Team",
                    PublishedDate = new DateTime(2026, 3, 1, 10, 0, 0, DateTimeKind.Utc),
                    Slug = "case-for-minimal-javascript",
                    Tags = new List<string> { "javascript", "performance", "philosophy" },
                    IsPublished = true,
                    ReadingTimeMinutes = 3,
                    CategoryId = category.Id
                }
            };

            context.Posts.AddRange(posts);
            await context.SaveChangesAsync();
        }
    }
}
