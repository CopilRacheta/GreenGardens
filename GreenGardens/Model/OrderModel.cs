using System.ComponentModel.DataAnnotations;
namespace GreenGardens.Model
{
    public class OrderModel
    {
        [Key]
        public int Guid { get; set; }
    }
}
