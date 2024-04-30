using System.ComponentModel.DataAnnotations;

namespace GreenGardens.Model
{
    public class CustomersModel
    {
        [Key]
        public int CustomerId { get; set; }

        [Required, StringLength(100)]
        [Display(Name ="FirstName")]
        public string Fname { get; set; }

        [Required,StringLength(100)]
        [Display(Name ="Surname")]
        public string Sname { get; set; }

        [Required,EmailAddress]
        public string EmailAddress { get; set; }

        [Required,StringLength (100)]
        public string Password { get; set; }

        [Required]
        public bool IsItAdmin { get; set; }

        public int LoyalityPoints { get; set ; }

    }
}
