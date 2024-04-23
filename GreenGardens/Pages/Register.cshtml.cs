using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GreenGardens.Model;
using GreenGardens.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace GreenGardens.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public CustomersModel Customer { get; set; }
        
        public RegisterModel(AppDbContext context)
        {
            _context = context;
        }


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            Customer.Password = HashPassword(Customer.Password);
            _context.Customers.Add(Customer);
            _context.SavedChanges();
            return RedirectToPage("Login");
        }


        private string HashPassword(string password)
        {
            byte[] salt = new byte[128/8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
