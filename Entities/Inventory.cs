using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Entities
{
    [Table("inventory")]
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        [Required]
        [ForeignKey("ProductInformation")]
        public int ProductId { get; set; }
        public ProductInformation Product { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
