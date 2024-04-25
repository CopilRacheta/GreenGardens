using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using GreenGardens.Data;
using GreenGardens.Model;

namespace GreenGardens.Pages
{
    public class AccountModel : PageModel
    {
        private readonly AppDbContext _db;

        public List<CustomersModel> Customers { get; set; }

        public AccountModel(AppDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Customers = _db.Customers.ToList();
            var emailAdress = HttpContext.Session.GetString("UserEmail");
            if (!string.IsNullOrEmpty(emailAdress))
            {
                Customers = _db.Customers.Where(c => c.EmailAddress == emailAdress).ToList();
            }

        }
    }
}
