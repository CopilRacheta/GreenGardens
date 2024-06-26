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

            var user = _context.Customers.FirstOrDefault(u =>u.EmailAddress == EmailAddress);

            if(user != null && VerifyPassword(Password, user.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Fname),
                    new Claim(ClaimTypes.Email, user.EmailAddress),
                    new Claim(ClaimTypes.Role, user.IsItAdmin ? "Admin" : "User"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                // Store email in session
                HttpContext.Session.SetString("UserEmail", user.EmailAddress);

                HttpContext.Session.SetString("UserId", user.EmailAddress);


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
