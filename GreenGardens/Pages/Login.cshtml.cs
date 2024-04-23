using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GreenGardens.Data;
using GreenGardens.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GreenGardens.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public string EmailAddress { get; set; }

        [BindProperty]
        public string Password { get; set; }


        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var customer = _context.Customers.FirstOrDefault(u=>u.EmailAddress == EmailAddress);

            if(customer != null && VerifyPassword(Password, customer.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, customer.Fname),
                    new Claim(ClaimTypes.Email, customer.EmailAddress),
                    new Claim(ClaimTypes.Role, customer.IsItAdmin ? "Admin" : "User"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToPage("Index");

            }
          return Page();
        }

        public bool VerifyPassword (string providedPassword,string storedHash)
        {
            return true;
        }
    }
}
