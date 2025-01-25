using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Entities
{
    public class Sales
    {
        [Key]
        public int SalesId { get; set; } // Removed the semicolon that was causing a syntax error

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalPrice must be greater than 0.")]
        [Column(TypeName = "decimal(18, 2)")] // Specifies precision for the decimal in the database
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }
    }
}

