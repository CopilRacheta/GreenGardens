using GreenGardens.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenGardens.Model
{
    public class BasketModel
    {
        [Key]
        public int BasketId { get; set; }

        [Required]
        public int ProductId { get; set; } // This is the ProductId property

        [ForeignKey("Id")]
        public ProductModel Product { get; set; }

        public int Quantity { get; set; } = 1;

        public string EmailAddress { get; set; }


        public BasketModel(AppDbContext context)
        {
            // Initialization logic here if needed
        }

        // Default parameterless constructor required by Entity Framework
        public BasketModel()
        {
        }
    }
}
