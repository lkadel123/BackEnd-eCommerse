using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;

namespace WebApplication2.Services
{
    public class InventoryService
    {
        private readonly AppDbContext _context;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        // CREATE: Add new inventory entry
        public async Task<Inventory> AddInventoryAsync(Inventory inventory)
        {
            _context.Inventory.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        // READ: Get all inventory items
        public async Task<IEnumerable<Inventory>> GetInventoryAsync()
        {
            return await _context.Inventory.Include(i => i.Product).ToListAsync();
        }

        // READ: Get inventory by product ID
        public async Task<Inventory> GetInventoryByProductIdAsync(int productId)
        {
            return await _context.Inventory.Include(i => i.Product).FirstOrDefaultAsync(i => i.ProductId == productId);
        }

        // UPDATE: Adjust stock quantity
        public async Task<Inventory> UpdateStockAsync(int productId, int quantity)
        {
            var inventory = await _context.Inventory.FirstOrDefaultAsync(i => i.ProductId == productId);
            if (inventory == null) throw new Exception("Product not found in inventory");

            inventory.StockQuantity += quantity;
            inventory.LastUpdated = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return inventory;
        }

        // DELETE: Remove product from inventory
        public async Task<bool> DeleteInventoryAsync(int productId)
        {
            var inventory = await _context.Inventory.FirstOrDefaultAsync(i => i.ProductId == productId);
            if (inventory == null) throw new Exception("Inventory not found for this product");

            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

