// =============================================================
// PublishCore Standard — Web Layer (Admin Area)
// Login.cshtml.cs — Autenticação do painel administrativo.
// =============================================================
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace PublishCoreStandard.Web.Areas.Admin.Pages;

[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<LoginModel> _logger;

    public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public string? ErrorMessage { get; set; }

        /// <summary>Exibe a página de login. Redireciona ao dashboard se já autenticado.</summary>
    public IActionResult OnGet()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToPage("/Index");

        return Page();
    }

        /// <summary>Processa o login via SignInManager. Retorna à página com erro se falhar.</summary>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var result = await _signInManager.PasswordSignInAsync(
            Input.Email,
            Input.Password,
            Input.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            _logger.LogInformation("Admin logged in.");
            return RedirectToPage("/Index");
        }

        if (result.RequiresTwoFactor)
        {
            ErrorMessage = "Two-factor authentication is not supported.";
            return Page();
        }

        if (result.IsLockedOut)
        {
            ErrorMessage = "Account locked out.";
            return Page();
        }

        ErrorMessage = "Invalid login attempt.";
        return Page();
    }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
