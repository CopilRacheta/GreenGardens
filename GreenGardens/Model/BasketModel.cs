using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenGardens.Model
{
    public class BasketModel
    {

        [Key]
        public int BasketId { get; set; }


        [Required]
        public int ProductId {  get; set; }

        [ForeignKey("ProductId")]
        public ProductModel Product {  get; set; }

        public int Quantity { get; set; } = 1;

        public string CustomerId {  get; set; }

    }
}
