using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GreenGardens.Model
{
    public class ProductsModel
    {
        [Key]
        public int Guid { get; set; }

        [Required,StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [AllowNull]
        public string? ImagePath { get; set; }

        [Required]
        public int Stock { get; set; }
        
        [Required]
        public int ExpectedStock { get; set; }
    }
}
