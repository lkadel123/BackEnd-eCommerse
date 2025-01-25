using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Entities
{
    [Table("ProductInformation")]
    public class ProductInformation
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductTitle { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal ProductPrice { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal ProductCostPrice { get; set; }
        public string ProductDescription { get; set; }
        [Required]
        public string ProductCategory { get; set; }
        public string? ProductImageUrl { get; set; }
    }
}

