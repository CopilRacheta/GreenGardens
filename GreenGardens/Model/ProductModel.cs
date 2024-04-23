using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GreenGardens.Model
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        [AllowNull]
        public string? ImagePath { get; set; }

        [Required]
        public int Stock { get; set; }
        
        [Required]
        public int ExpectedStock { get; set; }

        [Required]
        public int LoyalityPoints { get; set; }
    }
}
