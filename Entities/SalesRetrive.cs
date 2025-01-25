using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Entities
{
    [Table("sales")]
    public class SalesRetrive
    {
        [Key]
        public int SalesId { get; set; } 

        [Required]
        public int UserId { get; set; }
        public Users User { get; set; }

        [Required]
        public int ProductId { get; set; }

        public ProductInformation Product { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalPrice must be greater than 0.")]
        [Column(TypeName = "decimal(18, 2)")] 
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }
    }
}
